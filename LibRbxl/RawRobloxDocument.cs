using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Internal;

namespace LibRbxl
{
    public class RawRobloxDocument : RobloxDocument
    {
        public int ObjectCount { get; }
        public int TypeCount { get; }
        public TypeHeader[] TypeHeaders { get; }
        public Dictionary<int, List<PropertyBlock>> PropertyData { get; }
        public Tuple<int, int>[] ChildParentPairs { get; }

        public RawRobloxDocument(int objectCount, int typeCount, TypeHeader[] typeHeaders, Dictionary<int, List<PropertyBlock>> propertyData, Tuple<int, int>[] childParentPairs)
        {
            ObjectCount = objectCount;
            TypeCount = typeCount;
            TypeHeaders = typeHeaders;
            PropertyData = propertyData;
            ChildParentPairs = childParentPairs;
        }

        public new static RawRobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }

        public new static RawRobloxDocument FromStream(Stream stream)
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
                var document = new RawRobloxDocument(objectCount, typeCount, typeHeaders, propertyData, childParentPairs);
                FillRobloxDocument(document, objectCount, typeHeaders, propertyData, childParentPairs);
                return document;
            }
            catch (Exception ex)
            {
                throw new InvalidRobloxFileException("The specified Roblox file is corrupt or invalid.", ex);
            }
            finally
            {
            }
        }
    }
}
