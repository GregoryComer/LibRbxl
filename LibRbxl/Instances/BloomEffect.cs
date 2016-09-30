namespace LibRbxl.Instances
{
    public class BloomEffect : PostEffect
    {
        public override string ClassName => "BloomEffect";

        public float Intensity { get; set; }
        public float Size { get; set; }
        public float Threshold { get; set; }
    }
}
