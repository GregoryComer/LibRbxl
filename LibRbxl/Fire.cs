using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Fire : Instance
    {
        public override string ClassName => "Fire";

        public Color3 Color { get; set; }
        public bool Enabled { get; set; }
        [RobloxProperty("heat_xml", PropertyType.Float)]
        public float Heat { get; set; }
        public Color3 SecondaryColor { get; set; }
        [RobloxProperty("size_xml", PropertyType.Float)]
        public float Size { get; set; }
    }
}
