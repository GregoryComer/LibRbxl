using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ColorCorrectionEffect : PostEffect
    {
        public override string ClassName => "ColorCorrectionEffect";

        public float Brightness { get; set; }
        public float Contrast { get; set; }
        public float Saturation { get; set; }
        public Color3 TintColor { get; set; }
    }
}
