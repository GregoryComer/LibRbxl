using System;

namespace LibRbxl.Instances
{
    public struct Vector3 : IEquatable<Vector3>
    {
        private const float Epsilon = 0.000001f;

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public static Vector3 Zero => new Vector3(0, 0, 0);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Unit => new Vector3(X / Magnitude, Y / Magnitude, Z / Magnitude);
        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        public float Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 v, float scale)
        {
            return new Vector3(v.X * scale, v.Y * scale, v.Z * scale);
        }

        public static Vector3 operator *(float scale, Vector3 v)
        {
            return new Vector3(v.X * scale, v.Y * scale, v.Z * scale);
        }

        public static Vector3 operator /(Vector3 v, float divisor)
        {
            return new Vector3(v.X / divisor, v.Y / divisor, v.Z / divisor);
        }

        public static Vector3 operator /(float divisor, Vector3 v)
        {
            return new Vector3(divisor / v.X, divisor / v.Y, divisor/ v.Z);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector3 operator *(Vector3 v, Matrix3 m)
        {
            return new Vector3(v.X * m.R00 + v.Y * m.R01 + v.Z * m.R02,
                v.X * m.R10 + v.Y * m.R11 + v.Z * m.R12,
                v.X * m.R20 + v.Y * m.R21 + v.Z * m.R22);
        }

        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            return new Vector3(v.X * m.R00 + v.Y * m.R01 + v.Z * m.R02,
                v.X * m.R10 + v.Y * m.R11 + v.Z * m.R12,
                v.X * m.R20 + v.Y * m.R21 + v.Z * m.R22);
        }

        public bool Equals(Vector3 other)
        {
            return Math.Abs(X - other.X) < Epsilon && Math.Abs(Y - other.Y) < Epsilon && Math.Abs(Z - other.Z) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector3 && Equals((Vector3) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !left.Equals(right);
        }
    }
}
