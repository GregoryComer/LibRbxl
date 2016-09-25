using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Animation : Instance
    {
        public override string ClassName => "Animation";

        public string AnimationId { get; set; }
    }
}
