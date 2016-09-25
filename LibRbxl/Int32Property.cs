using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Int32Property : Property<int>
    {
        public Int32Property(string name, int value) : base(name, PropertyType.Int32, value)
        {
        }
    }
}
