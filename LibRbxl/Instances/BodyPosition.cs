namespace LibRbxl.Instances
{
    public class BodyPosition : BodyMover
    {
        public override string ClassName => "BodyPosition";

        public float D { get; set; }
        public Vector3 MaxForce { get; set; }
        public float P { get; set; }
        public Vector3 Position { get; set; }
    }
}
