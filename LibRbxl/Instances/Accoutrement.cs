namespace LibRbxl.Instances
{
    public class Accoutrement : Instance
    {
        public override string ClassName => "Accoutrement";

        public Vector3 AttachmentForward { get; set; }
        public CFrame AttachmentPoint { get; set; }
        public Vector3 AttachmentPos { get; set; }
        public Vector3 AttachmentRight { get; set; }
        public Vector3 AttachmentUp { get; set; }
    }
}
