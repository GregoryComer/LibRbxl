namespace LibRbxl.Instances
{
    public class MeshPart : BasePart
    {
        public override string ClassName => "MeshPart";

        public CollisionFidelity CollisionFidelity { get; set; }
        public string MeshId { get; set; }
        public string TextureId { get; set; }
    }
}
