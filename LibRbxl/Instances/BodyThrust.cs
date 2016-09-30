namespace LibRbxl.Instances
{
    public class BodyThrust : BodyMover
    {
        public override string ClassName => "BodyThrust";

        public Vector3 Force { get; set; }
        public Vector3 Location { get; set; }
    }
}
