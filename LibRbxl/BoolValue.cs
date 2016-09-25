using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BoolValue : Instance
    {
        public override string ClassName => "BoolValue";

        public bool Value { get; set; }
    }
}
