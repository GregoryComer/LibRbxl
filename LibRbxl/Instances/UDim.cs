namespace LibRbxl.Instances
{
    public struct UDim
    {
        public int Offset { get; }
        public float Scale { get; }

        public UDim(int offset, float scale)
        {
            Offset = offset;
            Scale = scale;
        }
    }
}
