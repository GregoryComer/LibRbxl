using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SelectionSphere : PVAdornment
    {
        public override string ClassName => "SelectionSphere";

        public Color3 SurfaceColor3 { get; set; }
        public float SurfaceTransparency { get; set; }
    }
}
