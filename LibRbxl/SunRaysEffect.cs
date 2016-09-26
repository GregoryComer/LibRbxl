using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SunRaysEffect : PostEffect
    {
        public override string ClassName => "SunRaysEffect";

        public float Intensity { get; set; }
        public float Spread { get; set; }
    }
}
