using System.Collections;
using System.Collections.Generic;

namespace LibRbxl.Instances
{
    public abstract class Instance : RobloxObject
    {
        private Instance _parent;
        public bool Archivable { get; set; }

        public abstract string ClassName { get; }

        public string Name { get; set; }

        [RobloxIgnore]
        public Instance Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                // DEBUG
                /*if (!_parent.Children.Contains(this))
                    _parent.Children.Add(this);*/
            }
        }

        [RobloxIgnore]
        public ChildCollection Children { get; }

        [RobloxIgnore]
        public int Referent { get; set; }

        [RobloxIgnore]
        public Dictionary<string, Property> UnmanagedProperties { get; } 

        protected Instance()
        {
            Children = new ChildCollection(this);
            UnmanagedProperties = new Dictionary<string, Property>();
        }
    }

    public class ChildCollection : IEnumerable<Instance>
    {
        private readonly List<Instance> _children;
        private readonly Instance _owner;

        public ChildCollection(Instance owner)
        {
            _children = new List<Instance>();
            _owner = owner;
        }

        public ChildCollection(IEnumerable<Instance> children, Instance owner)
        {
            _children = new List<Instance>();
            _owner = owner;

            foreach (var child in children)
                Add(child);
        }

        public void Add(Instance child)
        {
            child.Parent = _owner;
            // DEBUG
            // _children.Add(child);
        }

        public bool Contains(Instance child)
        {
            if (_children.Count == 0) // DEBUG, Possible infinite recursion?
                return false;
            return _children.Contains(child);
        }

        public void Remove(Instance child)
        {
            _children.Remove(child);
        }

        public IEnumerator<Instance> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

