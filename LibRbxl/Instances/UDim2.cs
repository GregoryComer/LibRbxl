namespace LibRbxl.Instances
{
    public struct UDim2
    {
        public UDim X { get; }
        public UDim Y { get; }

        public int OffsetX => X.Offset;
        public float ScaleX => X.Scale;
        public int OffsetY => Y.Offset;
        public float ScaleY => Y.Scale;

        public UDim2(UDim x, UDim y)
        {
            X = x;
            Y = y;
        }

        public UDim2(int offsetX, float scaleX, int offsetY, float scaleY)
        {
            X = new UDim(offsetX, scaleX);
            Y = new UDim(offsetY, scaleY);
        }
    }
}
