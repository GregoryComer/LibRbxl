using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class EnumerationProperty : Property<int>
    {
        public EnumerationProperty(string name, int value) : base(name, PropertyType.Enumeration, value)
        {
        }
    }
}
