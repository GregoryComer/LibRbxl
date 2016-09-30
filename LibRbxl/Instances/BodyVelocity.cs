namespace LibRbxl.Instances
{
    public class BodyVelocity : BodyMover
    {
        public override string ClassName => "BodyVelocity";

        public Vector3 MaxForce { get; set; }
        public float P { get; set; }
        public Vector3 Velocity { get; set; }
    }
}
