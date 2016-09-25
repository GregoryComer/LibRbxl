using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BoolProperty : Property<bool>
    {
        public BoolProperty(string name, bool value) : base(name, PropertyType.Boolean, value)
        {
        }
    }
}
