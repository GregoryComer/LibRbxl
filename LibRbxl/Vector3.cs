using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct Vector3
    {
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

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    }
}
