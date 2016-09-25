using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BrickColorProperty : Property<BrickColor>
    {
        public BrickColorProperty(string name, BrickColor value) : base(name, PropertyType.BrickColor, value)
        {
        }
    }
}
