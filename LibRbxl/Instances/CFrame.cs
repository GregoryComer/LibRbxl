namespace LibRbxl.Instances
{
    public struct CFrame
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
    }
}
