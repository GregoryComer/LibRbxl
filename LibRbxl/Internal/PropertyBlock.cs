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

        public PropertyBlock(string name, PropertyType propertyType, int typeId)
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
                case PropertyType.CFrame: // TODO
                {
                    //DEBUG
                    for (var i = 0; i < typeHeader.InstanceCount; i++)
                    {
                        var isShort = reader.ReadByte() != 0;
                        if (!isShort)
                            reader.ReadBytes(9*sizeof (float));
                    }
                    for (var i = 0; i < typeHeader.InstanceCount; i++)
                    {
                        reader.ReadBytes(3 * sizeof(float));
                    }
                    return new PropertyBlock<CFrame>(name, dataType, typeId, new CFrame[typeHeader.InstanceCount]);
                    //throw new NotImplementedException();
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
                default:
                {
                    // DEBUG
                    return null;
                    //throw new InvalidRobloxFileException("Unknown property data type");
                }
            }
        }

        public abstract Property GetProperty(int index);
    }

    internal class PropertyBlock<T> : PropertyBlock
    {
        public T[] Values { get; set; }

        public PropertyBlock(string name, PropertyType propertyType, int typeId, T[] values) : base(name, propertyType, typeId)
        {
            Values = values;
        }

        public override Property GetProperty(int index)
        {
            return PropertyFactory.Create(Name, PropertyType, Values[index]);
        }
    }
}
