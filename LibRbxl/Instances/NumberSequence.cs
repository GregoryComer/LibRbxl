using System;
using System.Linq;

namespace LibRbxl.Instances
{
    public class NumberSequence : IEquatable<NumberSequence>
    {
        public NumberSequenceKeypoint[] Keypoints { get; }

        public NumberSequence(NumberSequenceKeypoint[] keypoints)
        {
            Keypoints = keypoints;
        }

        public NumberSequence(float val) : this(new [] { new NumberSequenceKeypoint(0, val, 0), new NumberSequenceKeypoint(1, val, 0) })
        {
            
        }

        public NumberSequence(float n0, float n1) : this(new[] { new NumberSequenceKeypoint(0, n0, 0), new NumberSequenceKeypoint(1, n1, 0) })
        {
            
        }

        public bool Equals(NumberSequence other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(Keypoints, null) && ReferenceEquals(other.Keypoints, null)) return true;
            if (ReferenceEquals(Keypoints, null)) return false;
            if (ReferenceEquals(other.Keypoints, null)) return false;
            return Keypoints.SequenceEqual(other.Keypoints);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NumberSequence) obj);
        }

        public override int GetHashCode()
        {
            return (Keypoints != null ? Keypoints.GetHashCode() : 0);
        }

        public static bool operator ==(NumberSequence left, NumberSequence right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NumberSequence left, NumberSequence right)
        {
            return !Equals(left, right);
        }
    }
}
