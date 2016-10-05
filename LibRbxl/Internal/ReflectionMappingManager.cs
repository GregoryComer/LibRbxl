using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
// ReSharper disable InconsistentNaming

namespace LibRbxl.Internal
{
    internal static class ReflectionMappingManager
    {
        private static readonly Dictionary<Type, Dictionary<string, object>> _defaultValueCache = new Dictionary<Type, Dictionary<string, object>>();
        private static readonly Dictionary<Type, Dictionary<string, Tuple<PropertyInfo, PropertyType>>> _propertyCache = new Dictionary<Type, Dictionary<string, Tuple<PropertyInfo, PropertyType>>>();
        private static readonly Dictionary<Type, Dictionary<PropertyInfo, Tuple<PropertyType, string>>> _reversePropertyCache = new Dictionary<Type, Dictionary<PropertyInfo, Tuple<PropertyType, string>>>();
        private static readonly Dictionary<Type, string> _reverseTypeCache = new Dictionary<Type, string>();
        private static readonly Dictionary<string, Type> _typeCache = new Dictionary<string, Type>();

        public static IReadOnlyDictionary<Type, Dictionary<string, object>> DefaultValueCache => _defaultValueCache;
        public static IReadOnlyDictionary<Type, Dictionary<string, Tuple<PropertyInfo, PropertyType>>> PropertyCache => _propertyCache;
        private static IReadOnlyDictionary<Type, Dictionary<PropertyInfo, Tuple<PropertyType, string>>> ReversePropertyCache => _reversePropertyCache;
        public static IReadOnlyDictionary<Type, string> ReverseTypeCache => _reverseTypeCache;
        public static IReadOnlyDictionary<string, Type> TypeCache => _typeCache;

        public static object GetDefaultValue(Type type, string propertyName, PropertyType propertyType)
        {
            try
            {
                return DefaultValueCache[type][propertyName];
            }
            catch (KeyNotFoundException)
            {
                if (DefaultValueCache.ContainsKey(type))
                    _defaultValueCache.Add(type, new Dictionary<string, object>());

                var defaultValue = GetDefaultValueForType(propertyType);
                _defaultValueCache[type][propertyName] = defaultValue;
                return defaultValue;
            }
        }

        public static bool HasMappedClrProperty(Type type, string propertyName)
        {
            return PropertyCache.ContainsKey(type) && PropertyCache[type].ContainsKey(propertyName);
        }

        public static Tuple<PropertyInfo, PropertyType> GetMappedClrProperty(Type type, string propertyName)
        {
            try
            {
                return PropertyCache[type][propertyName];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("No matching CLR property for property.");
            }
        }

        static ReflectionMappingManager()
        {
            BuildTypeCache();
            BuildPropertyCache();
        }

        private static void BuildPropertyCache()
        {
            foreach (var typePair in _typeCache)
            {
                var propertyCacheEntry = new Dictionary<string, Tuple<PropertyInfo, PropertyType>>();
                var reversePropertyCacheEntry = new Dictionary<PropertyInfo, Tuple<PropertyType, string>>();
                var defaultValueCacheEntry = new Dictionary<string, object>();

                _propertyCache.Add(typePair.Value, propertyCacheEntry);
                _reversePropertyCache.Add(typePair.Value, reversePropertyCacheEntry);
                _defaultValueCache.Add(typePair.Value, defaultValueCacheEntry);
                
                foreach (var property in typePair.Value.GetProperties())
                {
                    var ignoreAttrs = property.GetCustomAttributes<RobloxIgnoreAttribute>();
                    if (ignoreAttrs.Any())
                        continue;

                    var nameTypeTuple = GetRobloxPropertyNameAndType(property);
                    propertyCacheEntry.Add(nameTypeTuple.Item1, new Tuple<PropertyInfo, PropertyType>(property, nameTypeTuple.Item2));
                    reversePropertyCacheEntry.Add(property, new Tuple<PropertyType, string>(nameTypeTuple.Item2, nameTypeTuple.Item1));

                    var defaultValue = GetDefaultValueInternal(property, nameTypeTuple.Item2);
                    defaultValueCacheEntry.Add(nameTypeTuple.Item1, defaultValue);
                }
            }
        }

        private static object GetDefaultValueInternal(PropertyInfo property, PropertyType propertyType)
        {
            var propertyAttr = property.GetCustomAttributes<RobloxPropertyAttribute>().FirstOrDefault();
            if (propertyAttr?.DefaultValue != null)
                return propertyAttr.DefaultValue;

            return GetDefaultValueForType(propertyType);
        }

