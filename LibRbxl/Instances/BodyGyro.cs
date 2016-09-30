namespace LibRbxl.Instances
{
    public class BodyGyro : BodyMover
    {
        public override string ClassName => "BodyGyro";

        public CFrame CFrame { get; set; }
        public float D { get; set; }
        public Vector3 MaxTorque { get; set; }
        public float P { get; set; }
    }
}
