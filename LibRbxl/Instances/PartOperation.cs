namespace LibRbxl.Instances
{
    public abstract class PartOperation : BasePart
    {
        public CollisionFidelity CollisionFidelity { get; set; }
        public int TriangleCount { get; set; }
        public bool UsePartColor { get; set; }
    }
}
