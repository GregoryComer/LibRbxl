namespace LibRbxl.Instances
{
    public class SlidingBallConstraint : Constraint
    {
        public override string ClassName => "SlidingBallConstraint";

        public ActuatorType ActuatorType { get; set; }
        public float CurrentPosition { get; set; }
        public bool LimitsEnabled { get; set; }
        public float LowerLimit { get; set; }
        public float MotorMaxAcceleration { get; set; }
        public float MotorMaxForce { get; set; }
        public float Restitution { get; set; }
        public float ServoMaxForce { get; set; }
        public float Size { get; set; }
        public float Speed { get; set; }
        public float TargetPosition { get; set; }
        public float UpperLimit { get; set; }
        public float Velocity { get; set; }
    }
}