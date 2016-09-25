using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class AxisProperty : Property<Axis>
    {
        public AxisProperty(string name, Axis value) : base(name, PropertyType.Axis, value)
        {
        }
    }
}
