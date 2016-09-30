namespace LibRbxl.Instances
{
    public struct Region3int16
    {
        public Vector3int16 Min { get; }
        public Vector3int16 Max { get; }

        public Region3int16(Vector3int16 min, Vector3int16 max)
        {
            Min = min;
            Max = max;
        }
    }
}
