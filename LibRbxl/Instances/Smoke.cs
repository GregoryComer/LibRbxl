namespace LibRbxl.Instances
{
    public class Smoke : Instance
    {
        public override string ClassName => "Smoke";

        public Color3 Color { get; set; }
        public bool Enabled { get; set; }
        public float Opacity { get; set; }
        public float RiseVelocity { get; set; }
        public float Size { get; set; }
    }
}
