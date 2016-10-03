using System;

namespace LibRbxl.Instances
{
    public struct NumberRange : IEquatable<NumberRange>
    {
        public float Min { get; }
        public float Max { get; }

        public NumberRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public NumberRange(float value) : this(value, value)
        {
            
        }

        public bool Equals(NumberRange other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NumberRange && Equals((NumberRange) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Min.GetHashCode()*397) ^ Max.GetHashCode();
            }
        }

        public static bool operator ==(NumberRange left, NumberRange right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NumberRange left, NumberRange right)
        {
            return !left.Equals(right);
        }
    }
}
