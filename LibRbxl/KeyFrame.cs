using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Keyframe : Instance
    {
        public override string ClassName => "Keyframe";

        public float Time { get; set; }
    }
}
