using System;
using System.Web;
using System.Web.UI;

namespace LibRbxl.Instances
{
    public class Matrix3 : IEquatable<Matrix3>
    {
        private const float Epsilon = 0.000001f;

        private readonly float[,] _values;

        public float[,] Values => _values;
        public float M00 => _values[0, 0];
        public float M01 => _values[0, 1];
        public float M02 => _values[0, 2];
        public float M10 => _values[1, 0];
        public float M11 => _values[1, 1];
        public float M12 => _values[1, 2];
        public float M20 => _values[2, 0];
        public float M21 => _values[2, 1];
        public float M22 => _values[2, 2];

        public static Matrix3 Identity => new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public Matrix3()
        {
            _values = Identity.Values;
        }

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

        public float Determinant => M00*M11*M22
                                    + M01*M12*M20
                                    + M02*M10*M21
                                    - M20*M11*M02
                                    - M21*M12*M00
                                    - M22*M10*M01;

        public Matrix3 Inverse => new Matrix3(
            M11*M22-M12*M21,
            M02*M21-M01*M22,
            M01*M12-M02*M11,
            M12*M20-M10*M22,
            M00*M22-M02*M20,
            M02*M10-M00*M12,
            M10*M21-M11*M20,
            M01*M20-M00*M21,
            M00*M11-M01*M10
            ) * (1 / Determinant);

        public Vector3 ToEulerAngles()
        {
            return new Vector3((float) Math.Atan2(-M12, M22), (float) Math.Asin(M02), (float) Math.Atan2(-M01, M00));
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
            return new Matrix3(m1.M00*scale, m1.M01*scale, m1.M02*scale, m1.M10*scale, m1.M11*scale, m1.M12*scale, m1.M20*scale, m1.M21*scale, m1.M22*scale);
        }

        public static Matrix3 operator *(float scale, Matrix3 m1)
        {
            return new Matrix3(m1.M00 * scale, m1.M01 * scale, m1.M02 * scale, m1.M10 * scale, m1.M11 * scale, m1.M12 * scale, m1.M20 * scale, m1.M21 * scale, m1.M22 * scale);
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
            return Math.Abs(M00 - other.M00) < Epsilon &&
                   Math.Abs(M01 - other.M01) < Epsilon &&
                   Math.Abs(M02 - other.M02) < Epsilon &&
                   Math.Abs(M10 - other.M10) < Epsilon &&
                   Math.Abs(M11 - other.M11) < Epsilon &&
                   Math.Abs(M12 - other.M12) < Epsilon &&
                   Math.Abs(M20 - other.M20) < Epsilon &&
                   Math.Abs(M21 - other.M21) < Epsilon &&
                   Math.Abs(M22 - other.M22) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Matrix3 && Equals((Matrix3) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        hash = hash*23 + _values[i, j].GetHashCode();
                    }
                }
                return hash;
            }
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
