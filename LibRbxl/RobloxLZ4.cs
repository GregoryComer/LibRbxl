using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using LZ4;

namespace LibRbxl
{
    public static class RobloxLZ4
    {
        public static byte[] ReadBlock(Stream stream)
        {
            var reader = new EndianAwareBinaryReader(stream);
            var compressedLength = reader.ReadInt32();
            var uncompressedLength = reader.ReadInt32();
            reader.ReadInt32(); // Reserved

            var compressedData = reader.ReadBytes(compressedLength);
            var outputBuffer = new byte[uncompressedLength];
            LZ4Codec.Decode(compressedData, 0, compressedLength, outputBuffer, 0, uncompressedLength, true);
            return outputBuffer;
        }

        public static void WriteBlock(Stream stream, byte[] bytes)
        {
            var writer = new EndianAwareBinaryWriter(stream);

            writer.WriteInt32((int) (bytes.Length + 1 + Math.Ceiling((bytes.Length - 0xF) / 255.0)));
            writer.WriteInt32(bytes.Length);
            writer.WriteInt32(0); // Reserved
            
            var compressedBytes = LZ4Codec.Encode(bytes, 0, bytes.Length);
            writer.WriteBytes(compressedBytes);
        }
    }
}
