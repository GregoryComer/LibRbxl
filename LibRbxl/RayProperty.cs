using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RayProperty : Property<Ray>
    {
        public RayProperty(string name, Ray value) : base(name, PropertyType.Ray, value)
        {
        }
    }
}
