using System;

namespace LibRbxl.Instances
{
    public struct NumberSequenceKeypoint : IEquatable<NumberSequenceKeypoint>
    {
        public float Time { get; }
        public float Value { get; }
        public float Envelope { get; }

        public NumberSequenceKeypoint(float time, float value, float envelope)
        {
            Time = time;
            Value = value;
            Envelope = envelope;
        }

        public bool Equals(NumberSequenceKeypoint other)
        {
            return Time.Equals(other.Time) && Value.Equals(other.Value) && Envelope.Equals(other.Envelope);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NumberSequenceKeypoint && Equals((NumberSequenceKeypoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Time.GetHashCode();
                hashCode = (hashCode*397) ^ Value.GetHashCode();
                hashCode = (hashCode*397) ^ Envelope.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(NumberSequenceKeypoint left, NumberSequenceKeypoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NumberSequenceKeypoint left, NumberSequenceKeypoint right)
        {
            return !left.Equals(right);
        }
    }
}