namespace LibRbxl.Instances
{
    public class DoubleConstrainedValue : Instance
    {
        public override string ClassName => "DoubleConstrainedValue";

        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Value { get; set; }
    }
}
