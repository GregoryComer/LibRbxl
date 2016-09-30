namespace LibRbxl.Instances
{
    public abstract class Light : Instance
    {
        public float Brightness { get; set; }
        public Color3 Color { get; set; }
        public bool Enabled { get; set; }
        public bool Shadows { get; set; }
    }
}
