namespace LibRbxl.Instances
{
    public class Humanoid : Instance, ICameraSubject
    {
        public override string ClassName => "Humanoid";

        public bool AutoJumpEnabled { get; set; }
        public bool AutoRotate { get; set; }
        public Vector3 CameraOffset { get; set; }
        public HumanoidDisplayDistanceType DisplayDistanceType { get; set; }
        [RobloxProperty("Health_xml", PropertyType.Float)]
        public float Health { get; set;}
        public float HealthDisplayDistance { get; set; }
        public float HipHeight { get; set; }
        public bool Jump { get; set; }
        public float JumpPower { get; set; }
        public BasePart LeftLeg { get; set; }
        public float MaxHealth { get; set; }
        public float MaxSlopeAngle { get; set; }
        public Vector3 MoveDirection { get; set; }
        public float NameDisplayDistance { get; set;}
        public NameOcclusion NameOcclusion { get; set; }
        public bool PlatformStand { get; set; }
        public HumanoidRigType RigType { get; set; }
        public BasePart RightLeg { get; set; }
        public SeatBase SeatPart { get; set; }
        public bool Sit { get; set; }
        public Vector3 TargetPoint { get; set; }
        public BasePart Torso { get; set; }
        public float WalkSpeed { get; set; }
        public BasePart WalkToPart { get; set; }
        public Vector3 WalkToPoint { get; set; }
    }

    public enum HumanoidDisplayDistanceType
    {
        Viewer = 0,
        Subject = 1,
        None = 2
    }
}
