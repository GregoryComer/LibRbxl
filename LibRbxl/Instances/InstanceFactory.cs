using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LibRbxl.Internal;

namespace LibRbxl.Instances
{
    internal static class InstanceFactory
    {
        public static Instance Create(string type, bool service)
        {
            if (!ReflectionMappingManager.TypeCache.ContainsKey(type) || type == "Instance")
            {
                var unmanagedInstance = new UnmanagedInstance(type) {IsSingleton = service};
                return unmanagedInstance;
            }

            var managedType = ReflectionMappingManager.TypeCache[type];
            var instance = (Instance)Activator.CreateInstance(managedType);
            return instance;
        }
    }
}