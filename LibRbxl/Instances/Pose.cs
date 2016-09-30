namespace LibRbxl.Instances
{
    public class Pose : Instance
    {
        public override string ClassName => "Pose";

        public CFrame CFrame { get; set; }
        public PoseEasingDirection EasingDirection { get; set; }
        public PoseEasingStyle EasingStyle { get; set; }
        [RobloxProperty("MaskWeight", PropertyType.Float)]
        public float Weight { get; set; }
    }
}
