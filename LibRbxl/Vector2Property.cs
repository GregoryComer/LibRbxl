using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Vector2Property : Property<Vector2>
    {
        public Vector2Property(string name, Vector2 value) : base(name, PropertyType.Vector2, value)
        {
        }
    }
}
