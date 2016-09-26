using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class IntConstrainedValue : Instance
    {
        public override string ClassName => "IntConstrainedValue";

        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int Value { get; set; }
    }
}
