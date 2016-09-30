using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Internal;

namespace LibRbxl
{
    public class RobloxDocument
    {
        public RobloxDocument()
        {
            Objects = new List<Instance>();
            ReferentProvider = new ReferentProvider();
        }

        public List<Instance> Objects { get; }
        public ReferentProvider ReferentProvider { get; }
        
        // public Workspace Workspace { get; }

        public void Save(Stream stream)
        {
            var writer = new EndianAwareBinaryWriter(stream);
            var serializer = new RobloxSerializer(this);

            ReferentProvider.ClearCache(); // Clearing existing referent cache guarantees that referents won't be fragmented
            var groupedObjects = Objects.GroupBy(n => n.ClassName).ToDictionary(n => n.Key, n => n.ToArray());

            var typeCount = groupedObjects.Count;
            var objectCount = Objects.Count;
            
            writer.WriteBytes(Signatures.Signature); // File signature
            writer.WriteInt32(typeCount); // Generic header values
            writer.WriteInt32(objectCount);
            writer.WriteInt32(0); // Reserved
            writer.WriteInt32(0); // Reserved

            // Write type headers
            var typeHeaders = new TypeHeader[typeCount];
            var nextTypeId = 0;
            foreach (var typeGroup in groupedObjects)
            {
                var typeHeader = new TypeHeader(typeGroup.Key, nextTypeId, typeGroup.Value.Select(n => ReferentProvider.GetReferent(n)).ToArray()); // TODO Additional service data
                typeHeaders[nextTypeId] = typeHeader;
                var bytes = typeHeader.Serialize();
                writer.WriteBytes(Signatures.TypeHeaderSignature);
                RobloxLZ4.WriteBlock(stream, bytes);
                nextTypeId++;
            }

            // Write property data
            foreach (var typeGroup in groupedObjects)
            {
                var propertySets = typeGroup.Value.Select(n => serializer.GetProperties(n)).ToArray(); // Set of properties for each object
                var propertyNames = propertySets.SelectMany(n => n.Items.Keys).Distinct().ToArray(); // Array of unique property namers
                var propertyCollections = // Gets a PropertyCollection for each distinct property type
                    propertyNames.Select(
                        propertyName =>
                        {
                            var set = propertySets.Select(
                                propertyCollection =>
                                    propertyCollection.Contains(propertyName)
                                        ? propertyCollection[propertyName]
                                        : serializer.GetPropertyDefault(propertyName, typeGroup.Value.GetType()));
                            return new {Properties = set, PropertyName = propertyName};
                        }).ToDictionary(n => n.PropertyName, n => n.Properties);
                var propertyBlocks = propertyCollections.Select(n => PropertyBlock.FromCollection(n.Key, n.Value));
                foreach (var bytes in propertyBlocks.Select(propertyBlock => propertyBlock.Serialize()))
                {
                    writer.WriteBytes(Signatures.PropBlockSignature);
                    RobloxLZ4.WriteBlock(stream, bytes);
                }
            }
        }
        
        public static RobloxDocument FromStream(Stream stream)
        {
            try
            {
                var reader = new EndianAwareBinaryReader(stream);

                // Check file signature
                var signatureBytes = reader.ReadBytes(Signatures.Signature.Length);
                if (!signatureBytes.SequenceEqual(Signatures.Signature))
                    throw new InvalidRobloxFileException("The file signature does not match.");

                var typeCount = reader.ReadInt32();
                var objectCount = reader.ReadInt32();
                reader.ReadInt32(); // Reserved
                reader.ReadInt32(); // Reserved

                // Deserialize type headers
                var typeHeaders = new TypeHeader[typeCount];
                for (var i = 0; i < typeCount; i++)
                {
                    var typeHeaderSignature = reader.ReadBytes(Signatures.TypeHeaderSignature.Length);
                    if (!typeHeaderSignature.SequenceEqual(Signatures.TypeHeaderSignature))
                        throw new InvalidRobloxFileException("Invalid type header signature.");

                    var decompressedBytes = RobloxLZ4.ReadBlock(stream);
                    var typeHeader = TypeHeader.Deserialize(decompressedBytes);
                    typeHeaders[i] = typeHeader;
                }

                // Read property data
                var propertyData = new Dictionary<int, List<PropertyBlock>>(); // Key is type id
                byte[] lastPropSignature;

                while (true)
                {
                    lastPropSignature = reader.ReadBytes(Signatures.PropBlockSignature.Length);
                    if (!lastPropSignature.SequenceEqual(Signatures.PropBlockSignature))
                        break;

                    var decompressedBytes = RobloxLZ4.ReadBlock(stream);
                    var propertyBlock = PropertyBlock.Deserialize(decompressedBytes, typeHeaders);

                    if (propertyBlock == null)
                        continue;

                    if (!propertyData.ContainsKey(propertyBlock.TypeId))
                        propertyData.Add(propertyBlock.TypeId, new List<PropertyBlock>());
                    propertyData[propertyBlock.TypeId].Add(propertyBlock);
                }

                if (!lastPropSignature.SequenceEqual(Signatures.ParentDataSignature))
                    throw new InvalidRobloxFileException("Missing parent data section.");
                var parentData = RobloxLZ4.ReadBlock(stream);
                var childParentPairs = Util.ReadParentData(parentData);

                var endSignature = reader.ReadBytes(Signatures.EndSignature.Length);
                if (!endSignature.SequenceEqual(Signatures.EndSignature))
                    throw new InvalidRobloxFileException("End signature is missing or invalid.");

                // Ignore the ...</roblox>

                // Create RobloxDocument object
                var document = new RobloxDocument();
                var serializer = new RobloxSerializer(document);
                var instances = new List<Instance>(objectCount);
                var propertyCollections = new List<PropertyCollection>();

                // Create instances for described objects, set referents
                foreach (var type in typeHeaders)
                {
                    for (var i = 0; i < type.InstanceCount; i++)
                    {
                        var instance = InstanceFactory.Create(type.Name);
                        var referent = type.Referents[i];
                        instance.Referent = referent;
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
                    if (pair.Item2 == -1) continue; // No parent
                    var child = document.ReferentProvider.GetCached(pair.Item1);
                    var parent = document.ReferentProvider.GetCached(pair.Item2);
                    child.Parent = parent;
                }

                document.Objects.AddRange(instances);
                return document;
            }
            catch (Exception ex)
            {
                throw new InvalidRobloxFileException("The specified Roblox file is corrupt or invalid.", ex);
            }
        }

        public static RobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }
    }
}
