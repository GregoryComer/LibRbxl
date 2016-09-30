namespace LibRbxl.Instances
{
    public class UnmanagedInstance : Instance
    {
        public override string ClassName { get; }

        public UnmanagedInstance(string className)
        {
            ClassName = className;
        }
    }
}
