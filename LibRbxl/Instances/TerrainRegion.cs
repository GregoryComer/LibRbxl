namespace LibRbxl.Instances
{
    public class TerrainRegion : Instance
    {
        public override string ClassName => "TerrainRegion";

        public bool IsSmooth { get; set; }
        public Vector3 SizeInCells { get; set; }
    }
}
