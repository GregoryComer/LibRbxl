namespace LibRbxl.Instances
{
    public class ObjectValue : Instance
    {
        public override string ClassName => "ObjectValue";

        public Instance Value { get; set; }
    }
}
