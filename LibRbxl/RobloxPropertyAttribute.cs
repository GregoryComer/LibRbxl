using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class RobloxPropertyAttribute : Attribute
    {
        public RobloxPropertyAttribute(string propertyName, PropertyType type, object defaultValue)
        {
            DefaultValue = defaultValue,
            PropertyName = propertyName;
            Type = type;
        }

        public object DefaultValue { get; set; }
        public string PropertyName { get; }
        public PropertyType? Type { get; }
    }
}
