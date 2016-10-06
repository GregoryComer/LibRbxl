namespace LibRbxl.Instances
{
    public class SpecialMesh : FileMesh
    {
        public override string ClassName => "SpecialMesh";

        public LevelOfDetailSetting LODX { get; set; }
        public LevelOfDetailSetting LODY { get; set; }
        public MeshType MeshType { get; set; }
    }
}
