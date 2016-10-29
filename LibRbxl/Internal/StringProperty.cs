namespace LibRbxl.Internal
{
    public class StringProperty : Property<string>
    {
        public StringProperty(string name, string value) : base(name, PropertyType.String, value)
        {
        }
    }
}
