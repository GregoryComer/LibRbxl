using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using NUnit.Framework;

namespace LibRbxl.Test
{
    [TestFixture]
    public class Vector2Tests
    {
        [Test]
        public void Add()
        {
            var v1 = new Vector2(1, 2);
            var v2 = new Vector2(10, 20);
            var expected = new Vector2(11, 22);

            var sum = v1 + v2;

            Assert.AreEqual(expected, sum);
        }

        [Test]
        public void Difference()
        {
            var v1 = new Vector2(10, 20);
            var v2 = new Vector2(1, 2);
            var expected = new Vector2(9, 18);

            var difference = v1 - v2;

            Assert.AreEqual(expected, difference);
        }

        [Test]
        public void Dot()
        {
            var v1 = new Vector2(1, 2);
            var v2 = new Vector2(10, 20);
            var expected = 50f;

            var dot = v1.Dot(v2);

            Assert.AreEqual(expected, dot);
        }

        [Test]
        public void Dot_Static()
        {
            var v1 = new Vector2(1, 2);
            var v2 = new Vector2(10, 20);
            var expected = 50f;

            var dot = Vector2.Dot(v1, v2);

            Assert.AreEqual(expected, dot);
        }

        [Test]
        public void Scale()
        {
            var v = new Vector2(1, 2);
            var s = 2.0f;
            var expected = new Vector2(2, 4);

            var scaled = v * s;

            Assert.AreEqual(expected, scaled);
        }

        [Test]
        public void Scale_Reversed()
        {
            var v = new Vector2(1, 2);
            var s = 2.0f;
            var expected = new Vector2(2, 4);

            var scaled = s * v;

            Assert.AreEqual(expected, scaled);
        }

        [Test]
        public void Divide()
        {
            var v = new Vector2(2, 8);
            var s = 2.0f;
            var expected = new Vector2(1, 4);

            var result = v / s;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Divide_Reversed()
        {
            var v = new Vector2(2, 4);
            var s = 16.0f;
            var expected = new Vector2(8, 4);

            var scaled = s / v;

            Assert.AreEqual(expected, scaled);
        }
    }
}
