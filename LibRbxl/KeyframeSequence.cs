using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class KeyframeSequence : Instance
    {
        public override string ClassName => "KeyframeSequence";

        public bool Loop { get; set; }
        public AnimationPriority Priority { get; set; }
    }
}
