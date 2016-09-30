using System;
using System.Collections.Generic;
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
            Objects = new List<RobloxObject>();
            ReferentProvider = new ReferentProvider();
        }

        public List<RobloxObject> Objects { get; }
        public ReferentProvider ReferentProvider { get; }

        public static RobloxDocument FromStream(Stream stream)
        {
            try
            {
                var reader = new EndianAwareBinaryReader(stream, Endianness.Little);

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
                    try
                    {
                        var child = document.ReferentProvider.GetCached(pair.Item1);
                        var parent = document.ReferentProvider.GetCached(pair.Item2);
                        child.Parent = parent;
                    }
                    // DEBUG
                    catch (Exception ex)
                    {

                    }
                }

                document.Objects.AddRange(instances);
                return document;
            }
                /*catch (Exception ex)
            {
                throw new InvalidRobloxFileException("The specified Roblox file is corrupt or invalid.", ex);
            }*/
                // DEBUG
            finally
            {
            }

            throw new NotImplementedException();
        }

        public DataModel DataModel { get; }

        public Workspace Workspace { get; }

        public static RobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }
    }
}
