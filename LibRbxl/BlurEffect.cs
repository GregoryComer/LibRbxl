using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BlurEffect : PostEffect
    {
        public override string ClassName => "BlurEffect";

        public float Size { get; set; }
    }
}
