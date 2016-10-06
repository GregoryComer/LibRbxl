namespace LibRbxl.Instances
{
    public class UnmanagedInstance : Instance
    {
        public override string ClassName { get; }

        [RobloxIgnore]
        public bool IsSingleton { get; set; }

        public UnmanagedInstance(string className)
        {
            ClassName = className;
        }
    }
}
