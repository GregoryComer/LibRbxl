using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RobloxSerializer
    {
        private readonly RobloxDocument _document;

        public RobloxSerializer(RobloxDocument document)
        {
            _document = document;
        }

        public PropertyCollection GetProperties(RobloxObject robloxObject)
        {
            var properties = new PropertyCollection();
            foreach (var prop in robloxObject.GetType().GetProperties())
            {
                if (CheckNoSerialize(prop))
                    continue;
                var propertyType = GetPropertyType(prop);
                var propertyName = GetPropertyName(prop);
                var propertyValue = GetPropertyValue(robloxObject, prop, propertyType);
                var robloxProperty = GetRobloxProperty(propertyValue, propertyName, propertyType);
                properties.Add(robloxProperty);
            }
            return properties;
        }

        private object GetPropertyValue(RobloxObject robloxObject, PropertyInfo propertyInfo, PropertyType propertyType)
        {
            if (propertyType != PropertyType.Referent)
                return propertyInfo.GetValue(robloxObject);
            else
                return _document.ReferentManager.GetReferent(robloxObject);
        }

        // TODO: Convert referent properties into object references. This is going to require the objects to be deserialized in a specific order.
        public void SetProperties(RobloxObject robloxObject, PropertyCollection propertyCollection)
        {
            foreach (var property in propertyCollection)
            {
                var clrProperty = GetClrPropertyForRobloxProperty(robloxObject, property);
                clrProperty.SetValue(robloxObject, property.Get());
            }
        }

        /// <summary>
        /// Looks for a matching CLR property on the given object. Properties with a matching RobloxPropertyAttribute take precedence. If no attribute match is found, a match by name is attempted. If no match is found, it returns null.
        /// </summary>
        private PropertyInfo GetClrPropertyForRobloxProperty(RobloxObject robloxObject, Property property)
        {
            var properties = robloxObject.GetType().GetProperties();

            // Look for a matching RobloxPropertyAttribute
            foreach (var propInfo in properties)
            {
                var attrs = propInfo.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
                if (!attrs.Any())
                    continue;
                if (CheckRobloxPropertyAttributeMatchesProperty(attrs.First(), property))
                    return propInfo;
            }

            // Look for a matching name
            var match = properties.FirstOrDefault(n => string.Compare(n.Name, property.Name, StringComparison.InvariantCultureIgnoreCase) == 0);

            // Either the matching property or null
            return match;
        }

        private bool CheckRobloxPropertyAttributeMatchesProperty(RobloxPropertyAttribute robloxPropertyAttribute, Property property)
        {
            return robloxPropertyAttribute.Type == property.Type;
        }

        private string GetPropertyName(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
            if (attrs.Any() && attrs.First().PropertyName != null)
                return attrs.First().PropertyName;
            return propertyInfo.Name;
        }

        private Property GetRobloxProperty(object value, string name, PropertyType type)
        {
            switch (type)
            {
                case PropertyType.String:
                    return new StringProperty(name, (string)value);
                case PropertyType.Boolean:
                    return new BoolProperty(name, (bool)value);
                case PropertyType.Int32:
                    return new Int32Property(name, (int)value);
                case PropertyType.Float:
                    return new FloatProperty(name, (float)value);
                case PropertyType.Double:
                    return new DoubleProperty(name, (double)value);
                case PropertyType.UDim2:
                    return new UDim2Property(name, (UDim2)value);
                case PropertyType.Ray:
                    return new RayProperty(name, (Ray)value);
                case PropertyType.Faces:
                    return new FacesProperty(name, (Faces)value);
                case PropertyType.Axis:
                    return new AxisProperty(name, (Axis)value);
                case PropertyType.BrickColor:
                    return new BrickColorProperty(name, (BrickColor)value);
                case PropertyType.Color3:
                    return new Color3Property(name, (Color3)value);
                case PropertyType.Vector2:
                    return new Vector2Property(name, (Vector2)value);
                case PropertyType.Vector3:
                    return new Vector3Property(name, (Vector3)value);
                case PropertyType.CFrame:
                    return new CFrameProperty(name, (CFrame)value);
                case PropertyType.Enumeration:
                    return new EnumerationProperty(name, (int)value);
                case PropertyType.Referent:
                    return new ReferentProperty(name, (int)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private PropertyType GetPropertyType(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
            if (attrs.Any() && attrs.First().Type.HasValue)
                return attrs.First().Type.Value;
            if (propertyInfo.PropertyType == typeof (Axis))
                return PropertyType.Axis;
            if (propertyInfo.PropertyType == typeof (bool))
                return PropertyType.Boolean;
            if (propertyInfo.PropertyType == typeof (BrickColor))
                return PropertyType.BrickColor;
            if (propertyInfo.PropertyType == typeof (CFrame))
                return PropertyType.CFrame;
            if (propertyInfo.PropertyType == typeof (Color3))
                return PropertyType.Color3;
            if (propertyInfo.PropertyType == typeof (double))
                return PropertyType.Double;
            if (propertyInfo.PropertyType == typeof (Faces))
                return PropertyType.Faces;
            if (propertyInfo.PropertyType == typeof (float))
                return PropertyType.Float;
            if (propertyInfo.PropertyType == typeof (Ray))
                return PropertyType.Ray;
            if (propertyInfo.PropertyType == typeof (string))
                return PropertyType.String;
            if (propertyInfo.PropertyType == typeof (UDim2))
                return PropertyType.UDim2;
            if (propertyInfo.PropertyType == typeof (Vector2))
                return PropertyType.Vector2;
            if (propertyInfo.PropertyType == typeof (Vector3))
                return PropertyType.Vector3;
            if (propertyInfo.PropertyType.IsEnum)
                return PropertyType.Enumeration;
            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(Instance)))
                return PropertyType.Referent;
            throw new ArgumentException("Property type does not match any Roblox property type.", nameof(propertyInfo));
        }

        private bool CheckNoSerialize(PropertyInfo propertyInfo)
        {
            return Attribute.IsDefined(propertyInfo, typeof (RobloxIgnoreAttribute));
        }
    }
}
