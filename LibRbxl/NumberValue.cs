using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class NumberValue : Instance
    {
        public override string ClassName => "NumberValue";

        public double Value { get; set; }
    }
}
