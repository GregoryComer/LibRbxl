namespace LibRbxl.Instances
{
    public class RodConstraint : Constraint
    {
        public override string ClassName => "RodConstraint";

        [RobloxIgnore]
        public float CurrentDistance { get; set; }
        public float Length { get; set; }
        public float Thickness { get; set; }
    }
}
