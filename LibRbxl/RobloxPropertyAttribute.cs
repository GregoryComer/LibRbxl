using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RobloxPropertyAttribute : Attribute
    {
        public RobloxPropertyAttribute(string propertyName, PropertyType type)
        {
            PropertyName = propertyName;
            Type = type;
        }

        public object DefaultValue { get; set; }
        public string PropertyName { get; }
        public PropertyType? Type { get; }
    }
}
