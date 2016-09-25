using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class DoubleProperty : Property<double>
    {
        public DoubleProperty(string name, double value) : base(name, PropertyType.Double, value)
        {
        }
    }
}