        public static object GetDefaultValueForType(PropertyType type)
        {
            switch (type)
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
                    return new UDim2(0, 0, 0, 0);
                case PropertyType.Ray:
                    return new Ray(Vector3.Zero, Vector3.Zero); // TODO Check Roblox default
                case PropertyType.Faces:
                    return 0; // TODO Check Roblox default
                case PropertyType.Axis:
                    return 0; // TODO Check Roblox default
                case PropertyType.BrickColor:
                    return 0; // // TODO Check Roblox default
                case PropertyType.Color3:
                    return new Color3(0, 0, 0); // TODO Check Roblox default
                case PropertyType.Vector2:
                    return Vector2.Zero;
                case PropertyType.Vector3:
                    return Vector3.Zero;
                case PropertyType.CFrame:
                    return new CFrame(Vector3.Zero); // // TODO Check Roblox default
                case PropertyType.Enumeration:
                    return 0;
                case PropertyType.Referent:
                    return -1;
                case PropertyType.NumberSequence: // TODO Check Roblox default
                    return new NumberSequence(0.0f);
                case PropertyType.ColorSequence: // TODO Check Roblox default
                    return new ColorSequence(new Color3(1, 1, 1));
                case PropertyType.NumberRange: // TODO Check Roblox default
                    return new NumberRange(0.0f);
                case PropertyType.Rectangle: // TODO Check Roblox default
                    return new Rectangle(0, 0, 0, 0);
                case PropertyType.PhysicalProperties: // TODO Check Roblox default
                    return new PhysicalProperties(false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static void BuildTypeCache()
        {
            var assemblyTypes = Assembly.GetAssembly(typeof(Instance)).GetTypes();
            foreach (var type in assemblyTypes.Where(type => typeof(Instance).IsAssignableFrom(type)))
            {
                var robloxTypeName = GetRobloxTypeName(type);
                _typeCache.Add(robloxTypeName, type);
                _reverseTypeCache.Add(type, robloxTypeName);
            }
        }

        private static Tuple<string, PropertyType> GetRobloxPropertyNameAndType(PropertyInfo propertyInfo)
        {
            var robloxPropertyAttrs = propertyInfo.GetCustomAttributes<RobloxPropertyAttribute>();
            var attr = robloxPropertyAttrs.FirstOrDefault();
            var propertyName = attr?.PropertyName ?? propertyInfo.Name;
            var propertyType = attr?.Type ?? GetPropertyType(propertyInfo.PropertyType);
            return new Tuple<string, PropertyType>(propertyName, propertyType);
        }

        private static PropertyType GetPropertyType(Type propertyType)
        {
            if (propertyType == typeof(int) && !propertyType.IsEnum)
                return PropertyType.Int32;
            else if (propertyType == typeof(string))
                return PropertyType.String;
            else if (propertyType.IsEnum || propertyType == typeof(int))
                return PropertyType.Enumeration;
            else if (propertyType == typeof(float))
                return PropertyType.Float;
            else if (propertyType == typeof(bool))
                return PropertyType.Boolean;
            else if (typeof(IInstance).IsAssignableFrom(propertyType))
                return PropertyType.Referent;
            else if (propertyType == typeof(Vector3))
                return PropertyType.Vector3;
            else if (propertyType == typeof(Color3))
                return PropertyType.Color3;
            else if (propertyType == typeof(UDim2))
                return PropertyType.UDim2;
            else if (propertyType == typeof(CFrame))
                return PropertyType.CFrame;
            else if (propertyType == typeof(BrickColor))
                return PropertyType.BrickColor;
            else if (propertyType == typeof(double))
                return PropertyType.Double;
            else if (propertyType == typeof(Faces))
                return PropertyType.Faces;
            else if (propertyType == typeof(Axis))
                return PropertyType.Axis;
            else if (propertyType == typeof(Vector2))
                return PropertyType.Vector2;
            else if (propertyType == typeof (Ray))
                return PropertyType.Ray;
            else if (propertyType == typeof(Rectangle))
                return PropertyType.Rectangle;
            else if (propertyType == typeof(NumberSequence))
                return PropertyType.NumberSequence;
            else if (propertyType == typeof(ColorSequence))
                return PropertyType.ColorSequence;
            else if (propertyType == typeof(NumberRange))
                return PropertyType.NumberRange;
            else if (propertyType == typeof (PhysicalProperties))
                return PropertyType.PhysicalProperties;
            else
                throw new ArgumentException("No Roblox data type matches CLR property type.");
        }

        /// <summary>
        /// If the given type has a RobloxTypeAttribute attribute, use the name specified there. Otherwise, default to the type name.
        /// </summary>
        private static string GetRobloxTypeName(Type type)
        {
            var robloxTypeAttributes = type.GetCustomAttributes<RobloxTypeAttribute>().ToArray();
            if (robloxTypeAttributes.Any())
                return robloxTypeAttributes.First().RobloxTypeName;
            return type.Name;
        }
    }
}
