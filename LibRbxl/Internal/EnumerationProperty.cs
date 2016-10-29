namespace LibRbxl.Internal
{
    public class EnumerationProperty : Property<int>
    {
        public EnumerationProperty(string name, int value) : base(name, PropertyType.Enumeration, value)
        {
        }
    }
}
