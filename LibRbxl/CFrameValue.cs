using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class CFrameValue : Instance
    {
        public override string ClassName => "CFrameValue";

        public CFrame Value { get; set; }
    }
}
