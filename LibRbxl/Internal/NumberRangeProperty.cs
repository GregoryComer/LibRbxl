using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class NumberRangeProperty : Property<NumberRange>
    {
        public NumberRangeProperty(string name, NumberRange value) : base(name, PropertyType.NumberRange, value)
        {
        }
    }
}
