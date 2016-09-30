namespace LibRbxl.Instances
{
    public class FileMesh : DataModelMesh
    {
        public override string ClassName => "FileMesh";

        public string MeshId { get; set; }
        public string TextureId { get; set; }
    }
}
