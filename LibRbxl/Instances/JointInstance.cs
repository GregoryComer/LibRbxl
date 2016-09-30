namespace LibRbxl.Instances
{
    public abstract class JointInstance : Instance
    {
        public CFrame C0 { get; set; }
        public CFrame C1 { get; set; }
        public BasePart Part0 { get; set; }
        public BasePart Part1 { get; set; }
    }
}
