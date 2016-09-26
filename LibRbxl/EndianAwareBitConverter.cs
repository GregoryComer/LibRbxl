using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public static class EndianAwareBitConverter
    {
        public static byte[] GetBytes(short value, Endianness endianness = Endianness.Little)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(int value, Endianness endianness = Endianness.Little)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(long value, Endianness endianness = Endianness.Little)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(float value, Endianness endianness = Endianness.Little)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(double value, Endianness endianness = Endianness.Little)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static short ToInt16(IEnumerable<byte> bytes, Endianness endianness = Endianness.Little)
        {
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse();
            var byteArray = bytes.ToArray();
            return BitConverter.ToInt16(byteArray, 0);
        }

        public static int ToInt32(IEnumerable<byte> bytes, Endianness endianness = Endianness.Little)
        {
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse();
            var byteArray = bytes.ToArray();
            return BitConverter.ToInt32(byteArray, 0);
        }

        public static long ToInt64(IEnumerable<byte> bytes, Endianness endianness = Endianness.Little)
        {
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse();
            var byteArray = bytes.ToArray();
            return BitConverter.ToInt64(byteArray, 0);
        }

        public static float ToSingle(IEnumerable<byte> bytes, Endianness endianness = Endianness.Little)
        {
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse();
            var byteArray = bytes.ToArray();
            return BitConverter.ToSingle(byteArray, 0);
        }

        public static double ToDouble(IEnumerable<byte> bytes, Endianness endianness = Endianness.Little)
        {
            if (BitConverter.IsLittleEndian && endianness != Endianness.Little)
                bytes = bytes.Reverse();
            var byteArray = bytes.ToArray();
            return BitConverter.ToDouble(byteArray, 0);
        }
    }

    public enum Endianness
    {
        Little,
        Big
    }
}
