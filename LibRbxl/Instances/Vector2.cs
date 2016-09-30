using System;

namespace LibRbxl.Instances
{
    public struct Vector2
    {
        public float X { get; }
        public float Y { get; }

        public static Vector2 Zero => new Vector2(0, 0);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Unit => new Vector2(X / Magnitude, Y / Magnitude);
        public float Magnitude => (float) Math.Sqrt(X*X + Y*Y);

        public float Dot(Vector2 v)
        {
            return Dot(this, v);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 v, float scale)
        {
            return new Vector2(v.X * scale, v.Y * scale);
        }

        public static Vector2 operator *(float scale, Vector2 v)
        {
            return new Vector2(v.X * scale, v.Y * scale);
        }

        public static Vector2 operator /(Vector2 v, float divisor)
        {
            return new Vector2(v.X / divisor, v.Y / divisor);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X*b.X + a.Y*b.Y;
        }
    }
}
