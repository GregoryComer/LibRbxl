using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class BrickColorProperty : Property<BrickColor>
    {
        public BrickColorProperty(string name, BrickColor value) : base(name, PropertyType.BrickColor, value)
        {
        }
    }
}
