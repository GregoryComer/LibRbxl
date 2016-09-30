namespace LibRbxl.Instances
{
    public class Decal : FaceInstance
    {
        public override string ClassName => "Decal";

        public string Texture { get; set; }
        public float Transparency { get; set; }
    }
}
