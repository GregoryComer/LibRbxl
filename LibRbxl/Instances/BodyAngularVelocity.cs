namespace LibRbxl.Instances
{
    public class BodyAngularVelocity : BodyMover
    {
        public override string ClassName => "BodyAngularVelocity";

        public Vector3 AngularVelocity { get; set; }
        public Vector3 MaxTorque { get; set; }
        public float P { get; set; }
    }
}
