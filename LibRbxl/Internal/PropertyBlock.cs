using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    internal abstract class PropertyBlock
    {
        public string Name { get; set; }
        public PropertyType PropertyType { get; set; }
        public int TypeId { get; set; }

        protected PropertyBlock(string name, PropertyType propertyType, int typeId)
        {
            Name = name;
            PropertyType = propertyType;
            TypeId = typeId;
        }

        public static PropertyBlock Deserialize(byte[] data, TypeHeader[] typeHeaders)
        {
            var stream = new MemoryStream(data);
            var reader = new EndianAwareBinaryReader(stream);
            return Deserialize(reader, typeHeaders);
        }

        private static PropertyBlock Deserialize(EndianAwareBinaryReader reader, TypeHeader[] typeHeaders)
        {
            var typeId = reader.ReadInt32();
            var name = Util.ReadLengthPrefixedString(reader);
            var dataType = (PropertyType) reader.ReadByte();
            
            var typeHeader = typeHeaders.FirstOrDefault(n => n.TypeId == typeId);
            if (typeHeader == null)
                throw new ArgumentException("No type header matches type id specified in property block.");

            switch (dataType)
            {
                case PropertyType.String:
                {
                    var values = Util.ReadStringArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<string>(name, dataType, typeId, values);
                }
                case PropertyType.Boolean:
                {
                    var values = Util.ReadBoolArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<bool>(name, dataType, typeId, values);
                }
                case PropertyType.Int32:
                {
                    var values = Util.ReadInt32Array(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<int>(name, dataType, typeId, values);
                }
                case PropertyType.Float:
                {
                    var values = Util.ReadFloatArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<float>(name, dataType, typeId, values);
                }
                case PropertyType.Double:
                {
                    var values = Util.ReadDoubleArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<double>(name, dataType, typeId, values);
                }
                case PropertyType.UDim2:
                {
                    var values = Util.ReadUDim2Array(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<UDim2>(name, dataType, typeId, values);
                }
                case PropertyType.Ray:
                {
                    var values = Util.ReadRayArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Ray>(name, dataType, typeId, values);
                }
                case PropertyType.Faces:
                {
                    var values = Util.ReadFacesArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Faces>(name, dataType, typeId, values);
                }
                case PropertyType.Axis:
                {
                    var values = Util.ReadAxisArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Axis>(name, dataType, typeId, values);
                }
                case PropertyType.BrickColor:
                {
                    var values = Util.ReadBrickColorArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<BrickColor>(name, dataType, typeId, values);
                }
                case PropertyType.Color3:
                {
                    var values = Util.ReadColor3Array(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Color3>(name, dataType, typeId, values);
                }
                case PropertyType.Vector2:
                {
                    var values = Util.ReadVector2Array(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Vector2>(name, dataType, typeId, values);
                }
                case PropertyType.Vector3:
                {
                    var values = Util.ReadVector3Array(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<Vector3>(name, dataType, typeId, values);
                }
                case PropertyType.CFrame:
                {
                    var values = Util.ReadCFrameArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<CFrame>(name, dataType, typeId, values);
                }
                case PropertyType.Enumeration:
                {
                    var values = Util.ReadEnumerationArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<int>(name, dataType, typeId, values);
                }
                case PropertyType.Referent:
                {
                    var values = Util.ReadReferentArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<int>(name, dataType, typeId, values);
                }
                case PropertyType.NumberSequence:
                {
                    var values = Util.ReadNumberSequenceArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<NumberSequence>(name, dataType, typeId, values);
                }
                case PropertyType.ColorSequence:
                {
                    var values = Util.ReadColorSequenceArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<ColorSequence>(name, dataType, typeId, values);
                }
                case PropertyType.NumberRange:
                {
                    var values = Util.ReadNumberRangeArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<NumberRange>(name, dataType, typeId, values);
                }
                case PropertyType.PhysicalProperties:
                {
                    var values = Util.ReadPhysicalPropertiesArray(reader, typeHeader.InstanceCount);
                    return new PropertyBlock<PhysicalProperties>(name, dataType, typeId, values);
                }
                default:
                {
                    // DEBUG
                    return null;
                    //throw new InvalidRobloxFileException("Unknown property data type");
                }
            }
        }

        public abstract Property GetProperty(int index);

        public byte[] Serialize()
        {
            var stream = new MemoryStream();
            var writer = new EndianAwareBinaryWriter(stream);
            Serialize(writer);
            var buffer = new byte[stream.Length];
            Array.Copy(stream.GetBuffer(), buffer, stream.Length);
            return buffer;
        }

        private void Serialize(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32(TypeId);
            Util.WriteLengthPrefixedString(writer, Name);
            writer.WriteByte((byte)PropertyType);
            SerializeValues(writer);
        }

        protected abstract void SerializeValues(EndianAwareBinaryWriter writer);
    }

    internal class PropertyBlock<T> : PropertyBlock
    {
        public List<T> Values { get; }

        public PropertyBlock(string name, PropertyType propertyType, int typeId) : base(name, propertyType, typeId)
        {
            Values = new List<T>();
        }

        public PropertyBlock(string name, PropertyType propertyType, int typeId, int capacity) : base(name, propertyType, typeId)
        {
            Values = new List<T>(capacity);
        }

        public PropertyBlock(string name, PropertyType propertyType, int typeId, IEnumerable<T> values) : base(name, propertyType, typeId)
        {
            Values = new List<T>(values);
        }

        public override Property GetProperty(int index)
        {
            return PropertyFactory.Create(Name, PropertyType, Values[index]);
        }

        protected override void SerializeValues(EndianAwareBinaryWriter writer)
        {
            var values = Values.ToArray();
            switch (PropertyType)
            {
                case PropertyType.String:
                    var strValues = values as string[];
                    if (strValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteStringArray(writer, strValues);
                    break;
                case PropertyType.Boolean:
                    var boolValues = values as bool[];
                    if (boolValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteBoolArray(writer, boolValues);
                    break;
                case PropertyType.Int32:
                    var intValues = values as int[];
                    if (intValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteInt32Array(writer, intValues);
                    break;
                case PropertyType.Float:
                    var floatValues = values as float[];
                    if (floatValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteFloatArray(writer, floatValues);
                    break;
                case PropertyType.Double:
                    var doubleValues = values as double[];
                    if (doubleValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteDoubleArray(writer, doubleValues);
                    break;
                case PropertyType.UDim2:
                    var udim2Values = values as UDim2[];
                    if (udim2Values == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteUDim2Array(writer, udim2Values);
                    break;
                case PropertyType.Ray:
                    var rayValues = values as Ray[];
                    if (rayValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteRayArray(writer, rayValues);
                    break;
                case PropertyType.Faces:
                    var facesValues = values as Faces[];
                    if (facesValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteFacesArray(writer, facesValues);
                    break;
                case PropertyType.Axis:
                    var axisValues = values as Axis[];
                    if (axisValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteAxisArray(writer, axisValues);
                    break;
                case PropertyType.BrickColor:
                    var brickColorValues = values as BrickColor[];
                    if (brickColorValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteBrickColorArray(writer, brickColorValues);
                    break;
                case PropertyType.Color3:
                    var colorValues = values as Color3[];
                    if (colorValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteColor3Array(writer, colorValues);
                    break;
                case PropertyType.Vector2:
                    var vector2Values = values as Vector2[];
                    if (vector2Values == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteVector2Array(writer, vector2Values);
                    break;
                case PropertyType.Vector3:
                    var vector3Values = values as Vector3[];
                    if (vector3Values == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteVector3Array(writer, vector3Values);
                    break;
                case PropertyType.CFrame:
                    var cFrameValues = values as CFrame[];
                    if (cFrameValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteCFrameArray(writer, cFrameValues);
                    break;
                case PropertyType.Enumeration:
                    var enumValues = values as int[];
                    if (enumValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteEnumerationArray(writer, enumValues);
                    break;
                case PropertyType.Referent:
                    var refValues = values as int[];
                    if (refValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteReferentArray(writer, refValues);
                    break;
                case PropertyType.NumberSequence:
                    var numberSequenceValues = values as NumberSequence[];
                    if (numberSequenceValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteNumberSequenceArray(writer, numberSequenceValues);
                    break;
                case PropertyType.ColorSequence:
                    var colorSequenceValues = values as ColorSequence[];
                    if (colorSequenceValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteColorSequenceArray(writer, colorSequenceValues);
                    break;
                case PropertyType.NumberRange:
                    var numberRangeValues = values as NumberRange[];
                    if (numberRangeValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WriteNumberRangeArray(writer, numberRangeValues);
                    break;
                case PropertyType.PhysicalProperties:
                    var physicalPropertiesValues = values as PhysicalProperties[];
                    if (physicalPropertiesValues == null)
                        throw new InvalidOperationException("Property type does not match CLR data type.");
                    Util.WritePhysicalPropertiesArray(writer, physicalPropertiesValues);
                    break;
                default:
                    throw new ArgumentException("Unknown Roblox data type.");
            }
        }
    }
}
