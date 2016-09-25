using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class PropertyCollection : IEnumerable<Property>
    {
        private Dictionary<string, Property> _properties;

        public PropertyCollection()
        {
            _properties = new Dictionary<string, Property>(StringComparer.InvariantCultureIgnoreCase);
        }

        public PropertyCollection(IEnumerable<Property> properties)
        {
            _properties = properties.ToDictionary(n => n.Name, n => n, StringComparer.InvariantCultureIgnoreCase);
        }

        public IReadOnlyDictionary<string, Property> Items => _properties;

        public Property this[string name] => _properties[name];

        public void Add(Property property)
        {
            if (!_properties.ContainsKey(property.Name))
                _properties.Add(property.Name, property);
            else
            {
                var existingProp = _properties[property.Name];
                if (existingProp.Type != property.Type)
                    throw new ArgumentException("The specified property already exists and the type does not match the provided property.", nameof(property));
                existingProp.Set(property.Get());
            }
        }

        public void AddRange(IEnumerable<Property> properties)
        {
            var genericPropertyType = typeof (Property<>);
            foreach (var prop in properties)
                Add(prop);
        }

        public bool Contains(string name)
        {
            return _properties.ContainsKey(name);
        }

        public void Remove(string name)
        {
            _properties.Remove(name);
        }

        public void Remove(Property property)
        {
            _properties.Remove(property.Name);
        }

        IEnumerator<Property> IEnumerable<Property>.GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }
    }
}
