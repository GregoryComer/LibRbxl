using System;
using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    internal class PropertyFactory
    {
        public static Property Create(string name, PropertyType propertyType, object value)
        {
            switch (propertyType)
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
                case PropertyType.NumberSequence:
                    return new NumberSequenceProperty(name, (NumberSequence)value);
                case PropertyType.ColorSequence:
                    return new ColorSequenceProperty(name, (ColorSequence)value);
                case PropertyType.NumberRange:
                    return new NumberRangeProperty(name, (NumberRange)value);
                case PropertyType.Rectangle:
                    return new RectangleProperty(name, (Rectangle)value);
                case PropertyType.PhysicalProperties:
                    return new PhysicalPropertiesProperty(name, (PhysicalProperties)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }
    }
}