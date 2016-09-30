namespace LibRbxl.Instances
{
    public class Attachment : Instance
    {
        public override string ClassName => "Attachment";

        public Vector3 Axis { get; set; }
        public CFrame CFrame { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 SecondaryAxis { get; set; }
        public bool Visible { get; set; }
        public Vector3 WorldAxis { get; set; }
        public Vector3 WorldPosition { get; set;}
        public Vector3 WorldRotation { get; set;}
        public Vector3 WorldSecondaryAxis { get; set; }
    }
}
