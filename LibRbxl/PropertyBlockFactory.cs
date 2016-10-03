using System;
using LibRbxl.Instances;
using LibRbxl.Internal;

namespace LibRbxl
{
    internal class PropertyBlockFactory
    {
        internal static PropertyBlock Create(string name, PropertyType type, int typeId, int capacity)
        {
            switch (type)
            {
                case PropertyType.String:
                    return new PropertyBlock<string>(name, type, typeId, capacity);
                case PropertyType.Boolean:
                    return new PropertyBlock<bool>(name, type, typeId, capacity);
                case PropertyType.Int32:
                    return new PropertyBlock<int>(name, type, typeId, capacity);
                case PropertyType.Float:
                    return new PropertyBlock<float>(name, type, typeId, capacity);
                case PropertyType.Double:
                    return new PropertyBlock<double>(name, type, typeId, capacity);
                case PropertyType.UDim2:
                    return new PropertyBlock<UDim2>(name, type, typeId, capacity);
                case PropertyType.Ray:
                    return new PropertyBlock<Ray>(name, type, typeId, capacity);
                case PropertyType.Faces:
                    return new PropertyBlock<Faces>(name, type, typeId, capacity);
                case PropertyType.Axis:
                    return new PropertyBlock<Axis>(name, type, typeId, capacity);
                case PropertyType.BrickColor:
                    return new PropertyBlock<BrickColor>(name, type, typeId, capacity);
                case PropertyType.Color3:
                    return new PropertyBlock<Color3>(name, type, typeId, capacity);
                case PropertyType.Vector2:
                    return new PropertyBlock<Vector2>(name, type, typeId, capacity);
                case PropertyType.Vector3:
                    return new PropertyBlock<Vector3>(name, type, typeId, capacity);
                case PropertyType.CFrame:
                    return new PropertyBlock<CFrame>(name, type, typeId, capacity);
                case PropertyType.Enumeration:
                    return new PropertyBlock<int>(name, type, typeId, capacity);
                case PropertyType.Referent:
                    return new PropertyBlock<int>(name, type, typeId, capacity);
                case PropertyType.NumberSequence:
                    return new PropertyBlock<NumberSequence>(name, type, typeId, capacity);
                case PropertyType.ColorSequence:
                    return new PropertyBlock<ColorSequence>(name, type, typeId, capacity);
                case PropertyType.NumberRange:
                    return new PropertyBlock<NumberRange>(name, type, typeId, capacity);
                case PropertyType.Rectangle:
                    return new PropertyBlock<Rectangle>(name, type, typeId, capacity);
                case PropertyType.PhysicalProperties:
                    return new PropertyBlock<PhysicalProperties>(name, type, typeId, capacity);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}