namespace LibRbxl.Instances
{
    public class HingeConstraint : Instance
    {
        public override string ClassName => "HingeConstraint";

        public ActuatorType ActuatorType { get; set; }
        public float AngularSpeed { get; set; }
        public float AngularVelocity { get; set; }
        public float CurrentAngle { get; set; }
        public bool LimitsEnabled { get; set; }
        public float LowerAngle { get; set; }
        public float MotorMaxAcceleration { get; set; }
        public float MotorMaxTorque { get; set; }
        public float Radius { get; set; }
        public float Restitution { get; set; }
        public float ServoMaxTorque { get; set; }
        public float TargetAngle { get; set; }
        public float UpperAngle { get; set; }
    }
}
