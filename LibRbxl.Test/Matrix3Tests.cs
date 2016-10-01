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
    public class Matrix3Tests
    {
        [Test]
        public void Multiply_Identity()
        {
            var m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var m2 = Matrix3.Identity;

            var product = m1*m2;

            Assert.AreEqual(m1, product);
        }

        [Test]
        public void Determinant()
        {
            var m = new Matrix3(5, -2, 1, 0, 3, -1, 2, 0, 7);

            var det = m.Determinant;

            Assert.AreEqual(103, det);
        }

        [Test]
        public void Determinant2()
        {
            var m = new Matrix3(1,2,3,4,5,6,7,8,9);

            var det = m.Determinant;

            Assert.AreEqual(0, det);
        }

        [Test]
        public void Inverse()
        {
            var m = new Matrix3(7, 2, 1, 0, 3, -1, -3, 4, -2);
            var expected = new Matrix3(-2, 8, -5, 3, -11, 7, 9, -34, 21);

            var inverse = m.Inverse;

            Assert.AreEqual(expected, inverse);
        }

        [Test]
        public void Inverse2()
        {
            var m = new Matrix3(1, 2, 3, 0, 4, 5, 1, 0, 6);
            var expected = new Matrix3(12.0f/11, -6.0f / 11, -1.0f / 11, 5.0f / 22, 3.0f / 22, -5.0f / 22, -2.0f / 11, 1.0f / 11, 2.0f / 11);

            var inverse = m.Inverse;

            Assert.AreEqual(expected, inverse);
        }

        [Test]
        public void AxisAngle()
        {
            var axis = new Vector3(1, 2, 3).Unit;
            var angle = 2.0f;
            var expected = new Matrix3((float) -0.314993471, (float) -0.526753187, (float) 0.789500117,
                (float) 0.931366682, (float) -0.0115333796, (float) 0.363900244, (float) -0.182579845,
                (float) 0.84994024, (float) 0.494233459);

            var mat = Matrix3.FromAxisAngle(axis, angle);

            Assert.AreEqual(expected, mat);
        }

        [Test]
        public void AxisAngle2()
        {
            var axis = new Vector3(-3, 2, -1).Unit;
            var angle = 1.0f;
            var expected = new Matrix3((float) 0.835822284, (float) 0.0278792381, (float) 0.548291862, (float) -0.421905935, (float) 0.671644509, (float) 0.609006643, (float) -0.351278484, (float) -0.740348935, (float) 0.57313782);

            var mat = Matrix3.FromAxisAngle(axis, angle);

            Assert.AreEqual(expected, mat);
        }

        [Test]
        public void EulerAngles()
        {
            var angles = new Vector3(1.0f, .5f, 2.0f);
            var expected = new Matrix3((float)-0.365203202, (float)-0.797983527, (float)0.47942555, (float)0.323412389, (float)-0.591676235, (float)-0.738460243, (float)0.872943878, (float)-0.11463587, (float)0.474159837);

            var mat = Matrix3.FromEulerAngles(angles.X, angles.Y, angles.Z);

            Assert.AreEqual(expected, mat);
        }

        [Test]
        public void EulerAngles2()
        {
            var angles = new Vector3(-.25f, 1.1f, 0f);
            var expected = new Matrix3((float)0.453596085, (float)0, (float)0.891207397, (float)-0.22048825, (float)0.968912423, (float)0.112221472, (float)-0.863501906, (float)-0.247403964, (float)0.439494878);

            var mat = Matrix3.FromEulerAngles(angles.X, angles.Y, angles.Z);

            Assert.AreEqual(expected, mat);
        }

        [Test]
        public void ToEulerAngles_Identity()
        {
            var m = Matrix3.Identity;
            var expected = new Vector3(0, 0, 0);

            var angles = m.ToEulerAngles();

            Assert.AreEqual(expected, angles);
        }

        [Test]
        public void ToEulerAngles()
        {
            var m = new Matrix3((float)-0.0294370614, (float)-0.0643211529, (float)0.997494996, (float)0.141997159, (float)-0.988075733, (float)-0.0595232993, (float)0.989429235, (float)0.1398893, (float)0.0382194705);
            var expected = new Vector3(1, 1.5f, 2);

            var angles = m.ToEulerAngles();

            Assert.AreEqual(expected, angles);
        }
    }
}
