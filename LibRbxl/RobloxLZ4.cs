using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var outputBuffer = new byte[uncompressedLength];
            var outputBufferIndex = 0;

            var bytesDecompressed = 0;
            while (bytesDecompressed < compressedLength)
            {
                var token = reader.ReadByte();
                bytesDecompressed++;
                var literalLength = token >> 4;
                var matchLength = token & 0xF;

                if (literalLength != 0)
                {
                    if (literalLength == 0xF)
                    {
                        byte lastByte;
                        do
                        {
                            lastByte = reader.ReadByte();
                            bytesDecompressed++;
                            literalLength += lastByte;
                        } while (lastByte == 0xFF);
                    }

                    //Trace.WriteLine($"[{stream.Position}][{outputBufferIndex}] Copying {literalLength} literal bytes.");

                    var literalBytes = reader.ReadBytes(literalLength);
                    Array.Copy(literalBytes, 0, outputBuffer, outputBufferIndex, literalLength);
                    bytesDecompressed += literalLength;
                    outputBufferIndex += literalLength;
                }

                if (bytesDecompressed >= compressedLength)
                    break;

                var matchOffset = (ushort) reader.ReadInt16();
                bytesDecompressed += 2;

                if (matchLength == 0xF)
                {
                    byte lastByte;
                    do
                    {
                        lastByte = reader.ReadByte();
                        bytesDecompressed++;
                        matchLength += lastByte;
                    } while (lastByte == 0xFF);
                }
                matchLength += 4; // Minimum match

                //Trace.WriteLine($"[{stream.Position}][{outputBufferIndex}] Copying {matchLength} match bytes from offset {matchOffset}.");

                var copied = 0;
                while (copied < matchLength)
                {
                    var copyAmount = Math.Min(matchLength - copied, matchOffset);
                    Array.Copy(outputBuffer, outputBufferIndex - matchOffset, outputBuffer, outputBufferIndex, copyAmount);
                    copied += copyAmount;
                    outputBufferIndex += copyAmount;
                }

                //Array.Copy(outputBuffer, outputBufferIndex - matchOffset, outputBuffer, outputBufferIndex, matchLength);
                //outputBufferIndex += matchLength;
            }

            return outputBuffer;
        }

        public static void WriteBlock(Stream stream, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
