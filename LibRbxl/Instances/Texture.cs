namespace LibRbxl.Instances
{
    public class Texture : Decal
    {
        public override string ClassName => "Texture";

        public float StudsPerTileU { get; set; }
        public float StudsPerTileV { get; set; }
    }
}
