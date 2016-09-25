using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class CFrameProperty : Property<CFrame>
    {
        public CFrameProperty(string name, CFrame value) : base(name, PropertyType.CFrame, value)
        {
        }
    }
}
