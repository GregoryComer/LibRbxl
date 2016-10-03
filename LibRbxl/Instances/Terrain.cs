namespace LibRbxl.Instances
{
    public class Terrain : BasePart
    {
        public override string ClassName => "Terrain";

        // TODO: Serialize terrain data
        public bool IsSmooth { get; set; }
        [RobloxIgnore] // TODO Is this serialized?
        public Region3int16 MaxExtents { get; set; }
        public Color3 WaterColor { get; set; }
        public float WaterTransparency { get; set; }
        public float WaterWaveSize { get; set; }
        public float WaterWaveSpeed { get; set; }
    }
}
