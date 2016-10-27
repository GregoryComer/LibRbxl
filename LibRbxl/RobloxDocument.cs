using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using LibRbxl.Internal;

namespace LibRbxl
{
    public class RobloxDocument
    {
        public RobloxDocument()
        {
            Children = new ChildCollection(null);
            ReferentProvider = new ReferentProvider();
        }

        public ChildCollection Children { get; }
        public IEnumerable<Instance> Instances => GetInstanceEnumerator(); 
        public ReferentProvider ReferentProvider { get; }
        public Workspace Workspace => (Workspace) Children.FirstOrDefault(n => n is Workspace);

        public Instance FindFirstChild(string name)
        {
            return Children.FirstOrDefault(n => n.Name == name);
        }

        public IEnumerable<Instance> GetInstanceEnumerator()
        {
            var queue = new Queue<Instance>(Children);
            while (queue.Count > 0)
            {
                var child = queue.Dequeue();
                yield return child;
                if (child.Children.Count <= 0) continue;
                foreach (var innerChild in child.Children)
                    queue.Enqueue(innerChild);
            }
        }

        public IEnumerable<Instance> GetChildFirstInstanceEnumerator()
        {
            foreach (var child in Children)
            {
                foreach (var innerChild in GetChildFirstInstanceEnumeratorInternal(child))
                    yield return innerChild;
                yield return child;
            }
        }

        private static IEnumerable<Instance> GetChildFirstInstanceEnumeratorInternal(Instance inst)
        {
            foreach (var child in inst.Children)
            {
                foreach (var innerChild in GetChildFirstInstanceEnumeratorInternal(child))
                    yield return innerChild;
                yield return child;
            }
        }

        #region Serialization

        public void Save(string filename)
        {
            using (var filestream = File.Open(filename, FileMode.Create))
            {
                Save(filestream);
            }
        }

        public void Save(Stream stream)
        {
            var writer = new EndianAwareBinaryWriter(stream);
            var serializer = new RobloxSerializer(this);

            ReferentProvider.ClearCache(); // Clearing existing referent cache guarantees that referents won't be fragmented
            var instances = GetChildFirstInstanceEnumerator().ToArray();
            var typeGroups = instances.GroupBy(n => n.ClassName).OrderBy(n => n.Key).ToDictionary(n => n.Key, n => n.ToArray());

            var typeCount = typeGroups.Count;
            var objectCount = typeGroups.Aggregate(0, (acc, pair) => acc + pair.Value.Length);
            
            writer.WriteBytes(Signatures.Signature); // File signature
            writer.WriteInt32(typeCount); // Generic header values
            writer.WriteInt32(objectCount);
            writer.WriteInt32(0); // Reserved
            writer.WriteInt32(0); // Reserved

            // Write type headers
            var typeHeaders = new TypeHeader[typeCount];
            var nextTypeId = 0;
            foreach (var typeGroup in typeGroups)
            {
                var typeHeader = new TypeHeader(typeGroup.Key, nextTypeId, typeGroup.Value.Select(n => ReferentProvider.GetReferent(n)).ToArray());
                if (IsSingleton(typeGroup))
                {
                    typeHeader.AdditionalData = new byte[typeHeader.InstanceCount];
                    for (var i = 0; i < typeHeader.InstanceCount; i++)
                        typeHeader.AdditionalData[i] = 0x1;
                }
                typeHeaders[nextTypeId] = typeHeader;
                var bytes = typeHeader.Serialize();
                writer.WriteBytes(Signatures.TypeHeaderSignature);
                RobloxLZ4.WriteBlock(stream, bytes);
                nextTypeId++;
            }

            // Write property data
            foreach (var typeGroup in typeGroups)
            {
                var typeHeader = typeHeaders.First(n => n.Name == typeGroup.Key);
                var instanceTypes = serializer.GetUniqueProperties(typeGroup.Value);
                var propertyBlocks = instanceTypes.Select(propertyDescriptor => serializer.FillPropertyBlock(propertyDescriptor.Name, propertyDescriptor.Type, typeHeader.TypeId, typeGroup.Value, ReferentProvider)).ToList();
                foreach (var propertyBlock in propertyBlocks)
                {
                    var bytes = propertyBlock.Serialize();
                    writer.WriteBytes(Signatures.PropBlockSignature);
                    RobloxLZ4.WriteBlock(stream, bytes);
                }
            }

            // Build parent child referent arrays
            var parentData = Util.BuildParentData(instances, ReferentProvider);
            
            var parentDataBytes = Util.SerializeParentData(parentData);
            writer.WriteBytes(Signatures.ParentDataSignature);
            RobloxLZ4.WriteBlock(stream, parentDataBytes);

            // Write ending signature
            writer.WriteBytes(Signatures.EndSignature);
            writer.WriteBytes(Signatures.FileEndSignature);
        }
        
        /// <summary>
        /// This is a debugging aid, which when used as a sorting key, orders instances in the same way Roblox seems to. This is useful when binary comparing files.
        /// </summary>
        private static string GetOrderingKey(IGrouping<string, Instance> n)
        {
            if (n.Key == "CSGDictionaryService")
                return "Caa";
            else if (n.Key == "GamepadService")
                return "Gb";
            else
                return n.Key;
        }

