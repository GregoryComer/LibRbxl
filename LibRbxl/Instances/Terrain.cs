namespace LibRbxl.Instances
{
    public class Terrain : BasePart
    {
        public override string ClassName => "Terrain";

        // TODO: Serialize terrain data
        [RobloxIgnore]
        public bool IsSmooth { get; set; }
        [RobloxIgnore]
        public Region3int16 MaxExtents { get; set; }
        public Color3 WaterColor { get; set; }
        public float WaterTransparency { get; set; }
        public float WaterWaveSize { get; set; }
        public float WaterWaveSpeed { get; set; }
    }
}
