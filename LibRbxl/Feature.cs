using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class Feature : Instance
    {
        public NormalId FaceId { get; set; }
        public InOut InOut { get; set; }
        public LeftRight LeftRight { get; set; }
        public TopBottom TopBottom { get; set; }
    }
}
