using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Color3Property : Property<Color3>
    {
        public Color3Property(string name, Color3 value) : base(name, PropertyType.Color3, value)
        {
        }
    }
}
