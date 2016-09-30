namespace LibRbxl.Instances
{
    public class Motor : JointInstance
    {
        public override string ClassName => "Motor";

        public float CurrentAngle { get; set; }
        public float DesiredAngle { get; set; }
        public float MaxVelocity { get; set; }
    }
}
