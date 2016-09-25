using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class Property
    {
        public string Name { get; set; }
        public PropertyType Type { get; set; }

        protected Property(string name, PropertyType type)
        {
            Name = name;
            Type = type;
        }

        public abstract object Get();

        public abstract void Set(object value);

        public void Set<T>(T value)
        {
            var tThis = this as Property<T>;
            if (tThis == null)
                throw new ArgumentException("Type of provided value does not match type of property.");
            tThis.Value = value;
        }
    }

    public abstract class Property<T> : Property
    {
        public T Value { get; set; }

        protected Property(string name, PropertyType type, T value) : base(name, type)
        {
            Value = value;
        }

        public override object Get()
        {
            return Value;
        }

        public override void Set(object value)
        {
            Set<T>((T)value);
        }
    }

    public enum PropertyType
    {
        String = 0x1,
        Boolean = 0x2,
        Int32 = 0x3,
        Float = 0x4,
        Double = 0x5,
        UDim2 = 0x7,
        Ray = 0x8,
        Faces = 0x9,
        Axis = 0xA,
        BrickColor = 0xB,
        Color3 = 0xC,
        Vector2 = 0xD,
        Vector3 = 0xE,
        CFrame = 0x10,
        Enumeration = 0x12,
        Referent = 0x13
    }
}
