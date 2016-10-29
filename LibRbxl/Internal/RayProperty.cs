using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class RayProperty : Property<Ray>
    {
        public RayProperty(string name, Ray value) : base(name, PropertyType.Ray, value)
        {
        }
    }
}
