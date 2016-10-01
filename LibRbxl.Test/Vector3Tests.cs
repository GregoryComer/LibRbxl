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
    public class Vector3Tests
    {
        [Test]
        public void MatrixMultiply_Identity()
        {
            var v = new Vector3(1, 2, 3);
            var m = Matrix3.Identity;

            var product = v*m;

            Assert.AreEqual(v, product);
        }

        [Test]
        public void MatrixMultiply()
        {
            var v = new Vector3(3, -2, 1);
            var m = new Matrix3((float)-0.224845082, (float)-0.35017547, (float)0.909297407, (float)-0.0412378013, (float)0.935775876, (float)0.35017547, (float)-0.973521471, (float)0.0412378013, (float)-0.224845082);
            var expected = new Vector3((float)0.935113072, (float)-1.64508963, (float)-3.22788525);

            var transformed = v*m;

            Assert.AreEqual(expected, transformed);
        }

        [Test]
        public void MatrixMultiply2()
        {
            var v = new Vector3(5, 8, -6);
            var m = new Matrix3((float)0.936293423, (float)-0.289629489, (float)0.198669329, (float)0.312991828, (float)0.944702566, (float)-0.0978434011, (float)-0.159345075, (float)0.153792009, (float)0.975170374);
            var expected = new Vector3((float)1.17241514, (float)9.70963955, (float)-5.4174118);

            var transformed = v * m;

            Assert.AreEqual(expected, transformed);
        }

        [Test]
        public void Add()
        {
            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(10, 20, 30);
            var expected = new Vector3(11, 22, 33);

            var sum = v1 + v2;

            Assert.AreEqual(expected, sum);
        }

        [Test]
        public void Difference()
        {
            var v1 = new Vector3(10, 20, 30);
            var v2 = new Vector3(1, 2, 3);
            var expected = new Vector3(9, 18, 27);

            var difference = v1 - v2;

            Assert.AreEqual(expected, difference);
        }

        [Test]
        public void Dot()
        {
            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(10, 20, 30);
            var expected = 140f;

            var dot = v1.Dot(v2);

            Assert.AreEqual(expected, dot);
        }

        [Test]
        public void Dot_Static()
        {
            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(10, 20, 30);
            var expected = 140f;

            var dot = Vector3.Dot(v1, v2);

            Assert.AreEqual(expected, dot);
        }

        [Test]
        public void Scale()
        {
            var v = new Vector3(1, 2, 3);
            var s = 2.0f;
            var expected = new Vector3(2, 4, 6);

            var scaled = v*s;

            Assert.AreEqual(expected, scaled);
        }

        [Test]
        public void Scale_Reversed()
        {
            var v = new Vector3(1, 2, 3);
            var s = 2.0f;
            var expected = new Vector3(2, 4, 6);

            var scaled = s*v;

            Assert.AreEqual(expected, scaled);
        }

        [Test]
        public void Divide()
        {
            var v = new Vector3(2, 8, 16);
            var s = 2.0f;
            var expected = new Vector3(1, 4, 8);

            var result = v/s;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Divide_Reversed()
        {
            var v = new Vector3(2, 4, 8);
            var s = 16.0f;
            var expected = new Vector3(8, 4, 2);

            var scaled = s/v;

            Assert.AreEqual(expected, scaled);
        }
    }
}
