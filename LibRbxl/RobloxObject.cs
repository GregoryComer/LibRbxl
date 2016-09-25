using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class RobloxObject
    {
        protected RobloxObject()
        {
            
        }
        
        /*protected T GetPropertyOrDefault<T>(PropertyCollection properties, string name, T defaultValue)
        {
            if (properties.Contains(name))
            {
                var prop = properties[name];
                var typedProp = prop as Property<T>;
                return typedProp != null ? typedProp.Value : defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        protected void SetProperty<T>(PropertyCollection properties, string name, Property<T> property)
        {
            if (properties.Contains(name))
            {
                var prop = properties[name];
                var typedProp = prop as Property<T>;
                if (typedProp == null)
                    throw new InvalidOperationException("Property already exists but does not match expected type.");
                typedProp.Value = property.Value;
            }
            else
            {
                properties.Add(property);
            }
        }*/
    }
}
