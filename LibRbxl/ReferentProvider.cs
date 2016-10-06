using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class ReferentProvider
    {
        private readonly Dictionary<Instance, int> _cache = new Dictionary<Instance, int>();
        private readonly Dictionary<int, Instance> _inverseCache = new Dictionary<int, Instance>();
        private int _nextReferent = 0;

        public void Add(Instance instance, int referent)
        {
            Trace.WriteLine($"{referent}\t{instance.ClassName}");

            if (_cache.ContainsKey(instance) && _cache[instance] != referent)
                throw new ArgumentException("The specified roblox object already has a cached referent value.", nameof(instance));
            if (_inverseCache.ContainsKey(referent) && _inverseCache[referent] != instance)
                throw new ArgumentException("The specified referent value already exists.");

            _cache.Add(instance, referent);
            _inverseCache.Add(referent, instance);
            if (_nextReferent < referent)
                _nextReferent = referent + 1;
        }

        public void ClearCache()
        {
            return; // DEBUG
            _cache.Clear();
            _inverseCache.Clear();
            _nextReferent = 0;
        }
        
        public int GetReferent(Instance instance)
        {
            if (instance == null)
                return -1;

            if (_cache.ContainsKey(instance))
                return _cache[instance];
            else
            {
                var referent = AllocateReferent();
                Add(instance, referent);
                return referent;
            }
        }

        private int AllocateReferent()
        {
            return _nextReferent++;
        }

        public Instance GetCached(int referent)
        {
            return _inverseCache[referent];
        }
    }
}