using System;
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
        private readonly Dictionary<Tuple<Type, string>, Property> _defaultPropertyCache = new Dictionary<Tuple<Type, string>, Property>(); // Contains default values for (PropertyType, PropertyName) pairs
        private readonly Dictionary<Type, Tuple<string, PropertyType>[]> _typePropertyCache = new Dictionary<Type, Tuple<string, PropertyType>[]>();  // Contains property (Type, Name) pairs for each instance type
        private readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _propertyMappingCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>(); // For each type, inner dictionary is a mapping between Roblox property name and CLR property

        public RobloxSerializer(RobloxDocument document)
        {
            _document = document;
        }

        public PropertyCollection GetProperties(Instance instance)
        {
            var properties = new PropertyCollection();
            foreach (var prop in instance.GetType().GetProperties())
            {
                if (CheckNoSerialize(prop))
                    continue;
                var propertyType = GetPropertyTypeAndNameFromClrProperty(prop).Item2;
                var propertyName = GetPropertyName(prop);
                var propertyValue = GetPropertyValue(instance, prop, propertyType);
                var robloxProperty = GetRobloxProperty(propertyValue, propertyName, propertyType);
                properties.Add(robloxProperty);
            }
            return properties;
        }

        public Property GetPropertyDefault(string propertyName, Type parentType, PropertyType propertyType)
        {
            var cacheKey = new Tuple<Type, string>(parentType, propertyName);
            if (_defaultPropertyCache.ContainsKey(cacheKey))
            {
                return _defaultPropertyCache[cacheKey];
            }

            var property =  GetPropertyDefaultNoCache(propertyName, parentType, propertyType);
            _defaultPropertyCache.Add(cacheKey, property);
            return property;
        }

        private static Property GetPropertyDefaultNoCache(string propertyName, Type parentType, PropertyType propertyType)
        {
            foreach (var propertyInfo in parentType.GetProperties())
            {
                if (propertyInfo.Name == propertyName)
                {
                    var attrs = propertyInfo.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
                    if (attrs.Length > 0 && attrs[0].DefaultValue != null)
                    {
                        return PropertyFactory.Create(propertyName, propertyType, attrs[0].DefaultValue);
                    }
                }
            }

            switch (propertyType)
            {
                case PropertyType.String:
                    return new StringProperty(propertyName, "");
                case PropertyType.Boolean:
                    return new BoolProperty(propertyName, false);
                case PropertyType.Int32:
                    return new Int32Property(propertyName, 0);
                case PropertyType.Float:
                    return new FloatProperty(propertyName, 0.0f);
                case PropertyType.Double:
                    return new DoubleProperty(propertyName, 0.0);
                case PropertyType.UDim2:
                    return new UDim2Property(propertyName, new UDim2(0, 0, 0, 0));
                case PropertyType.Ray:
                    return new RayProperty(propertyName, new Ray(Vector3.Zero, Vector3.Zero)); // TODO Check Roblox default
                case PropertyType.Faces:
                    return new FacesProperty(propertyName, 0); // TODO Check Roblox default
                case PropertyType.Axis:
                    return new AxisProperty(propertyName, 0); // TODO Check Roblox default
                case PropertyType.BrickColor:
                    return new BrickColorProperty(propertyName, 0); // // TODO Check Roblox default
                case PropertyType.Color3:
                    return new Color3Property(propertyName, new Color3(0, 0, 0)); // TODO Check Roblox default
                case PropertyType.Vector2:
                    return new Vector2Property(propertyName, Vector2.Zero);
                case PropertyType.Vector3:
                    return new Vector3Property(propertyName, Vector3.Zero);
                case PropertyType.CFrame:
                    return new CFrameProperty(propertyName, new CFrame(Vector3.Zero)); // // TODO Check Roblox default
                case PropertyType.Enumeration:
                    return new EnumerationProperty(propertyName, 0);
                case PropertyType.Referent:
                    return new ReferentProperty(propertyName, -1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
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
            foreach (var property in propertyCollection)
            {
                var clrProperty = GetClrPropertyForRobloxProperty(instance.GetType(), property.Name);
                if (clrProperty != null)
                {
                    if (property.Type != PropertyType.Referent)
                        clrProperty.SetValue(instance, property.Get());
                    else
                    {
                        var referent = (int) property.Get();
                        if (referent != 0)
                            // It seems like in many cases 0 means no referent? For example, gui object's next selection property. TODO look into this.
                        {
                            var inst = (referent != -1) ? document.ReferentProvider.GetCached(referent) : null;
                            clrProperty.SetValue(instance, inst);
                        }
                    }
                }
                else
                {
                    instance.UnmanagedProperties.Add(property.Name, property);
                    Trace.WriteLine($"Found unmanaged property \"{property.Name}\" of type {property.Type} on object of class {instance.ClassName}.");
                }
            }
        }

        public Dictionary<string, PropertyType> GetUniqueProperties(Instance[] instances)
        {
            var propertyDict = new Dictionary<string, PropertyType>();
            foreach (var instance in instances)
            {
                var instanceType = instance.GetType();
                if (_typePropertyCache.ContainsKey(instanceType))
                {
                    var properties = _typePropertyCache[instanceType];
                    foreach (var propertyPair in properties)
                    {
                        if (propertyDict.ContainsKey(propertyPair.Item1))
                        {
                            if (propertyDict[propertyPair.Item1] != propertyPair.Item2)
                                throw new ArgumentException($"Instance of property \"{propertyPair.Item1}\" does not match type of previously defined property.");
                        }
                        else
                        {
                            propertyDict.Add(propertyPair.Item1, propertyPair.Item2);
                        }
                    }
                }
                else
                {
                    Dictionary<string, PropertyInfo> propertyMappingCacheEntry;
                    if (_propertyMappingCache.ContainsKey(instanceType))
                        propertyMappingCacheEntry = _propertyMappingCache[instanceType];
                    else
                    {
                        propertyMappingCacheEntry = new Dictionary<string, PropertyInfo>();
                        _propertyMappingCache.Add(instanceType, propertyMappingCacheEntry);
                    }

                    foreach (var propertyInfo in instanceType.GetProperties())
                    {
                        var ignoreAttrs = propertyInfo.GetCustomAttributes<RobloxIgnoreAttribute>();
                        if (ignoreAttrs.Any())
                            continue;

                        var propertyTypeNamePair = GetPropertyTypeAndNameFromClrProperty(propertyInfo);
                        if (propertyDict.ContainsKey(propertyTypeNamePair.Item1))
                        {
                            if (propertyDict[propertyTypeNamePair.Item1] != propertyTypeNamePair.Item2)
                                throw new ArgumentException($"Instance of property \"{propertyTypeNamePair.Item1}\" does not match type of previously defined property.");
                        }
                        else
                        {
                            propertyDict.Add(propertyTypeNamePair.Item1, propertyTypeNamePair.Item2);
                        }
                        
                        if (!propertyMappingCacheEntry.ContainsKey(propertyTypeNamePair.Item1)) // Cache CLR property -> Roblox property mapping
                            propertyMappingCacheEntry.Add(propertyTypeNamePair.Item1, propertyInfo);
                    }
                }
            }
            return propertyDict;
        }

        private Tuple<string, PropertyType> GetPropertyTypeAndNameFromClrProperty(PropertyInfo propertyInfo)
        {
            PropertyType type;
            string name;

            var attrs = propertyInfo.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
            if (attrs.Length > 0 && attrs[0].Type.HasValue)
            {
                type = attrs[0].Type.Value;
            }
            else
            {
                // These are roughly ordered by frequency.
                if (propertyInfo.PropertyType == typeof (int) && !propertyInfo.PropertyType.IsEnum)
                    type = PropertyType.Int32;
                else if (propertyInfo.PropertyType == typeof (string))
                    type = PropertyType.String;
                else if (propertyInfo.PropertyType.IsEnum || propertyInfo.PropertyType == typeof (int))
                    type = PropertyType.Enumeration;
                else if (propertyInfo.PropertyType == typeof (float))
                    type = PropertyType.Float;
                else if (propertyInfo.PropertyType == typeof (bool))
                    type = PropertyType.Boolean;
                else if (typeof (Instance).IsAssignableFrom(propertyInfo.PropertyType))
                    type = PropertyType.Referent;
                else if (propertyInfo.PropertyType == typeof (Vector3))
                    type = PropertyType.Vector3;
                else if (propertyInfo.PropertyType == typeof (Color3))
                    type = PropertyType.Color3;
                else if (propertyInfo.PropertyType == typeof (UDim2))
                    type = PropertyType.UDim2;
                else if (propertyInfo.PropertyType == typeof (CFrame))
                    type = PropertyType.CFrame;
                else if (propertyInfo.PropertyType == typeof (BrickColor))
                    type = PropertyType.BrickColor;
                else if (propertyInfo.PropertyType == typeof (double))
                    type = PropertyType.Double;
                else if (propertyInfo.PropertyType == typeof (Faces))
                    type = PropertyType.Faces;
                else if (propertyInfo.PropertyType == typeof (Axis))
                    type = PropertyType.Axis;
                else if (propertyInfo.PropertyType == typeof (Vector2))
                    type = PropertyType.Vector2;
                else if (propertyInfo.PropertyType == typeof (Ray))
                    type = PropertyType.Ray;
                else
                    throw new ArgumentException("No Roblox property matches of CLR property.");
            }

            if (attrs.Length > 0 && attrs[0].PropertyName != null)
            {
                name = attrs[0].PropertyName;
            }
            else
            {
                name = propertyInfo.Name;
            }

            return new Tuple<string, PropertyType>(name, type);
        }

        private PropertyInfo GetClrPropertyForRobloxProperty(Type type, string propertyName)
        {
            foreach (var property in type.GetProperties())
            {
                var attr = property.GetCustomAttributes<RobloxPropertyAttribute>().FirstOrDefault();
                if (attr != null && attr.PropertyName == propertyName)
                {
                    return property;
                }
            }
            foreach (var property in type.GetProperties())
            {
                if (property.Name == propertyName)
                    return property;
            }
            return null;
        }

        private static string GetPropertyName(PropertyInfo propertyInfo)
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
                    return new StringProperty(name, (string) value);
                case PropertyType.Boolean:
                    return new BoolProperty(name, (bool) value);
                case PropertyType.Int32:
                    return new Int32Property(name, (int) value);
                case PropertyType.Float:
                    return new FloatProperty(name, (float) value);
                case PropertyType.Double:
                    return new DoubleProperty(name, (double) value);
                case PropertyType.UDim2:
                    return new UDim2Property(name, (UDim2) value);
                case PropertyType.Ray:
                    return new RayProperty(name, (Ray) value);
                case PropertyType.Faces:
                    return new FacesProperty(name, (Faces) value);
                case PropertyType.Axis:
                    return new AxisProperty(name, (Axis) value);
                case PropertyType.BrickColor:
                    return new BrickColorProperty(name, (BrickColor) value);
                case PropertyType.Color3:
                    return new Color3Property(name, (Color3) value);
                case PropertyType.Vector2:
                    return new Vector2Property(name, (Vector2) value);
                case PropertyType.Vector3:
                    return new Vector3Property(name, (Vector3) value);
                case PropertyType.CFrame:
                    return new CFrameProperty(name, (CFrame) value);
                case PropertyType.Enumeration:
                    return new EnumerationProperty(name, (int) value);
                case PropertyType.Referent:
                    return new ReferentProperty(name, (int) value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private bool CheckNoSerialize(PropertyInfo propertyInfo)
        {
            return Attribute.IsDefined(propertyInfo, typeof (RobloxIgnoreAttribute));
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }

        public PropertyBlock<T> FillPropertyBlock<T>(string propertyName, PropertyType propertyType, int typeId, Instance[] instances, ReferentProvider referentProvider)
        {
            var propertyBlock = new PropertyBlock<T>(propertyName, propertyType, typeId, instances.Length);
            var instanceType = instances[0].GetType();
            Dictionary<string, PropertyInfo> cache;
            if (_propertyMappingCache.ContainsKey(instanceType))
                cache = _propertyMappingCache[instanceType];
            else
            {
                cache = new Dictionary<string, PropertyInfo>();
                _propertyMappingCache.Add(instanceType, cache);
            }

            for (var i = 0; i < instances.Length; i++)
            {
                propertyBlock.Values.Add(GetPropertyValue<T>(propertyName, instances[i], instanceType, propertyType, cache, referentProvider));
            }
            return propertyBlock;
        }

        private T GetPropertyValue<T>(string propertyName, Instance instance, Type instanceType, PropertyType propertyType, Dictionary<string, PropertyInfo> cache, ReferentProvider referentProvider)
        {
            PropertyInfo property;
            if (cache.ContainsKey(propertyName))
                property = cache[propertyName];
            else
            {
                property = GetClrPropertyForRobloxProperty(instanceType, propertyName);
                cache.Add(propertyName, property);
            }

            T value;

            if (propertyType != PropertyType.Referent)
            {
                var propertyValue = property.GetValue(instance);
                if (propertyValue != null)
                    value = (T) propertyValue;
                else
                    value = (T) GetDefaultValue(property, propertyType);
            }
            else
            {
                value = (T)(object)referentProvider.GetReferent((Instance) property.GetValue(instance));
            }

            if (value != null)
                return value;

            // DEGUG
            else if (propertyType == PropertyType.String)
                return (T)(object)""; // TEMP
            else
                throw new ArgumentException();
        }

        private object GetDefaultValue(PropertyInfo property, PropertyType propertyType)
        {
            var attrs = property.GetCustomAttributes<RobloxPropertyAttribute>().ToArray();
            if (attrs.Length > 0 && attrs[0].DefaultValue != null)
            {
                return attrs[0].DefaultValue;
            }

            switch (propertyType)
            {
                case PropertyType.String:
                    return "";
                case PropertyType.Boolean:
                    return false;
                case PropertyType.Int32:
                    return 0;
                case PropertyType.Float:
                    return 0.0f;
                case PropertyType.Double:
                    return 0.0;
                case PropertyType.UDim2:
                    return new UDim2(0, 0.0f, 0, 0.0f);
                case PropertyType.Ray:
                    return new Ray(Vector3.Zero, Vector3.Zero);
                case PropertyType.Faces:
                    return 0;
                case PropertyType.Axis:
                    return 0;
                case PropertyType.BrickColor:
                    return 0;
                case PropertyType.Color3:
                    return new Color3(0, 0, 0);
                case PropertyType.Vector2:
                    return Vector2.Zero;
                case PropertyType.Vector3:
                    return Vector3.Zero;
                case PropertyType.CFrame:
                    return new CFrame(Vector3.Zero, Matrix3.Identity);
                case PropertyType.Enumeration:
                    return 0;
                case PropertyType.Referent:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }
    }
}
