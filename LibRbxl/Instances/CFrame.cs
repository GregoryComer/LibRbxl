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
    }
}
