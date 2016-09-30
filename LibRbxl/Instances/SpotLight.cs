namespace LibRbxl.Instances
{
    public class SpotLight : Light
    {
        public override string ClassName => "SpotLight";

        public float Angle { get; set; }
        public NormalId Face { get; set; }
        public float Range { get; set; }
    }
}
