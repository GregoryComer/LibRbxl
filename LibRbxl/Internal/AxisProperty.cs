using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class AxisProperty : Property<Axis>
    {
        public AxisProperty(string name, Axis value) : base(name, PropertyType.Axis, value)
        {
        }
    }
}
