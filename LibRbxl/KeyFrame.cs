using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class KeyFrame : Instance
    {
        public override string ClassName => "KeyFrame";

        public float Time { get; set; }
    }
}
