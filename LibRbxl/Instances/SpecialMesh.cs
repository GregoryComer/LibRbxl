namespace LibRbxl.Instances
{
    public class SpecialMesh : FileMesh
    {
        public override string ClassName => "SpecialMesh";

        public int LODX { get; set; } // TODO Determine enumeration
        public int LODY { get; set; }
        public MeshType MeshType { get; set; }
    }
}
