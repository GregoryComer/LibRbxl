namespace LibRbxl.Instances
{
    public class UnmanagedInstance : Instance
    {
        public override string ClassName { get; }

        public bool IsService { get; set; }

        public UnmanagedInstance(string className)
        {
            ClassName = className;
        }
    }
}
