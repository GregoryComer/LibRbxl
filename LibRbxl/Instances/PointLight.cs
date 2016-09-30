namespace LibRbxl.Instances
{
    public class PointLight : Light
    {
        public override string ClassName => "PointLight";

        public float Range { get; set; }
    }
}
