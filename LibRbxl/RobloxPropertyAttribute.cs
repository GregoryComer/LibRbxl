using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RobloxPropertyAttribute : Attribute
    {
        public RobloxPropertyAttribute(string propertyName, PropertyType type)
        {
            PropertyName = propertyName;
            Type = type;
        }

        public string PropertyName { get; }
        public PropertyType? Type { get; }
    }
}
