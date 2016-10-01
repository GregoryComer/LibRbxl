using System;
using System.IO;
using LibRbxl.Instances;
using LibRbxl.Internal;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace LibRbxl.Test
{
    [TestFixture]
    public class TypeSerializationTests
    {
        [Test]
        public void String_Deserialize()
        {
            var data = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x54, 0x65, 0x73, 0x74, 0x07, 0x00, 0x00, 0x00, 0x53, 0x74, 0x72, 0x69, 0x6E, 0x67, 0x32 };
            var expected = new[] { "Test", "String2" };

            TestDeserializationHelper(data, reader => Util.ReadStringArray(reader, expected.Length), expected);
        }

        [Test]
        public void String_Serialize()
        {
            var strings = new[] {"Test", "String2"};
            var expected = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x54, 0x65, 0x73, 0x74, 0x07, 0x00, 0x00, 0x00, 0x53, 0x74, 0x72, 0x69, 0x6E, 0x67, 0x32 };
            
            TestSerializationHelper(strings, (writer, s) => Util.WriteStringArray(writer, strings), expected);
        }

        [Test]
        public void Bool_Deserialize()
        {
            var data = new byte[] {0x1, 0x0, 0xFF, 0x0};
            var expected = new[] {true, false, true, false};

            TestDeserializationHelper(data, reader => Util.ReadBoolArray(reader, expected.Length), expected);
        }

        [Test]
        public void Bool_Serialize()
        {
            var values = new[] { true, false, true, false };
            var expected = new byte[] { 0x1, 0x0, 0x1, 0x0 };

            TestSerializationHelper(values, (writer, data) => Util.WriteBoolArray(writer, data), expected);
        }

        [Test]
        public void Int_Deserialize()
        {
            var data = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA, 0x3, 0x0 };
            var expected = new[] {5, -2, 0};

            TestDeserializationHelper(data, reader => Util.ReadInt32Array(reader, expected.Length), expected);
        }

        [Test]
        public void Int_Serialize()
        {
            var values = new[] { 5, -2, 0 };
            var expected = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA, 0x3, 0x0 };

            TestSerializationHelper(values, (writer, data) => Util.WriteInt32Array(writer, data), expected);
        }

        [Test]
        public void Float_Deserialize()
        {
            var data = new byte[] { 0x7F, 0x00, 0x87, 0x00, 0x00, 0xF4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            var expected = new[] {1.0f, 0.0f, -500.0f};

            TestDeserializationHelper(data, reader => Util.ReadFloatArray(reader, expected.Length), expected);
        }

        [Test]
        public void Float_Serialize()
        {
            var values = new[] {1.0f, 0.0f, -500.0f};
            var expected = new byte[] { 0x7F, 0x00, 0x87, 0x00, 0x00, 0xF4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };

            TestSerializationHelper(values, (writer, data) => Util.WriteFloatArray(writer, data), expected);
        }

        [Test]
        public void Double_Deserialize()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x7F, 0xC0 };
            var expected = new[] { 1.0, 0.0, -500 };

            TestDeserializationHelper(data, reader => Util.ReadDoubleArray(reader, expected.Length), expected);
        }

        [Test]
        public void Double_Serialize()
        {
            var values = new[] {1.0, 0.0, -500};
            var expected = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x7F, 0xC0 };

            TestSerializationHelper(values, (writer, data) => Util.WriteDoubleArray(writer, data), expected);
        }

        [Test]
        public void UDim2_Deserialize()
        {
            var data = new byte[] { 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7C, 0x00, 0x7F, 0x99, 0x00, 0x00, 0x99, 0x00, 0x00, 0x9A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x63, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0xE8, 0x00, 0x28 };
            var expected = new[] { new UDim2(-50, 0.0f, 500, .2f), new UDim2(0, 0, 0, 0), new UDim2(50, .5f, 20, 1.0f) };

            TestDeserializationHelper(data, reader => Util.ReadUDim2Array(reader, expected.Length), expected);
        }

        [Test]
        public void UDim2_Serialize()
        {
            var values = new[] {new UDim2(-50, 0.0f, 500, .2f),  new UDim2(0, 0, 0, 0), new UDim2(50, .5f, 20, 1.0f)};
            var expected = new byte[] { 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7C, 0x00, 0x7F, 0x99, 0x00, 0x00, 0x99, 0x00, 0x00, 0x9A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x63, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0xE8, 0x00, 0x28 };

            TestSerializationHelper(values, (writer, data) => Util.WriteUDim2Array(writer, data), expected);
        }

        [Test]
        public void Ray_Deserialize()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x40, 0x40 };
            var expected = new[] { new Ray(Vector3.Zero, new Vector3(1.0f, 2.0f, 3.0f)) };

            TestDeserializationHelper(data, reader => Util.ReadRayArray(reader, expected.Length), expected);
        }

        [Test]
        public void Ray_Serialize()
        {
            var values = new[] {new Ray(Vector3.Zero, new Vector3(1.0f, 2.0f, 3.0f))};
            var expected = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x40, 0x40};

            TestSerializationHelper(values, (writer, data) => Util.WriteRayArray(writer, data), expected);
        }

        [Test]
        public void Faces_Deserialize()
        {
            var data = new byte[] { 0x1, 0x2, 0x4, 0x8, 0x10, 0x20, 0x3F };
            var expected = new[] { Faces.Front, Faces.Bottom, Faces.Left, Faces.Back, Faces.Top, Faces.Right, Faces.Front | Faces.Bottom | Faces.Left | Faces.Back | Faces.Top | Faces.Right };

            TestDeserializationHelper(data, reader => Util.ReadFacesArray(reader, expected.Length), expected);
        }

        [Test]
        public void Faces_Serialize()
        {
            var values = new[] { Faces.Front, Faces.Bottom, Faces.Left, Faces.Back, Faces.Top, Faces.Right, Faces.Front | Faces.Bottom | Faces.Left | Faces.Back | Faces.Top | Faces.Right };
            var expected = new byte[] { 0x1, 0x2, 0x4, 0x8, 0x10, 0x20, 0x3F };

            TestSerializationHelper(values, (writer, data) => Util.WriteFacesArray(writer, data), expected);
        }

        [Test]
        public void Axis_Deserialize()
        {
            var data = new byte[] { 0x1, 0x2, 0x4, 0x7 };
            var expected = new[] { Axis.X, Axis.Y, Axis.Z, Axis.X | Axis.Y | Axis.Z };

            TestDeserializationHelper(data, reader => Util.ReadAxisArray(reader, expected.Length), expected);
        }

        [Test]
        public void Axis_Serialize()
        {
            var values = new[] {Axis.X, Axis.Y, Axis.Z, Axis.X | Axis.Y | Axis.Z};
            var expected = new byte[] {0x1, 0x2, 0x4, 0x7};

            TestSerializationHelper(values, (writer, data) => Util.WriteAxisArray(writer, data), expected);
        }

        [Test]
        public void BrickColor_Deserialize()
        {
            var data = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x9A, 0x17};
            var expected = new [] {BrickColor.White, BrickColor.DarkRed, BrickColor.BrightBlue};

            TestDeserializationHelper(data, reader => Util.ReadBrickColorArray(reader, expected.Length), expected);
        }

        [Test]
        public void BrickColor_Serialize()
        {
            var values = new[] { BrickColor.White, BrickColor.DarkRed, BrickColor.BrightBlue };
            var expected = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x9A, 0x17 };

            TestSerializationHelper(values, (writer, data) => Util.WriteBrickColorArray(writer, data), expected);
        }

        [Test]
        public void Color3_Deserialize()
        {
            var data = new byte[] { 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var expected = new[] { new Color3(.5f, 0, 0), new Color3(0, .5f, 0), new Color3(0, 0, .5f) };

            TestDeserializationHelper(data, reader => Util.ReadColor3Array(reader, expected.Length), expected);
        }

        [Test]
        public void Color3_Serialize()
        {
            var values = new[] {new Color3(.5f, 0, 0), new Color3(0, .5f, 0), new Color3(0, 0, .5f)};
            var expected = new byte[] { 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            TestSerializationHelper(values, (writer, data) => Util.WriteColor3Array(writer, data), expected);
        }

        [Test]
        public void Vector2_Deserialize()
        {
            var data = new byte[] { 0x00, 0x7F, 0x8C, 0x00, 0x33, 0x38, 0x00, 0x33, 0x80, 0x00, 0x34, 0x01, 0x7F, 0x88, 0x6E, 0x00, 0xF4, 0x4F, 0x00, 0x00, 0x8B, 0x00, 0x00, 0x58 };
            var expected = new[] { new Vector2(0.0f, 1.0f), new Vector2(1.2f, 1000f), new Vector2(-10000f, 0.00001f) };

            TestDeserializationHelper(data, reader => Util.ReadVector2Array(reader, expected.Length), expected);
        }

        [Test]
        public void Vector2_Serialize()
        {
            var values = new[] { new Vector2(0.0f, 1.0f), new Vector2(1.2f, 1000f), new Vector2(-10000f, 0.00001f)  };
            var expected = new byte[] { 0x00, 0x7F, 0x8C, 0x00, 0x33, 0x38, 0x00, 0x33, 0x80, 0x00, 0x34, 0x01, 0x7F, 0x88, 0x6E, 0x00, 0xF4, 0x4F, 0x00, 0x00, 0x8B, 0x00, 0x00, 0x58 };

            TestSerializationHelper(values, (writer, data) => Util.WriteVector2Array(writer, data), expected);
        }

        [Test]
        public void Vector3_Deserialize()
        {
            var data = new byte[] { 0x00, 0x88, 0x78, 0x00, 0xF4, 0x47, 0x00, 0x00, 0xAE, 0x00, 0x00, 0x15, 0x7F, 0x6C, 0x79, 0x00, 0x0C, 0x47, 0x00, 0x6F, 0xAE, 0x00, 0x7A, 0x15, 0x80, 0x81, 0x79, 0x00, 0x40, 0xEB, 0x00, 0x00, 0x85, 0x00, 0x01, 0x1F };
            var expected = new[] { new Vector3(0.0f, 1.0f, 2.0f), new Vector3(1000f, .000002f, -5f), new Vector3(-.01f, -.02f, -.03f) };

            TestDeserializationHelper(data, reader => Util.ReadVector3Array(reader, expected.Length), expected);
        }

        [Test]
        public void Vector3_Serialize()
        {
            var values = new[] { new Vector3(0.0f, 1.0f, 2.0f), new Vector3(1000f, .000002f, -5f), new Vector3(-.01f, -.02f, -.03f) };
            var expected = new byte[] { 0x00, 0x88, 0x78, 0x00, 0xF4, 0x47, 0x00, 0x00, 0xAE, 0x00, 0x00, 0x15, 0x7F, 0x6C, 0x79, 0x00, 0x0C, 0x47, 0x00, 0x6F, 0xAE, 0x00, 0x7A, 0x15, 0x80, 0x81, 0x79, 0x00, 0x40, 0xEB, 0x00, 0x00, 0x85, 0x00, 0x01, 0x1F };

            TestSerializationHelper(values, (writer, data) => Util.WriteVector3Array(writer, data), expected);
        }

        [Test]
        public void CFrame_Deserialize()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CFrame_Serialize()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Enumeration_Deserialize()
        {
            var data = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x5, 0x2, 0x0 };
            var expected = new[] { 5, 2, 0 };

            TestDeserializationHelper(data, reader => Util.ReadEnumerationArray(reader, expected.Length), expected);
        }

        [Test]
        public void Enumeration_Serialize()
        {
            var values = new[] { 5, 2, 0 };
            var expected = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x5, 0x2, 0x0 };

            TestSerializationHelper(values, (writer, data) => Util.WriteEnumerationArray(writer, data), expected);
        }

        [Test]
        public void Referent_Deserialize()
        {
            var data = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA, 0x2, 0x4, 0x7 };
            var expected = new[] { 5, 6, 8, 4 };

            TestDeserializationHelper(data, reader => Util.ReadReferentArray(reader, expected.Length), expected);
        }

        [Test]
        public void Referent_Serialize()
        {
            var values = new[] {5, 6, 8, 4};
            var expected = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA, 0x2, 0x4, 0x7};

            TestSerializationHelper(values, (writer, data) => Util.WriteReferentArray(writer, data), expected);
        }

        private static void TestDeserializationHelper<T>(byte[] data, Func<EndianAwareBinaryReader, T[]> readAction, T[] expectedValues)
        {
            var stream = new MemoryStream(data);
            var reader = new EndianAwareBinaryReader(stream);

            var values = readAction(reader);

            Assert.AreEqual(expectedValues, values);
        }

        private static void TestSerializationHelper<T>(T[] values, Action<EndianAwareBinaryWriter, T[]> writeAction, byte[] expectedBytes)
        {
            var stream = new MemoryStream();
            var writer = new EndianAwareBinaryWriter(stream);
            
            writeAction(writer, values);

            var buffer = new byte[stream.Length];
            Array.Copy(stream.GetBuffer(), buffer, stream.Length);

            Assert.AreEqual(expectedBytes, buffer);
        }
    }
}
