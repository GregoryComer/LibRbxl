using System;
using System.Linq;

namespace LibRbxl.Instances
{
    public struct ColorSequence : IEquatable<ColorSequence>
    {
        public ColorSequenceKeypoint[] Keypoints { get; }

        public ColorSequence(Color3 c0, Color3 c1) : this(new[] {new ColorSequenceKeypoint(0, c0), new ColorSequenceKeypoint(1, c1)})
        {
            
        }

        public ColorSequence(Color3 color) : this(new [] { new ColorSequenceKeypoint(0, color), new ColorSequenceKeypoint(1, color)})
        {
            
        }

        public ColorSequence(ColorSequenceKeypoint[] keypoints)
        {
            Keypoints = keypoints;
        }

        public bool Equals(ColorSequence other)
        {
            if (Keypoints == null && other.Keypoints == null) return true;
            if (Keypoints == null || other.Keypoints == null) return false;
            return Keypoints.SequenceEqual(other.Keypoints);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ColorSequence && Equals((ColorSequence) obj);
        }

        public override int GetHashCode()
        {
            return (Keypoints != null ? Keypoints.GetHashCode() : 0);
        }

        public static bool operator ==(ColorSequence left, ColorSequence right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ColorSequence left, ColorSequence right)
        {
            return !left.Equals(right);
        }
    }

    public struct ColorSequenceKeypoint : IEquatable<ColorSequenceKeypoint>
    {
        public float Time { get; }
        public Color3 Value { get; }

        public ColorSequenceKeypoint(float time, Color3 value)
        {
            Time = time;
            Value = value;
        }

        public bool Equals(ColorSequenceKeypoint other)
        {
            return Time.Equals(other.Time) && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ColorSequenceKeypoint && Equals((ColorSequenceKeypoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Time.GetHashCode()*397) ^ Value.GetHashCode();
            }
        }

        public static bool operator ==(ColorSequenceKeypoint left, ColorSequenceKeypoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ColorSequenceKeypoint left, ColorSequenceKeypoint right)
        {
            return !left.Equals(right);
        }
    }
}
