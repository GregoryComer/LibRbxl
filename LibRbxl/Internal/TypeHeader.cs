using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl.Internal
{
    internal class TypeHeader
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int[] Referents { get; set; }
        public byte[] AdditionalData { get; set; }

        public int InstanceCount => Referents.Length;

        public TypeHeader(string name, int typeId, int[] referents, byte[] additionalData = null)
        {
            TypeId = typeId;
            Name = name;
            Referents = referents;
            AdditionalData = additionalData;
        }

        public static TypeHeader Deserialize(byte[] data)
        {
            return Deserialize(new EndianAwareBinaryReader(new MemoryStream(data) ));
        }

        public static TypeHeader Deserialize(EndianAwareBinaryReader reader)
        {
            var typeId = reader.ReadInt32();
            var typeName = Util.ReadLengthPrefixedString(reader);
            var containsExtraDataByte = reader.ReadByte();
            var instanceCount = reader.ReadInt32();
            var referentArray = Util.ReadReferentArray(reader, instanceCount);
            var extraData = (containsExtraDataByte != 0) ? reader.ReadBytes(instanceCount) : null;
            
            return new TypeHeader(typeName, typeId, referentArray, extraData);
        }

        public byte[] Serialize()
        {
            var stream = new MemoryStream();
            var writer = new EndianAwareBinaryWriter(stream);

            Serialize(writer);

            return stream.GetBuffer().Take((int) stream.Length).ToArray();
        }

        public void Serialize(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32(TypeId);
            Util.WriteLengthPrefixedString(writer, Name);
            writer.WriteByte((byte) (AdditionalData != null ? 1 : 0));
            writer.WriteInt32(InstanceCount);
            writer.WriteBytes(Util.TransformInt32Array(Referents));
            if (AdditionalData != null)
                writer.WriteBytes(AdditionalData);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
