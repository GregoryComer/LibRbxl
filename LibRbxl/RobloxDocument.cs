using System;
using System.Collections.Generic;
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
            ReferentProvider = new ReferentProvider();
        }

        public List<RobloxObject> Objects { get; set; }
        public ReferentProvider ReferentProvider { get; }

        public static RobloxDocument FromStream(Stream stream)
        {
            try
            {
                var reader = new EndianAwareBinaryReader(stream, Endianness.Little);

                // Check file signature
                var signatureBytes = reader.ReadBytes(Constants.Signature.Length);
                if (!signatureBytes.SequenceEqual(Constants.Signature))
                    throw new InvalidRobloxFileException("The file signature does not match.");

                var typeCount = reader.ReadInt32();
                var objectCount = reader.ReadInt32();
                reader.ReadInt32(); // Reserved
                reader.ReadInt32(); // Reserved
                
                // Deserialize type headers
                var typeHeaders = new TypeHeader[typeCount];
                for (var i = 0; i < typeCount; i++)
                {
                    var typeHeaderSignature = reader.ReadBytes(Constants.TypeHeaderSignature.Length);
                    if (typeHeaderSignature.SequenceEqual(Constants.TypeHeaderSignature))
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
                    lastPropSignature = reader.ReadBytes(Constants.ParentDataSignature.Length);
                    if (!lastPropSignature.SequenceEqual(Constants.ParentDataSignature))
                        break;

                    var decompressedBytes = RobloxLZ4.ReadBlock(stream);
                    var propertyBlock = PropertyBlock.Deserialize(decompressedBytes, typeHeaders);
                    if (!propertyData.ContainsKey(propertyBlock.TypeId))
                        propertyData.Add(propertyBlock.TypeId, new List<PropertyBlock>());
                    propertyData[propertyBlock.TypeId].Add(propertyBlock);
                }

                if (!lastPropSignature.SequenceEqual(Constants.ParentDataSignature))
                    throw new InvalidRobloxFileException("Missing parent data section.");
            }
            catch (Exception ex)
            {
                throw new InvalidRobloxFileException("The specified Roblox file is corrupt or invalid.", ex);
            }

            throw new NotImplementedException();
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
