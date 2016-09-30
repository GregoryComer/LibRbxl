using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    internal static class Util
    {
        public static readonly Encoding RobloxEncoding = Encoding.GetEncoding("ISO-8859-1");

        public static byte[] DeinterleaveBytes(byte[] data, int valueSize)
        {
            if (data.Length % valueSize != 0)
                throw new ArgumentException("Data length must be a multiple of value size.");

            var deinterleaved = new byte[data.Length];
            var valueCount = data.Length/valueSize;

            for (var i = 0; i < valueCount; i++)
            {
                deinterleaved[i*valueSize] = data[i];
                deinterleaved[i*valueSize + 1] = data[i + valueCount];
                deinterleaved[i*valueSize + 2] = data[i + valueCount * 2];
                deinterleaved[i*valueSize + 3] = data[i + valueCount * 3];
            }

            return deinterleaved;
        }

        public static byte[] InterleaveBytes(byte[] data, int valueSize)
        {
            if (data.Length % valueSize != 0)
                throw new ArgumentException("Data length must be a multiple of value size.");

            var interleavedBytes = new byte[data.Length];
            var valueCount = data.Length / valueSize;

            for (var i = 0; i < valueCount; i++)
            {
                interleavedBytes[i] = data[i*valueSize];
                interleavedBytes[i + valueCount] = data[i*valueSize + 1];
                interleavedBytes[i + valueCount * 2] = data[i*valueSize + 2];
                interleavedBytes[i + valueCount * 3] = data[i * valueSize + 3];
            }

            return interleavedBytes;
        }

        public static Axis[] ReadAxisArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, r => (Axis)r.ReadByte());
        }

        public static bool[] ReadBoolArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, r => r.ReadByte() != 0);
        }

        public static BrickColor[] ReadBrickColorArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadInterleavedPropertyDataArrayHelper(reader, count, sizeof (int), bytes => (BrickColor)EndianAwareBitConverter.ToInt32(bytes, Endianness.Big));
        }

        public static Color3[] ReadColor3Array(EndianAwareBinaryReader reader, int count)
        {
            var rValues = ReadFloatArray(reader, count);
            var gValues = ReadFloatArray(reader, count);
            var bValues = ReadFloatArray(reader, count);

            var colorValues=  new Color3[count];
            for (var i = 0; i < count; i++)
            {
                colorValues[i] = new Color3(rValues[i], gValues[i], bValues[i]);
            }

            return colorValues;
        }

        public static double[] ReadDoubleArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, r => r.ReadDouble());
        }

        public static Faces[] ReadFacesArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, r => (Faces)r.ReadByte());
        }

        public static float[] ReadFloatArray(EndianAwareBinaryReader reader, int count)
        {
            var interleaved = reader.ReadBytes(count * sizeof(float));
            var deinterleaved = DeinterleaveBytes(interleaved, sizeof(float));
            var values = new float[count];

            for (var i = 0; i < count; i++)
            {
                values[i] = ReverseTransformFloat(deinterleaved, i * sizeof(float));
            }

            return values;
        }

        public static int[] ReadEnumerationArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadInterleavedPropertyDataArrayHelper(reader, count, sizeof (int), bytes => EndianAwareBitConverter.ToInt32(bytes, Endianness.Big));
        }

        public static int[] ReadInt32Array(EndianAwareBinaryReader reader, int count)
        {
            var bytes = reader.ReadBytes(count*sizeof (int));
            return ReverseTransformInt32Array(bytes);
        }

        public static string ReadLengthPrefixedString(EndianAwareBinaryReader reader)
        {
            var stringLength = reader.ReadInt32();
            var stringBytes = reader.ReadBytes(stringLength);
            return RobloxEncoding.GetString(stringBytes);
        }

        public static Tuple<int, int>[] ReadParentData(byte[] data) // Tuple format is (Child, Parent)
        {
            var stream = new MemoryStream(data);
            var reader = new EndianAwareBinaryReader(stream);
            return ReadParentData(reader);
        }

        public static Tuple<int, int>[] ReadParentData(EndianAwareBinaryReader reader) // Tuple format is (Child, Parent)
        {
            reader.ReadByte(); // Reserved
            var entryCount = reader.ReadInt32();

            var childReferents = ReadReferentArray(reader, entryCount);
            var parentReferents = ReadReferentArray(reader, entryCount);

            var pairs = new Tuple<int,int>[entryCount];
            for (var i = 0; i < entryCount; i++)
                pairs[i] = new Tuple<int, int>(childReferents[i], parentReferents[i]);
            return pairs;
        }

        public static Ray[] ReadRayArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, r => new Ray(ReadVector3(reader), ReadVector3(reader)));
        }

        public static int[] ReadReferentArray(EndianAwareBinaryReader reader, int count)
        {
            var values = new int[count];
            var bytes = reader.ReadBytes(count*sizeof (int));
            var deinterleaved = DeinterleaveBytes(bytes, sizeof (int));

            var last = 0;
            var buffer = new byte[sizeof (int)];
            for (var i = 0; i < count; i++)
            {
                Array.Copy(deinterleaved, i*sizeof (int), buffer, 0, sizeof (int));
                var value = EndianAwareBitConverter.ToInt32(buffer, Endianness.Big);
                value = ReverseTransformInt32(value);
                value += last;
                values[i] = value;
                last = value;
            }

            return values;
        }

        public static Vector3 ReadVector3(EndianAwareBinaryReader reader)
        {
            return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static T[] ReadPropertyDataArrayHelper<T>(EndianAwareBinaryReader reader, int count, Func<EndianAwareBinaryReader, T> readProc)
        {
            var values = new T[count];
            for (var i = 0; i < count; i++)
                values[i] = readProc(reader);
            return values;
        }

        public static T[] ReadInterleavedPropertyDataArrayHelper<T>(EndianAwareBinaryReader reader, int count, int valueSize, Func<byte[], T> convertProc)
        {
            var values = new T[count];
            var bytes = reader.ReadBytes(count*valueSize);
            var deinterleaved = DeinterleaveBytes(bytes, valueSize);

            var buffer = new byte[valueSize];
            for (var i = 0; i < count; i++)
            {
                Array.Copy(deinterleaved, i * valueSize, buffer, 0, valueSize);
                values[i] = convertProc(buffer);
            }

            return values;
        }
        
        public static string[] ReadStringArray(EndianAwareBinaryReader reader, int count)
        {
            return ReadPropertyDataArrayHelper(reader, count, ReadLengthPrefixedString);
        }

        public static UDim2[] ReadUDim2Array(EndianAwareBinaryReader reader, int count)
        {
            var scaleXValues = ReadFloatArray(reader, count);
            var scaleYValues = ReadFloatArray(reader, count);
            var offsetXValues = ReadInt32Array(reader, count);
            var offsetYValues = ReadInt32Array(reader, count);

            var values = new UDim2[count];
            for (var i = 0; i < count; i++)
                values[i] = new UDim2(offsetXValues[i], scaleXValues[i], offsetYValues[i], scaleYValues[i]);
            return values;
        }

        public static Vector2[] ReadVector2Array(EndianAwareBinaryReader reader, int count)
        {
            var xValues = ReadFloatArray(reader, count);
            var yValues = ReadFloatArray(reader, count);

            var values = new Vector2[count];
            for (var i = 0; i < count; i++)
                values[i] = new Vector2(xValues[i], yValues[i]);
            return values;
        }

        public static Vector3[] ReadVector3Array(EndianAwareBinaryReader reader, int count)
        {
            var xValues = ReadFloatArray(reader, count);
            var yValues = ReadFloatArray(reader, count);
            var zValues = ReadFloatArray(reader, count);

            var values = new Vector3[count];
            for (var i = 0; i < count; i++)
                values[i] = new Vector3(xValues[i], yValues[i], zValues[i]);
            return values;
        }

        public static float ReverseTransformFloat(byte[] val)
        {
            return ReverseTransformFloat(val, 0);
        }

        public static float ReverseTransformFloat(byte[] buffer, int index)
        {
            var bytes = new byte[sizeof (float)];
            Array.Copy(buffer, index, bytes, 0, sizeof(float));
            var intForm = (uint)EndianAwareBitConverter.ToInt32(bytes, Endianness.Big);
            var transformedIntForm = (intForm >> 1) | (intForm << 31); // Rotate right 1 bit
            var transformedBytes = EndianAwareBitConverter.GetBytes((int)transformedIntForm, Endianness.Big);
            return EndianAwareBitConverter.ToSingle(transformedBytes, Endianness.Big);
        }

        public static int ReverseTransformInt32(int value) // TODO: This can probably be done without a branch
        {
            if (value%2 == 0)
                return value/2;
            else
                return -(value + 1)/2;
        }

        public static int[] ReverseTransformInt32Array(byte[] data)
        {
            if (data.Length % sizeof(int) != 0)
                throw new ArgumentException("Invalid interleaved data. Data length must be a multiple of 4 bytes.");

            var valueCount = data.Length / sizeof(int);
            var values = new int[valueCount];
            var buffer = new byte[sizeof(int)];

            var deinterleavedData = DeinterleaveBytes(data, sizeof (int));
            
            for (var i = 0; i < valueCount; i++)
            {
                Array.Copy(deinterleavedData, i * sizeof(int), buffer, 0, sizeof(int));
                var transformedValue = EndianAwareBitConverter.ToInt32(buffer, Endianness.Big);
                values[i] = ReverseTransformInt32(transformedValue);
            }

            return values;
        }

        public static byte[] TransformFloat(float val)
        {
            byte[] buffer = new byte[sizeof(float)];
            TransformFloat(val, buffer, 0);
            return buffer;
        }

        public static void TransformFloat(float val, byte[] buffer, int index)
        {
            var floatBytes = EndianAwareBitConverter.GetBytes(val, Endianness.Big);
            var intForm = (uint)EndianAwareBitConverter.ToInt32(floatBytes, Endianness.Big);
            var transformedIntForm = (intForm << 1) | (intForm >> 31); // Rotate left 1 bit
            var transformedBytes = EndianAwareBitConverter.GetBytes((int)transformedIntForm, Endianness.Big);
            Array.Copy(transformedBytes, 0, buffer, index, sizeof(float));
        }

        public static int TransformInt32(int value) // TODO: This can probably be done without a branch
        {
            if (value >= 0)
                return value*2;
            else
                return -value*2 - 1;
        }

        public static byte[] TransformInt32Array(int[] data)
        {
            var bytes = new byte[data.Length * sizeof(int)];

            for (var i = 0; i < data.Length; i++)
            {
                var transformed = TransformInt32(data[i]);
                var valueBytes = EndianAwareBitConverter.GetBytes(transformed, Endianness.Big);
                Array.Copy(valueBytes, 0, bytes, i * sizeof(int), sizeof(int));
            }

            var interleaved = InterleaveBytes(bytes, sizeof (int));
            return interleaved;
        }

        public static void WriteAxisArray(EndianAwareBinaryWriter writer, Axis[] values)
        {
            WritePropertyDataHelper(writer, values, (w, val) => w.WriteByte((byte)val));
        }

        public static void WriteBoolArray(EndianAwareBinaryWriter writer, bool[] values)
        {
            WritePropertyDataHelper(writer, values, (w, val) => w.WriteByte((byte) (val ? 1 : 0)));
        }

        public static void WriteBrickColorArray(EndianAwareBinaryWriter writer, BrickColor[] values)
        {
            WriteInterleavedPropertyDataHelper(writer, values, sizeof(int), val => EndianAwareBitConverter.GetBytes((int)val));
        }

        public static void WriteColor3Array(EndianAwareBinaryWriter writer, Color3[] values)
        {
            var rValues = new float[values.Length];
            var gValues = new float[values.Length];
            var bValues = new float[values.Length];

            WriteFloatArray(writer, rValues);
            WriteFloatArray(writer, gValues);
            WriteFloatArray(writer, bValues);
        }

        public static void WriteEnumerationArray(EndianAwareBinaryWriter writer, int[] values)
        {
            WriteInterleavedPropertyDataHelper(writer, values, sizeof(int), val => EndianAwareBitConverter.GetBytes(val));
        }

        public static void WriteFacesArray(EndianAwareBinaryWriter writer, Faces[] values)
        {
            WritePropertyDataHelper(writer, values, (w, val) => w.WriteByte((byte)val));
        }

        public static void WriteFloatArray(EndianAwareBinaryWriter writer, float[] values)
        {
            var buffer = new byte[values.Length * sizeof(float)];
            for (var i = 0; i < values.Length; i++)
            {
                TransformFloat(values[i], buffer, i * sizeof(float));
            }
            var interleaved = InterleaveBytes(buffer, sizeof(float));
            writer.WriteBytes(interleaved);
        }

        public static void WriteDoubleArray(EndianAwareBinaryWriter writer, double[] values)
        {
            WritePropertyDataHelper(writer, values, (w, val) => w.WriteDouble(val));
        }

        public static void WriteInt32Array(EndianAwareBinaryWriter writer, int[] values)
        {
            var bytes = TransformInt32Array(values);
            writer.WriteBytes(bytes);
        }
        
        public static void WriteInterleavedPropertyDataHelper<T>(EndianAwareBinaryWriter writer, T[] values, int valueSize, Func<T, byte[]> convertProc)
        {
            var bytes = new byte[values.Length * valueSize];

            for (var i = 0; i < values.Length; i++)
            {
                var valBytes = convertProc(values[i]);
                Array.Copy(valBytes, 0, bytes, i * valueSize, valueSize);
            }

            var interleaved = InterleaveBytes(bytes, valueSize);
            writer.WriteBytes(interleaved);
        }

        public static void WriteLengthPrefixedString(EndianAwareBinaryWriter writer, string value)
        {
            var stringBytes = RobloxEncoding.GetBytes(value);
            writer.WriteInt32(stringBytes.Length);
            writer.WriteBytes(stringBytes);
        }

        public static void WritePropertyDataHelper<T>(EndianAwareBinaryWriter writer, T[] values, Action<EndianAwareBinaryWriter, T> writeProc)
        {
            foreach (var val in values)
                writeProc(writer, val);
        }

        public static void WriteRayArray(EndianAwareBinaryWriter writer, Ray[] values)
        {
            WritePropertyDataHelper(writer, values, (w, val) => { WriteVector3(w, val.Origin); WriteVector3(w, val.Direction); });
        }

        public static void WriteReferentArray(EndianAwareBinaryWriter writer, int[] values)
        {
            var bytes = new byte[values.Length * sizeof(int)];

            var last = 0;
            for (var i = 0; i < values.Length; i++)
            {
                var valBytes = EndianAwareBitConverter.GetBytes(values[i] - last, Endianness.Big);
                last = values[i] - last;
                Array.Copy(valBytes, 0, bytes, i * sizeof(int), sizeof(int));
            }

            var interleaved = InterleaveBytes(bytes, sizeof(int));
            writer.WriteBytes(interleaved);
        }

        private static void WriteVector3(EndianAwareBinaryWriter writer, Vector3 val)
        {
            writer.WriteSingle(val.X);
            writer.WriteSingle(val.Y);
            writer.WriteSingle(val.Z);
        }

        public static void WriteStringArray(EndianAwareBinaryWriter writer, string[] values)
        {
            WritePropertyDataHelper(writer, values, WriteLengthPrefixedString);
        }

        public static void WriteUDim2Array(EndianAwareBinaryWriter writer, UDim2[] values)
        {
            var scaleXValues = new float[values.Length];
            var scaleYValues = new float[values.Length];
            var offsetXValues = new int[values.Length];
            var offsetYValues = new int[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                scaleXValues[i] = values[i].ScaleX;
                scaleYValues[i] = values[i].ScaleY;
                offsetXValues[i] = values[i].OffsetX;
                offsetYValues[i] = values[i].OffsetY;
            }

            WriteFloatArray(writer, scaleXValues);
            WriteFloatArray(writer, scaleYValues);
            WriteInt32Array(writer, offsetXValues);
            WriteInt32Array(writer, offsetYValues);
        }

        public static void WriteVector2Array(EndianAwareBinaryWriter writer, Vector2[] values)
        {
            var xValues = new float[values.Length];
            var yValues = new float[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                xValues[i] = values[i].X;
                yValues[i] = values[i].Y;
            }

            WriteFloatArray(writer, xValues);
            WriteFloatArray(writer, yValues);
        }

        public static void WriteVector3Array(EndianAwareBinaryWriter writer, Vector3[] values)
        {
            var xValues = new float[values.Length];
            var yValues = new float[values.Length];
            var zValues = new float[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                xValues[i] = values[i].X;
                yValues[i] = values[i].Y;
                zValues[i] = values[i].Z;
            }

            WriteFloatArray(writer, xValues);
            WriteFloatArray(writer, yValues);
            WriteFloatArray(writer, zValues);
        }
    }
}
