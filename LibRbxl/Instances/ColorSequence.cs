namespace LibRbxl.Instances
{
    public struct ColorSequence
    {
        public ColorSequenceKeypoint[] Keypoints { get; }

        public ColorSequence(Color3 c0, Color3 c1) : this(new[] {new ColorSequenceKeypoint(0, c0), new ColorSequenceKeypoint(1, c1)})
        {
            
        }

        public ColorSequence(Color3 color) : this(new [] { new ColorSequenceKeypoint(0, color), new ColorSequenceKeypoint(1, color)})
        {
            
        }

        public ColorSequence(ColorSequenceKeypoint[] keypoints)
        {
            Keypoints = keypoints;
        }
    }

    public struct ColorSequenceKeypoint
    {
        public float Time { get; }
        public Color3 Value { get; }

        public ColorSequenceKeypoint(float time, Color3 value)
        {
            Time = time;
            Value = value;
        }
    }
}
