using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ArcHandles : PartAdornment
    {
        public override string ClassName => "ArcHandles";

        public Axis Axis { get; set; }
    }
}
