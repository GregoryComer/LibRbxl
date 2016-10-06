using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using LibRbxl.Internal;

namespace LibRbxl
{
    internal class RobloxSerializer
    {
        private readonly RobloxDocument _document;

        public RobloxSerializer(RobloxDocument document)
        {
            _document = document;
        }

        public PropertyCollection GetProperties(Instance instance)
        { 
            var properties = new PropertyCollection();

            foreach (var propertyPairs in ReflectionMappingManager.PropertyCache[instance.GetType()])
            {
                var value = GetPropertyValue(instance, propertyPairs.Value.Item1, propertyPairs.Value.Item2);
                var robloxProperty = PropertyFactory.Create(propertyPairs.Key, propertyPairs.Value.Item2, value);
                properties.Add(robloxProperty);
            }

            return properties;
        }

        private object GetPropertyValue(Instance instance, PropertyInfo propertyInfo, PropertyType propertyType)
        {
            if (propertyType != PropertyType.Referent)
                return propertyInfo.GetValue(instance);
            else
                return _document.ReferentProvider.GetReferent(instance);
        }

        public void SetProperties(RobloxDocument document, Instance instance, PropertyCollection propertyCollection)
        {
            var instanceType = instance.GetType();
            foreach (var property in propertyCollection)
            {
                if (ReflectionMappingManager.HasMappedClrProperty(instanceType, property.Name))
                {
                    var mappedPropertyTuple = ReflectionMappingManager.GetMappedClrProperty(instanceType, property.Name);
                    if (mappedPropertyTuple.Item2 != PropertyType.Referent)
                        mappedPropertyTuple.Item1.SetValue(instance, property.Get());
                    else
                    {
                        var referent = (int)property.Get();
                        if (referent != 0)
                        // It seems like in many cases 0 means no referent? For example, gui object's next selection property. TODO look into this.
                        {
                            var inst = (referent != -1) ? document.ReferentProvider.GetCached(referent) : null;
                            mappedPropertyTuple.Item1.SetValue(instance, inst);
                        }
                    }
                }
                else
                {
                    instance.UnmanagedProperties.Add(property.Name, property);
                }
            }
        }
        
        public PropertyBlock FillPropertyBlock(string propertyName, PropertyType propertyType, int typeId, Instance[] instances, ReferentProvider referentProvider)
        {
            switch (propertyType)
            {
                case PropertyType.String:
                    return FillPropertyBlock<string>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Boolean:
                    return FillPropertyBlock<bool>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Int32:
                    return FillPropertyBlock<int>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Float:
                    return FillPropertyBlock<float>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Double:
                    return FillPropertyBlock<double>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.UDim2:
                    return FillPropertyBlock<UDim2>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Ray:
                    return FillPropertyBlock<Ray>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Faces:
                    return FillPropertyBlock<Faces>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Axis:
                    return FillPropertyBlock<Axis>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.BrickColor:
                    return FillPropertyBlock<BrickColor>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Color3:
                    return FillPropertyBlock<Color3>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Vector2:
                    return FillPropertyBlock<Vector2>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Vector3:
                    return FillPropertyBlock<Vector3>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.CFrame:
                    return FillPropertyBlock<CFrame>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Enumeration:
                    return FillPropertyBlock<int>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Referent:
                    return FillPropertyBlock<int>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.NumberSequence:
                    return FillPropertyBlock<NumberSequence>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.ColorSequence:
                    return FillPropertyBlock<ColorSequence>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.NumberRange:
                    return FillPropertyBlock<NumberRange>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.Rectangle:
                    return FillPropertyBlock<Rectangle>(propertyName, propertyType, typeId, instances, referentProvider);
                case PropertyType.PhysicalProperties:
                    return FillPropertyBlock<PhysicalProperties>(propertyName, propertyType, typeId, instances, referentProvider);
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }

        public PropertyBlock<T> FillPropertyBlock<T>(string propertyName, PropertyType propertyType, int typeId, Instance[] instances, ReferentProvider referentProvider)
        {
            var propertyBlock = new PropertyBlock<T>(propertyName, propertyType, typeId, instances.Length);
            var instanceType = instances[0].GetType();
            for (var i = 0; i < instances.Length; i++)
            {
                propertyBlock.Values.Add(GetPropertyValue<T>(propertyName, instances[i], instanceType, propertyType, referentProvider));
            }
            return propertyBlock;
        }

        private T GetPropertyValue<T>(string propertyName, Instance instance, Type instanceType, PropertyType propertyType, ReferentProvider referentProvider)
        {
            if (ReflectionMappingManager.HasMappedClrProperty(instanceType, propertyName))
            {
                var propertyTuple = ReflectionMappingManager.PropertyCache[instanceType][propertyName];

                if (propertyType != PropertyType.Referent)
                {
                    var propertyValue = propertyTuple.Item1.GetValue(instance);
                    if (propertyValue != null)
                        return (T) propertyValue;
                    else
                        return (T) ReflectionMappingManager.GetDefaultValue(instanceType, propertyName, propertyType);
                }
                else
                {
                    var referentObj = propertyTuple.Item1.GetValue(instance);
                    if (referentObj != null)
                        return
                            (T) (object) referentProvider.GetReferent((Instance) propertyTuple.Item1.GetValue(instance));
                    else
                        return (T) ReflectionMappingManager.GetDefaultValueForType(PropertyType.Referent);
                }
            }
            else if (instance.UnmanagedProperties.ContainsKey(propertyName))
            {
                var objValue =  instance.UnmanagedProperties[propertyName].Get();
                if (objValue != null)
                    return (T) objValue;
                else
                    return (T) ReflectionMappingManager.GetDefaultValueForType(propertyType);
            }
            else
            {
                return (T)ReflectionMappingManager.GetDefaultValueForType(propertyType);
            }
        }

        public IEnumerable<PropertyNameTypePair> GetUniqueProperties(Instance[] instances)
        {
            var instanceType = instances[0].GetType();
            var properties = new HashSet<PropertyNameTypePair>();
            // TODO If the instance type isn't cached, add it. This could occur if the user overrides an Instance type to support a type that's not in the library
            var cachedTypes = ReflectionMappingManager.PropertyCache[instanceType];
            foreach (var cachedTypePair in cachedTypes)
            {
                properties.Add(new PropertyNameTypePair(cachedTypePair.Key, cachedTypePair.Value.Item2));
            }
            foreach (var instance in instances)
            {
                if (instance.UnmanagedProperties != null && instance.UnmanagedProperties.Any())
                {
                    foreach (var unmanagedPropertyPair in instance.UnmanagedProperties)
                    {
                        properties.Add(new PropertyNameTypePair(unmanagedPropertyPair.Key,
                            unmanagedPropertyPair.Value.Type));
                    }
                }
            }
            return properties.OrderBy(n => n.Name, StringComparer.Ordinal);
        }

        public class PropertyNameTypePair : IEquatable<PropertyNameTypePair>
        {
            public string Name { get; }
            public PropertyType Type { get; }

            public PropertyNameTypePair(string name, PropertyType type)
            {
                Name = name;
                Type = type;
            }

            public bool Equals(PropertyNameTypePair other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Name, other.Name) && Type == other.Type;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((PropertyNameTypePair) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (int) Type;
                }
            }

            public static bool operator ==(PropertyNameTypePair left, PropertyNameTypePair right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PropertyNameTypePair left, PropertyNameTypePair right)
            {
                return !Equals(left, right);
            }
        }
    }
}
