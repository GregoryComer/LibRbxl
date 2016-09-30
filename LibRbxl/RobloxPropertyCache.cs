// RobloxPropertyCache.cs - LibRbxl
// Copyright © https://github.com/DanDevPC/
// This file is subject to the terms and conditions defined in the 'LICENSE' file.

using System;
using System.Collections.Generic;
using System.Reflection;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class RobloxPropertyCache
    {
        private static Dictionary<Type, RobloxPropertyCache> _typeCaches = new Dictionary<Type, RobloxPropertyCache>();
        private Dictionary<string, PropertyInfo> _cache;

        public RobloxPropertyCache(Type type)
        {
            _cache = new Dictionary<string, PropertyInfo>();
            foreach (var propInfo in type.GetProperties())
            {
                var attr = propInfo.GetCustomAttribute<RobloxPropertyAttribute>();
                _cache.Add(attr?.PropertyName ?? propInfo.Name, propInfo);
            }
        }

        public bool Get(string propertyName, out PropertyInfo match)
        {
            return _cache.TryGetValue(propertyName, out match);
        }

        public static RobloxPropertyCache FetchCacheForType(Type type)
        {
            RobloxPropertyCache cache;
            if (!_typeCaches.TryGetValue(type, out cache))
            {
                _typeCaches[type] = cache = new RobloxPropertyCache(type);
            }
            return cache;
        }
    }
}