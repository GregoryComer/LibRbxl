namespace LibRbxl.Instances
{
    public class SunRaysEffect : PostEffect
    {
        public override string ClassName => "SunRaysEffect";

        public float Intensity { get; set; }
        public float Spread { get; set; }
    }
}
