using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class EndianAwareBinaryWriter
    {
        private Stream _stream;

        public Endianness Endianness { get; set; }

        public EndianAwareBinaryWriter(Stream stream, Endianness endianness = Endianness.Little)
        {
            _stream = stream;
            Endianness = endianness;
        }

        public void WriteByte(byte value)
        {
            _stream.WriteByte(value);
        }

        public void WriteBytes(byte[] value)
        {
            _stream.Write(value, 0, value.Length);
        }

        public void WriteInt16(short value)
        {
            var bytes = EndianAwareBitConverter.GetBytes(value, Endianness);
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteInt32(int value)
        {
            var bytes = EndianAwareBitConverter.GetBytes(value, Endianness);
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteInt64(long value)
        {
            var bytes = EndianAwareBitConverter.GetBytes(value, Endianness);
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteSingle(float value)
        {
            var bytes = EndianAwareBitConverter.GetBytes(value, Endianness);
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteDouble(double value)
        {
            var bytes = EndianAwareBitConverter.GetBytes(value, Endianness);
            _stream.Write(bytes, 0, bytes.Length);
        }
    }
}