        private bool IsSingleton(KeyValuePair<string, Instance[]> typeGroup)
        {
            if (typeGroup.Value[0] is ISingleton)
                return true;
            var unmanagedType = typeGroup.Value[0] as UnmanagedInstance;
            if (unmanagedType == null)
                return false;
            return unmanagedType.IsSingleton;
        }

        public static RobloxDocument FromStream(Stream stream)
        {
            try
            {
                var reader = new EndianAwareBinaryReader(stream);

                int typeCount;
                int objectCount;
                TypeHeader[] typeHeaders;
                Dictionary<int, List<PropertyBlock>> propertyData;
                Tuple<int, int>[] childParentPairs;
                ReadRaw(reader, out typeCount, out objectCount, out typeHeaders, out propertyData, out childParentPairs);

                // Ignore the ...</roblox>

                // Create RobloxDocument object
                var document = new RobloxDocument();
                var serializer = new RobloxSerializer(document);
                var instances = new List<Instance>(objectCount);
                var propertyCollections = new List<PropertyCollection>();

                // Create instances for described objects
                foreach (var type in typeHeaders)
                {
                    for (var i = 0; i < type.InstanceCount; i++)
                    {
                        var instance = InstanceFactory.Create(type.Name, type.AdditionalData != null);
                        var referent = type.Referents[i];
                        document.ReferentProvider.Add(instance, referent);
                        instances.Add(instance);

                        var propertyCollection = new PropertyCollection();
                        var propertyDataBlockList = propertyData[type.TypeId];
                        foreach (var propertyBlock in propertyDataBlockList)
                            propertyCollection.Add(propertyBlock.GetProperty(i));
                        propertyCollections.Add(propertyCollection);
                    }
                }

                // Set properties
                for (var i = 0; i < instances.Count; i++)
                {
                    var instance = instances[i];
                    serializer.SetProperties(document, instance, propertyCollections[i]);
                }

                // Set parents
                foreach (var pair in childParentPairs)
                {
                    var child = document.ReferentProvider.GetCached(pair.Item1);
                    if (pair.Item2 == -1) // No parent
                    {
                        document.Children.Add(child);
                    }
                    else
                    {
                        var parent = document.ReferentProvider.GetCached(pair.Item2);
                        child.Parent = parent;
                    }
                }
                
                return document;
            }
                /*catch (Exception ex)
            {
                throw new InvalidRobloxFileException("The specified Roblox file is corrupt or invalid.", ex);
            }*/
            finally
            {
            }
        }

        public static RobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }

        public static void ReadRaw(EndianAwareBinaryReader reader, out int typeCount, out int objectCount, out TypeHeader[] typeHeaders, out Dictionary<int, List<PropertyBlock>> propertyData, out Tuple<int, int>[] childParentPairs)
        {
            // Check file signature
            var signatureBytes = reader.ReadBytes(Signatures.Signature.Length);
            if (!signatureBytes.SequenceEqual(Signatures.Signature))
                throw new InvalidRobloxFileException("The file signature does not match.");

            typeCount = reader.ReadInt32();
            objectCount = reader.ReadInt32();
            reader.ReadInt32(); // Reserved
            reader.ReadInt32(); // Reserved

            // Deserialize type headers
            typeHeaders = new TypeHeader[typeCount];
            for (var i = 0; i < typeCount; i++)
            {
                var typeHeaderSignature = reader.ReadBytes(Signatures.TypeHeaderSignature.Length);
                if (!typeHeaderSignature.SequenceEqual(Signatures.TypeHeaderSignature))
                    throw new InvalidRobloxFileException("Invalid type header signature.");

                var decompressedBytes = RobloxLZ4.ReadBlock(reader.Stream);
                var typeHeader = TypeHeader.Deserialize(decompressedBytes);
                typeHeaders[i] = typeHeader;
            }

            // Read property data
            propertyData = new Dictionary<int, List<PropertyBlock>>(); // Key is type id
            byte[] lastPropSignature;

            while (true)
            {
                lastPropSignature = reader.ReadBytes(Signatures.PropBlockSignature.Length);
                if (!lastPropSignature.SequenceEqual(Signatures.PropBlockSignature))
                    break;

                var decompressedBytes = RobloxLZ4.ReadBlock(reader.Stream);
                var propertyBlock = PropertyBlock.Deserialize(decompressedBytes, typeHeaders);

                if (propertyBlock == null)
                    continue;

                if (!propertyData.ContainsKey(propertyBlock.TypeId))
                    propertyData.Add(propertyBlock.TypeId, new List<PropertyBlock>());
                propertyData[propertyBlock.TypeId].Add(propertyBlock);
            }

            if (!lastPropSignature.SequenceEqual(Signatures.ParentDataSignature))
                throw new InvalidRobloxFileException("Missing parent data section.");
            var parentData = RobloxLZ4.ReadBlock(reader.Stream);
            childParentPairs = Util.ReadParentData(parentData);

            var endSignature = reader.ReadBytes(Signatures.EndSignature.Length);
            if (!endSignature.SequenceEqual(Signatures.EndSignature))
                throw new InvalidRobloxFileException("End signature is missing or invalid.");
        }
        #endregion
    }
}
