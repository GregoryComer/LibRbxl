﻿namespace LibRbxl.Instances
{
    public class RopeConstraint : Constraint
    {
        public override string ClassName => "RopeConstraint";

        [RobloxIgnore]
        public float CurrentDistance { get; set; }
        public float Length { get; set; }
        public float Restitution { get; set; }
        public float Thickness { get; set; }
    }
}
