using LibRbxl.Instances;

namespace LibRbxl
{
    public class ReferentProperty : Property<int>
    {
        public ReferentProperty(string name, int value) : base(name, PropertyType.Referent, value)
        {
        }
    }
}
