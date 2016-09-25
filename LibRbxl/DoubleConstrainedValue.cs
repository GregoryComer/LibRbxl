using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class DoubleConstrainedValue : Instance
    {
        public override string ClassName => "DoubleConstrainedValue";

        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Value { get; set; }
    }
}
