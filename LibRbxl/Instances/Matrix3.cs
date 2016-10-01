using System;
using System.Web;
using System.Web.UI;

namespace LibRbxl.Instances
{
    public struct Matrix3 : IEquatable<Matrix3>
    {
        private const float Epsilon = 0.000001f;

        private readonly float[,] _values;

        public float[,] Values => _values;
        public float R00 => _values[0, 0];
        public float R01 => _values[0, 1];
        public float R02 => _values[0, 2];
        public float R10 => _values[1, 0];
        public float R11 => _values[1, 1];
        public float R12 => _values[1, 2];
        public float R20 => _values[2, 0];
        public float R21 => _values[2, 1];
        public float R22 => _values[2, 2];

        public static Matrix3 Identity => new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public Matrix3(float[,] values)
        {
            if (values.GetLength(0) != 3 || values.GetLength(1) != 3)
                throw new ArgumentException("Values array must be of size 3x3.", nameof(values));

            _values = values;
        }

        public Matrix3(float r00, float r01, float r02, float r10, float r11, float r12, float r20, float r21, float r22) : this(new float[3, 3]
            {
                {r00, r01, r02},
                {r10, r11, r12},
                {r20, r21, r22}
            })
        {
            
        }

        public float this[int row, int col] => _values[row, col];

        public float Determinant => R00*R11*R22
                                    + R01*R12*R20
                                    + R02*R10*R21
                                    - R20*R11*R02
                                    - R21*R12*R00
                                    - R22*R10*R01;

        public Matrix3 Inverse => new Matrix3(
            R11*R22-R12*R21,
            R02*R21-R01*R22,
            R01*R12-R02*R11,
            R12*R20-R10*R22,
            R00*R22-R02*R20,
            R02*R10-R00*R12,
            R10*R21-R11*R20,
            R01*R20-R00*R21,
            R00*R11-R01*R10
            ) * (1 / Determinant);

        public Vector3 ToEulerAngles()
        {
            return new Vector3((float) Math.Atan2(-R12, R22), (float) Math.Asin(R02), (float) Math.Atan2(-R01, R00));
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            var vals = new float[3,3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    for (var k = 0; k < 3; k++)
                    {
                        vals[i, j] += m1[i, k]*m2[k, j];
                    }
                }
            }
            return new Matrix3(vals);
        }

        public static Matrix3 operator *(Matrix3 m1, float scale)
        {
            return new Matrix3(m1.R00*scale, m1.R01*scale, m1.R02*scale, m1.R10*scale, m1.R11*scale, m1.R12*scale, m1.R20*scale, m1.R21*scale, m1.R22*scale);
        }

        public static Matrix3 operator *(float scale, Matrix3 m1)
        {
            return new Matrix3(m1.R00 * scale, m1.R01 * scale, m1.R02 * scale, m1.R10 * scale, m1.R11 * scale, m1.R12 * scale, m1.R20 * scale, m1.R21 * scale, m1.R22 * scale);
        }

        public static Matrix3 FromAxisAngle(Vector3 axis, float angle)
        {
            var u = axis.Unit;
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new Matrix3(new float[3, 3]
            {
                {
                    (float) (cos + u.X*u.X*(1 - cos)), (float) (u.X*u.Y*(1 - cos) - u.Z*sin), (float) (u.X*u.Z*(1 - cos) + u.Y*sin)
                },
                {
                    (float) (u.Y*u.X*(1 - cos) + u.Z*sin), (float) (cos + u.Y*u.Y*(1 - cos)), (float) (u.Y*u.Z*(1 - cos) - u.X*sin)
                },
                {
                    (float) (u.Z*u.X*(1 - cos) - u.Y*sin), (float) (u.Z*u.Y*(1 - cos) + u.X*sin), (float) (cos + u.Z*u.Z*(1 - cos))
                }
            });
        }

        internal static Matrix3 FromEulerAngles(Vector3 angles)
        {
            return FromEulerAngles(angles.X, angles.Y, angles.Z);
        }

        public static Matrix3 FromEulerAngles(float x, float y, float z) // 11, 12, 21, 22
        {
            return new Matrix3(new float[3,3]
            {
                { (float) (Math.Cos(y) * Math.Cos(z)), (float) (-Math.Cos(y)*Math.Sin(z)), (float) Math.Sin(y) },
                { (float) (Math.Cos(z) * Math.Sin(x) * Math.Sin(y) + Math.Cos(x) * Math.Sin(z)), (float) (Math.Cos(x) * Math.Cos(z) - Math.Sin(x) * Math.Sin(y) * Math.Sin(z)), (float) (-Math.Cos(y) * Math.Sin(x)) },
                { (float) (Math.Sin(x) * Math.Sin(z) - Math.Cos(x) * Math.Cos(z) * Math.Sin(y)), (float) (Math.Cos(z) * Math.Sin(x) + Math.Cos(x)*Math.Sin(y)*Math.Sin(z)), (float) (Math.Cos(x) * Math.Cos(y)) }
            });
        }

        public bool Equals(Matrix3 other)
        {
            return Math.Abs(R00 - other.R00) < Epsilon &&
                   Math.Abs(R01 - other.R01) < Epsilon &&
                   Math.Abs(R02 - other.R02) < Epsilon &&
                   Math.Abs(R10 - other.R10) < Epsilon &&
                   Math.Abs(R11 - other.R11) < Epsilon &&
                   Math.Abs(R12 - other.R12) < Epsilon &&
                   Math.Abs(R20 - other.R20) < Epsilon &&
                   Math.Abs(R21 - other.R21) < Epsilon &&
                   Math.Abs(R22 - other.R22) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Matrix3 && Equals((Matrix3) obj);
        }

        public override int GetHashCode()
        {
            return _values?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Matrix3 left, Matrix3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix3 left, Matrix3 right)
        {
            return !left.Equals(right);
        }
    }
}
