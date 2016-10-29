using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class CFrameProperty : Property<CFrame>
    {
        public CFrameProperty(string name, CFrame value) : base(name, PropertyType.CFrame, value)
        {
        }
    }
}
