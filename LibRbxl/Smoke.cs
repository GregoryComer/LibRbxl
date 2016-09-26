using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Smoke : Instance
    {
        public override string ClassName => "Smoke";

        public Color3 Color { get; set; }
        public bool Enabled { get; set; }
        public float Opacity { get; set; }
        public float RiseVelocity { get; set; }
        public float Size { get; set; }
    }
}
