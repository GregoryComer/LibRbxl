namespace LibRbxl.Instances
{
    public struct Rect
    {
        public float Height => Max.Y - Min.Y;
        public Vector2 Min { get; }
        public Vector2 Max { get; }
        public float Width => Max.X - Min.X;

        public Rect(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public Rect(float minX, float minY, float maxX, float maxY) : this(new Vector2(minX, minY), new Vector2(maxX, maxY))
        {
            
        }
    }
}