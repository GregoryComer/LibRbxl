﻿using System;
using System.IO;
using System.Linq;
using LibRbxl.Instances;
using LibRbxl.Internal;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace LibRbxl.Test
{
    [TestFixture]
    public class TypeSerializationTests
    {
        private const float Pi = (float)Math.PI;
        private const float PiOver2 = (float)(Math.PI / 2);

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
            var data = new byte[]
            {
                0x02, 0x03, 0x05, 0x06, 0x07, 0x09, 0x0A, 0x0C, 0x0D, 0x0E, 0x10, 0x11, 0x14, 0x15, 0x17, 0x18, 0x19, 0x1B,
                0x1C, 0x1E, 0x1F, 0x20, 0x22, 0x23, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00,
                0x40, 0x40, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0xA0, 0x40, 0x00, 0x00, 0xC0, 0x40, 0x00, 0x00, 0xE0,
                0x40, 0x00, 0x00, 0x00, 0x41, 0x00, 0x00, 0x10, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00
            };
            var expected = new[]
            {
                // Special matrix types
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(Pi, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(Pi, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, -PiOver2, 0)),
                // Non-special matrices
                new CFrame(new Vector3(1, 2, 3), new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9))
            };

            TestDeserializationHelper(data, reader => Util.ReadCFrameArray(reader, expected.Length), expected);
        }

        [Test]
        public void CFrame_Serialize()
        {
            var values = new[]
            {
                // Special matrix types
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(Pi, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, -Pi)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, -PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(Pi, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(PiOver2, 0, -PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(0, PiOver2, 0)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-PiOver2, 0, PiOver2)),
                new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(-Pi, -PiOver2, 0)),
                // Non-special matrices
                new CFrame(new Vector3(1, 2, 3), new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9))
            };
            var expected = new byte[]
            {
                0x02, 0x03, 0x05, 0x06, 0x07, 0x09, 0x0A, 0x0C, 0x0D, 0x0E, 0x10, 0x11, 0x14, 0x15, 0x17, 0x18, 0x19, 0x1B,
                0x1C, 0x1E, 0x1F, 0x20, 0x22, 0x23, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00,
                0x40, 0x40, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0xA0, 0x40, 0x00, 0x00, 0xC0, 0x40, 0x00, 0x00, 0xE0,
                0x40, 0x00, 0x00, 0x00, 0x41, 0x00, 0x00, 0x10, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00
            };

            TestSerializationHelper(values, (writer, data) => Util.WriteCFrameArray(writer, data), expected);
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

        [Test]
        public void NumberSequence_Deserialize()
        {
            var data = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x20, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0xA0,
                0x41, 0x00, 0x00, 0xC0, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0x40, 0x40
            };
            var expected = new[]
            {
                new NumberSequence(0.0f, 1.0f),
                new NumberSequence(new[]
                {
                    new NumberSequenceKeypoint(0.0f, 10f, 0.0f),
                    new NumberSequenceKeypoint(0.5f, 20f, 1.5f),
                    new NumberSequenceKeypoint(1f, 30f, 3.0f),
                })
            };

            TestDeserializationHelper(data, reader => Util.ReadNumberSequenceArray(reader, expected.Length), expected);
        }

        [Test]
        public void NumberSequence_Serialize()
        {
            var values = new[]
            {
                new NumberSequence(0.0f, 1.0f),
                new NumberSequence(new[]
                {
                    new NumberSequenceKeypoint(0.0f, 10f, 0.0f),
                    new NumberSequenceKeypoint(0.5f, 20f, 1.5f),
                    new NumberSequenceKeypoint(1f, 30f, 3.0f),
                })
            };
            var expected = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x20, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0xA0,
                0x41, 0x00, 0x00, 0xC0, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0x40, 0x40
            };

            TestSerializationHelper(values, (writer, data) => Util.WriteNumberSequenceArray(writer, data), expected);
        }

        [Test]
        public void ColorSequence_Deserialize()
        {
            var data = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC,
                0x4C, 0x3E, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC, 0x4C,
                0x3E, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00,
                0x00, 0x80, 0x3F, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x80, 0x3F,
                0x00, 0x00, 0x00, 0x00
            };
            var expected = new[]
            {
                new ColorSequence(new Color3(0, .1f, .2f)), new ColorSequence(new Color3(0, 0, 0), new Color3(1, 1, 1)),
                new ColorSequence(new[]
                {
                    new ColorSequenceKeypoint(0.0f, new Color3(0, 0, 0)),
                    new ColorSequenceKeypoint(.5f, new Color3(.5f, 0, 0)),
                    new ColorSequenceKeypoint(1.0f, new Color3(.5f, 1.0f, 0))
                })
            };

            TestDeserializationHelper(data, reader => Util.ReadColorSequenceArray(reader, expected.Length), expected);
        }

        [Test]
        public void ColorSequence_Serialize()
        {
            var values = new[]
            {
                new ColorSequence(new Color3(0, .1f, .2f)), new ColorSequence(new Color3(0, 0, 0), new Color3(1, 1, 1)),
                new ColorSequence(new[]
                {
                    new ColorSequenceKeypoint(0.0f, new Color3(0, 0, 0)),
                    new ColorSequenceKeypoint(.5f, new Color3(.5f, 0, 0)),
                    new ColorSequenceKeypoint(1.0f, new Color3(.5f, 1.0f, 0))
                })
            };
            var expected = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC,
                0x4C, 0x3E, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC, 0x4C,
                0x3E, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00,
                0x00, 0x80, 0x3F, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x80, 0x3F,
                0x00, 0x00, 0x00, 0x00
            };

            TestSerializationHelper(values, (writer, data) => Util.WriteColorSequenceArray(writer, data), expected);
        }

        [Test]
        public void NumberRange_Deserialize()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0xC0, 0x3F, 0x00, 0x00, 0xC0, 0x3F };
            var expected = new[]
            {
                new NumberRange(0.0f, 1.0f),
                new NumberRange(1.5f)
            };

            TestDeserializationHelper(data, reader => Util.ReadNumberRangeArray(reader, expected.Length), expected);
        }

        [Test]
        public void NumberRange_Serialize()
        {
            var values = new[]
            {
                new NumberRange(0.0f, 1.0f),
                new NumberRange(1.5f)  
            };
            var expected = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0xC0, 0x3F, 0x00, 0x00, 0xC0, 0x3F };

            TestSerializationHelper(values, (writer, data) => Util.WriteNumberRangeArray(writer, data), expected);
        }

        [Test]
        public void Rectangle_Deserialize()
        {
            var data = new byte[]
            {
                0x00, 0x82, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x7F, 0x82, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x80, 0x82,
                0x00, 0x60, 0x00, 0x00, 0x00, 0x00, 0x80, 0x82, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00
            };
            var expected = new[]
            {
                new Rectangle(0, 1, 2, 3), 
                new Rectangle(9, 10, 11, 12)
            };

            TestDeserializationHelper(data, reader => Util.ReadRectangleArray(reader, expected.Length), expected);
        }

        [Test]
        public void Rectangle_Serialize()
        {
            var values = new[]
            {
                new Rectangle(0, 1, 2, 3),
                new Rectangle(9, 10, 11, 12)
            };
            var expected = new byte[]
            {
                0x00, 0x82, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x7F, 0x82, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x80, 0x82,
                0x00, 0x60, 0x00, 0x00, 0x00, 0x00, 0x80, 0x82, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00
            };

            TestSerializationHelper(values, (writer, data) => Util.WriteRectangleArray(writer, data), expected);
        }

        [Test]
        public void PhysicalProperties_Deserialize()
        {
            var data = new byte[] { 0x00, 0x01, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC, 0x4C, 0x3E, 0x9A, 0x99, 0x99, 0x3E, 0xCD, 0xCC, 0xCC, 0x3E, 0x00, 0x00, 0x00, 0x3F };
            var expected = new[]
            {
                new PhysicalProperties(false),
                new PhysicalProperties(.1f, .2f, .3f, .4f, .5f),
            };

            TestDeserializationHelper(data, reader => Util.ReadPhysicalPropertiesArray(reader, expected.Length), expected);
        }

        [Test]
        public void PhysicalProperties_Serialize()
        {
            var values = new[]
            {
                new PhysicalProperties(false),
                new PhysicalProperties(.1f, .2f, .3f, .4f, .5f),
            };
            var expected = new byte[] { 0x00, 0x01, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC, 0x4C, 0x3E, 0x9A, 0x99, 0x99, 0x3E, 0xCD, 0xCC, 0xCC, 0x3E, 0x00, 0x00, 0x00, 0x3F };

            TestSerializationHelper(values, (writer, data) => Util.WritePhysicalPropertiesArray(writer, data), expected);
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
