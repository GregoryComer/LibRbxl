using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class Vector3Property : Property<Vector3>
    {
        public Vector3Property(string name, Vector3 value) : base(name, PropertyType.Vector3, value)
        {
        }
    }
}
