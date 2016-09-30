namespace LibRbxl.Instances
{
    public class VelocityMotor : JointInstance
    {
        public override string ClassName => "VelocityMotor";

        public float CurrentAngle { get; set; }
        public float DesiredAngle { get; set; }
        public Hole Hole { get; set; }
        public float MaxVelocity { get; set; }
    }
}
