using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class PhysicalPropertiesProperty : Property<PhysicalProperties>
    {
        public PhysicalPropertiesProperty(string name, PhysicalProperties value) : base(name, PropertyType.PhysicalProperties, value)
        {
        }
    }
}
