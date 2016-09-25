using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class FloatProperty : Property<float>
    {
        public FloatProperty(string name, float value) : base(name, PropertyType.Float, value)
        {
        }
    }
}
