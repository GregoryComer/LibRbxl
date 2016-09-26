using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct NumberRange
    {
        public double Min { get; }
        public double Max { get; }

        public NumberRange(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public NumberRange(double value) : this(value, value)
        {
            
        }
    }
}
