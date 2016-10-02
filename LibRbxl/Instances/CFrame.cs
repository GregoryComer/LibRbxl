using System;

namespace LibRbxl.Instances
{
    public class CFrame : IEquatable<CFrame>
    {
        public Vector3 Position { get; }
        public Matrix3 Matrix { get; }
        public float X => Position.X;
        public float Y => Position.Y;
        public float Z => Position.Z;

        public CFrame(Vector3 position) : this()
        {
            Position = position;
            Matrix = Matrix3.Identity;
        }

        public CFrame(Vector3 position, Matrix3 matrix)
        {
            Position = position;
            Matrix = matrix;
        }

        public CFrame(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            Matrix = Matrix3.Identity;
        }

        public CFrame()
        {
            Position = Vector3.Zero;
            Matrix = Matrix3.Identity;
        }

        public static CFrame Identity => new CFrame(Vector3.Zero, Matrix3.Identity);

        public static CFrame operator *(CFrame left, CFrame right)
        {
            return new CFrame(left.Position + right.Position * left.Matrix, left.Matrix * right.Matrix);
        }

        public static CFrame FromAxisAngle(Vector3 axis, float angle)
        {
            return new CFrame(Vector3.Zero, Matrix3.FromAxisAngle(axis, angle));
        }

        public static CFrame FromEulerAngles(float x, float y, float z)
        {
            return new CFrame(Vector3.Zero, Matrix3.FromEulerAngles(x, y, z));
        }

        public bool Equals(CFrame other)
        {
            return Position.Equals(other.Position) && Matrix.Equals(other.Matrix);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CFrame && Equals((CFrame) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Position.GetHashCode()*397) ^ Matrix.GetHashCode();
            }
        }

        public static bool operator ==(CFrame left, CFrame right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CFrame left, CFrame right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"{Position}, {{{Matrix.M00}, {Matrix.M01}, {Matrix.M02}, {Matrix.M10}, {Matrix.M11}, {Matrix.M12}, {Matrix.M20}, {Matrix.M21}, {Matrix.M22}}}";
        }
    }
}
