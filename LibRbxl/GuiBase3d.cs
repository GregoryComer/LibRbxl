using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class GuiBase3d : GuiBase
    {
        public Color3 Color3 { get; set; }
        public float Transparency { get; set; }
        public bool Visible { get; set; }
    }
}
