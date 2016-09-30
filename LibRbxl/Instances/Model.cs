namespace LibRbxl.Instances
{
    public class Model : PVInstance
    {
        public override string ClassName => "Model";

        public CFrame ModelInPrimary { get; set; }
        public BasePart PrimaryPart { get; set; }
    }
}
