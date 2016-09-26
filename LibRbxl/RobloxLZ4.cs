using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZ4;

namespace LibRbxl
{
    public static class RobloxLZ4
    {
        public static byte[] ReadBlock(Stream stream)
        {
            var reader = new EndianAwareBinaryReader(stream, Endianness.Little);
            var compressedLength = reader.ReadInt32();
            var uncompressedLength = reader.ReadInt32();
            var reserved = reader.ReadInt32();

            var buffer = new byte[uncompressedLength];
            var lz4Stream = new LZ4Stream(stream, CompressionMode.Decompress);
            lz4Stream.Read(buffer, 0, uncompressedLength);
            return buffer;
        }

        public static void WriteBlock(Stream stream, byte[] bytes)
        {
            var writer = new EndianAwareBinaryWriter(stream, Endianness.Little);

            byte[] compressedBytes;
            using (var bufferStream = new MemoryStream(bytes.Length))
            {
                using (var lz4Stream = new LZ4Stream(bufferStream, CompressionMode.Compress))
                {
                    lz4Stream.Write(bytes, 0, bytes.Length);
                }
                compressedBytes = bufferStream.GetBuffer();
            }

            writer.WriteInt32(compressedBytes.Length);
            writer.WriteInt32(bytes.Length);
            writer.WriteInt32(0);
            writer.WriteBytes(compressedBytes);
        }
    }
}
