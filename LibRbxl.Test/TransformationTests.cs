using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl;
using LibRbxl.Internal;
using NUnit.Framework;

namespace LibRbxl.Test
{
    [TestFixture]
    public class TransformationTests
    {
        [Test]
        public void ReverseTransformFloat()
        {
            var value = new byte[] { 0x82, 0x20, 0x00, 0x01 };
            var expected = -9.0f;

            var transformed = Util.ReverseTransformFloat(value);

            Assert.AreEqual(expected, transformed);
        }

        [Test]
        public void ReverseTransformInt32_Positive()
        {
            var inputValue = 18;
            var expectedOutput = 9;

            var output = Util.ReverseTransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReverseTransformInt32_Negative()
        {
            var inputValue = 21;
            var expectedOutput = -11;

            var output = Util.ReverseTransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReverseTransformInt32_Zero()
        {
            var inputValue = 0;
            var expectedOutput = 0;

            var output = Util.ReverseTransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void TransformFloat()
        {
            var value = -9.0f;
            var expected = new byte[] { 0x82, 0x20, 0x00, 0x01 };

            var transformed = Util.TransformFloat(value);

            Assert.AreEqual(expected, transformed);
        }

        [Test]
        public void TransformInt32Array()
        {
            var values = new[] {5, -11, 0x12345678, 0};
            var expected = new byte[] {0x0, 0x0, 0x24, 0x0, 0x0, 0x0, 0x68, 0x0, 0x0, 0x0, 0xAC, 0x0, 0xA, 0x15, 0xF0, 0x0};

            var output = Util.TransformInt32Array(values);

            Assert.AreEqual(expected, output);
        }

        [Test]
        public void TransformInt32_Positive()
        {
            var inputValue = 5;
            var expectedOutput = 10;

            var output = Util.TransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void TransformInt32_Negative()
        {
            var inputValue = -10;
            var expectedOutput = 19;

            var output = Util.TransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void TransformInt32_Zero()
        {
            var inputValue = 0;
            var expectedOutput = 0;

            var output = Util.TransformInt32(inputValue);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReverseTransformInt32Array()
        {
            var value = new byte[] { 0x0, 0x0, 0x24, 0x0, 0x0, 0x0, 0x68, 0x0, 0x0, 0x0, 0xAC, 0x0, 0xA, 0x15, 0xF0, 0x0 };
            var expected = new[] { 5, -11, 0x12345678, 0 };

            var output = Util.ReverseTransformInt32Array(value);

            Assert.AreEqual(expected, output);
        }
    }
}
