using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class ColorSequenceProperty : Property<ColorSequence>
    {
        public ColorSequenceProperty(string name, ColorSequence value) : base(name, PropertyType.ColorSequence, value)
        {
        }
    }
}
