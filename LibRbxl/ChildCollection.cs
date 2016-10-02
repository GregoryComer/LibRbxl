using System.Collections;
using System.Collections.Generic;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class ChildCollection : ICollection<Instance>
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
            _children.Add(child);
            if (child.Parent != _owner)
                child.Parent = _owner;
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains(Instance child)
        {
            return _children.Contains(child);
        }

        public void CopyTo(Instance[] array, int arrayIndex)                
        {
            _children.CopyTo(array, arrayIndex);
        }

        bool ICollection<Instance>.Remove(Instance item)
        {
            return _children.Remove(item);
        }

        public int Count => _children.Count;
        public bool IsReadOnly => false;

        public void Remove(Instance child)
        {
            _children.Remove(child);
            if (child.Parent == _owner)
                child.Parent = null;
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