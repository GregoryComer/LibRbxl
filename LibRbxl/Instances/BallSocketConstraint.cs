namespace LibRbxl.Instances
{
    public class BallSocketConstraint : Constraint
    {
        public override string ClassName => "BallSocketConstraint";

        public bool LimitsEnabled { get; set; }
        public float Radius { get; set; }
        public float Restitution { get; set; }
        public float UpperAngle { get; set; }
    }
}
