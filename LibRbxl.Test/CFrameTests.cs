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
    public class CFrameTests
    {
        [Test]
        public void Multiply_Identity()
        {
            var cframe = new CFrame(new Vector3(1, 2, 3), Matrix3.FromEulerAngles(1, 2, 3));

            var product = cframe * CFrame.Identity;

            Assert.AreEqual(cframe, product);
        }

        [Test]
        public void Multiply()
        {
            var cframe1 = CFrame.FromEulerAngles((float) Math.PI, 0, 0);
            var cframe2 = new CFrame(1, 2, 3);
            var expected = new CFrame(new Vector3(1, -1.99999976f, -3.00000024f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-1, (float)8.74227766e-008, (float)0, (float)-8.74227766e-008, (float)-1));

            var product = cframe1*cframe2;

            Assert.AreEqual(expected, product);
        }

        [Test]
        public void Multiply2()
        {
            var cframe1 = CFrame.FromEulerAngles((float)Math.PI / 2, 0, 0);
            var cframe2 = new CFrame(2, 4, 6);
            var expected = new CFrame(new Vector3(2, -6, 3.99999976f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-4.37113883e-008, (float)-1, (float)0, (float)1, (float)-4.37113883e-008));

            var product = cframe1 * cframe2;

            Assert.AreEqual(expected, product);
        }

        [Test]
        public void Multiply3()
        {
            var cframe1 = new CFrame(new Vector3(1, -1.99999976f, -3.00000024f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-1, (float)8.74227766e-008, (float)0, (float)-8.74227766e-008, (float)-1));
            var cframe2 = new CFrame(new Vector3(2, -6, 3.99999976f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-4.37113883e-008, (float)-1, (float)0, (float)1, (float)-4.37113883e-008));
            var expected = new CFrame(new Vector3(3, 4.00000095f, -6.99999952f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)1.31134158e-007, (float)1, (float)0, (float)-1, (float)1.31134158e-007));

            var product = cframe1*cframe2;

            Assert.AreEqual(expected, product);
        }

        [Test]
        public void Multiply4()
        {
            var cframe1 = new CFrame(new Vector3(1, -1.99999976f, -3.00000024f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-1, (float)8.74227766e-008, (float)0, (float)-8.74227766e-008, (float)-1));
            var cframe2 = new CFrame(new Vector3(2, -6, 3.99999976f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)-4.37113883e-008, (float)-1, (float)0, (float)1, (float)-4.37113883e-008));
            var expected = new CFrame(new Vector3(3, -2.99999976f, 2f), new Matrix3((float)1, (float)0, (float)0, (float)0, (float)1.31134158e-007, (float)1, (float)0, (float)-1, (float)1.31134158e-007));

            var product = cframe2 * cframe1;

            Assert.AreEqual(expected, product);
        }
    }
}
