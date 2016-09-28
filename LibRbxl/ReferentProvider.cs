using System;
using System.Collections.Generic;
using System.Linq;

namespace LibRbxl
{
    public class ReferentProvider
    {
        private readonly Dictionary<Instance, int> _cache = new Dictionary<Instance, int>();
        private int _nextReferent = 0;

        public void Add(Instance instance, int referent)
        {
            if (_cache.ContainsKey(instance) && _cache[instance] != referent)
                throw new ArgumentException("The specified roblox object already has a cached referent value.", nameof(instance));
            if (_cache.ContainsValue(referent) && !_cache.Any(n => n.Value == referent && n.Key == instance))
                throw new ArgumentException("The specified referent value already exists.");

            _cache.Add(instance, referent);
            if (_nextReferent < referent)
                _nextReferent = referent + 1;
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
        
        public int GetReferent(Instance instance)
        {
            if (_cache.ContainsKey(instance))
                return _cache[instance];
            else
            {
                var referent = AllocateReferent();
                _cache.Add(instance, referent);
                return referent;
            }
        }

        private int AllocateReferent()
        {
            return _nextReferent++;
        }

        public Instance GetCached(int referent)
        {
            return _cache.First(n => n.Value == referent).Key;
        }
    }
}