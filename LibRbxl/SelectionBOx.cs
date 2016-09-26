using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SelectionBox : PVAdornment
    {
        public override string ClassName => "SelectionBox";

        public float LineThickness { get; set; }
        public Color3 SurfaceColor3 { get; set; }
        public float SurfaceTransparency { get; set; }
    }
}
