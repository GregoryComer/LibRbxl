namespace LibRbxl.Internal
{
    public class BoolProperty : Property<bool>
    {
        public BoolProperty(string name, bool value) : base(name, PropertyType.Boolean, value)
        {
        }
    }
}
