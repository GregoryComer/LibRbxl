using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class Light : Instance
    {
        public float Brightness { get; set; }
        public Color3 Color { get; set; }
        public bool Enabled { get; set; }
        public bool Shadows { get; set; }
    }
}
