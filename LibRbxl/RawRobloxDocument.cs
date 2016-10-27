using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Internal;

namespace LibRbxl
{
    public class RawRobloxDocument
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

        public static RawRobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }

        public static RawRobloxDocument FromStream(Stream stream)
        {
            var reader = new EndianAwareBinaryReader(stream);

            int typeCount;
            int objectCount;
            TypeHeader[] typeHeaders;
            Dictionary<int, List<PropertyBlock>> propertyData;
            Tuple<int, int>[] childParentPairs;
            RobloxDocument.ReadRaw(reader, out typeCount, out objectCount, out typeHeaders, out propertyData, out childParentPairs);

            return new RawRobloxDocument(objectCount, typeCount, typeHeaders, propertyData, childParentPairs);
        }
    }
}
