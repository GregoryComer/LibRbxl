using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class EndianAwareBinaryReader
    {
        private Stream _stream;

        public Endianness Endianness { get; set; }
        public Stream Stream => _stream;

        public EndianAwareBinaryReader(Stream stream, Endianness endianness = Endianness.Little)
        {
            _stream = stream;
            Endianness = endianness;
        }

        public byte ReadByte()
        {
            var val = _stream.ReadByte();
            if (val == -1)
                throw new EndOfStreamException();
            return (byte) val;
        }

        public byte[] ReadBytes(int count)
        {
            var bytes = new byte[count];
            _stream.Read(bytes, 0, count);
            return bytes;
        }

        public double ReadDouble()
        {
            var bytes = new byte[sizeof(double)];
            _stream.Read(bytes, 0, sizeof(double));
            return EndianAwareBitConverter.ToDouble(bytes, Endianness);
        }

        public short ReadInt16()
        {
            var bytes = new byte[sizeof(short)];
            _stream.Read(bytes, 0, sizeof (short));
            return EndianAwareBitConverter.ToInt16(bytes, Endianness);
        }

        public int ReadInt32()
        {
            var bytes = new byte[sizeof(int)];
            _stream.Read(bytes, 0, sizeof(int));
            return EndianAwareBitConverter.ToInt32(bytes, Endianness);
        }

        public long ReadInt64()
        {
            var bytes = new byte[sizeof(long)];
            _stream.Read(bytes, 0, sizeof(long));
            return EndianAwareBitConverter.ToInt64(bytes, Endianness);
        }

        public float ReadSingle()
        {
            var bytes = new byte[sizeof(float)];
            _stream.Read(bytes, 0, sizeof(float));
            return EndianAwareBitConverter.ToSingle(bytes, Endianness);
        }
    }
}
