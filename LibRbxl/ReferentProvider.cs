using System;
using System.Collections.Generic;
using System.Linq;

namespace LibRbxl
{
    public class ReferentProvider
    {
        private readonly Dictionary<RobloxObject, int> _cache = new Dictionary<RobloxObject, int>();
        private int _nextReferent = 0;

        public void Add(RobloxObject robloxObject, int referent)
        {
            if (_cache.ContainsKey(robloxObject) && _cache[robloxObject] != referent)
                throw new ArgumentException("The specified roblox object already has a cached referent value.", nameof(robloxObject));
            if (_cache.ContainsValue(referent) && !_cache.Any(n => n.Value == referent && n.Key == robloxObject))
                throw new ArgumentException("The specified referent value already exists.");

            _cache.Add(robloxObject, referent);
            if (_nextReferent < referent)
                _nextReferent = referent + 1;
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
        
        public int GetReferent(RobloxObject robloxObject)
        {
            if (_cache.ContainsKey(robloxObject))
                return _cache[robloxObject];
            else
            {
                var referent = AllocateReferent();
                _cache.Add(robloxObject, referent);
                return referent;
            }
        }

        private int AllocateReferent()
        {
            return _nextReferent++;
        }
    }
}