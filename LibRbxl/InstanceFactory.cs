using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LibRbxl
{
    internal static class InstanceFactory
    {
        private static readonly Dictionary<string, Type> MappedTypes = new Dictionary<string, Type>(); // Maps Roblox type names to managed types

        static InstanceFactory()
        {
            BuildTypeDictionary();
        }

        private static void BuildTypeDictionary() // This could be replaced with an explicit list, but I'm using reflection here to save on development time.
        {
            var assemblyTypes = Assembly.GetAssembly(typeof(Instance)).GetTypes();
            foreach (var type in assemblyTypes.Where(type => typeof(Instance).IsAssignableFrom(type)))
            {
                var robloxTypeName = GetRobloxTypeName(type);
                MappedTypes.Add(robloxTypeName, type);
            }
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

        public static Instance Create(string type)
        {
            if (!MappedTypes.ContainsKey(type))
                throw new ArgumentException($"No managed type for Roblox type \"{type}\".");

            var managedType = MappedTypes[type];
            var instance = (Instance)Activator.CreateInstance(managedType);
            return instance;
        }
    }
}