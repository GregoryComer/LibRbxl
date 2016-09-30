namespace LibRbxl.Instances
{
    public class BodyForce : BodyMover
    {
        public override string ClassName => "BodyForce";

        public Vector3 Force { get; set; }
    }
}
