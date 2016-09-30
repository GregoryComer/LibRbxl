namespace LibRbxl.Instances
{
    public class SurfaceLight : Light
    {
        public override string ClassName => "SurfaceLight";

        public float Angle { get; set; }
        public NormalId Face { get; set; }
        public float Range { get; set; }
    }
}
