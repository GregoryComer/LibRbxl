namespace LibRbxl.Instances
{
    public class IntConstrainedValue : Instance
    {
        public override string ClassName => "IntConstrainedValue";

        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int Value { get; set; }
    }
}
