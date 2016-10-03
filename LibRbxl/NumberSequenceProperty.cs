using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class NumberSequenceProperty : Property<NumberSequence>
    {
        public NumberSequenceProperty(string name, NumberSequence value) : base(name, PropertyType.NumberSequence, value)
        {
        }
    }
}
