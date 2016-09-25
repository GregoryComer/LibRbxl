using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BloomEffect : PostEffect
    {
        public override string ClassName => "BloomEffect";

        public float Intensity { get; set; }
        public float Size { get; set; }
        public float Threshold { get; set; }
    }
}
