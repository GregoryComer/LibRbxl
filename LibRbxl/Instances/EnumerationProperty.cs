namespace LibRbxl.Instances
{
    public class EnumerationProperty : Property<int>
    {
        public EnumerationProperty(string name, int value) : base(name, PropertyType.Enumeration, value)
        {
        }
    }
}
