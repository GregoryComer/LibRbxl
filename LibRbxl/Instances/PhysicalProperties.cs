using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl.Instances
{
    public class PhysicalProperties : IEquatable<PhysicalProperties>
    {
        public PhysicalProperties()
        {
            Enabled = true;

            // Default values taken from Roblox
            Density = .7f;
            Friction = .3f;
            Elasticity = .5f;
            FrictionWeight = 1f;
            ElasticityWeight = 1f;
        }

        public PhysicalProperties(bool enabled)
        {
            Enabled = enabled;

            // Default values taken from Roblox
            Density = .7f;
            Friction = .3f;
            Elasticity = .5f;
            FrictionWeight = 1f;
            ElasticityWeight = 1f;
        }

        public PhysicalProperties(float density, float friction, float elasticity, float frictionWeight, float elasticityWeight)
        {
            Enabled = true;
            Density = density;
            Friction = friction;
            Elasticity = elasticity;
            FrictionWeight = frictionWeight;
            ElasticityWeight = elasticityWeight;
        }

        public bool Enabled { get; set; }
        public float Density { get; set; }
        public float Friction { get; set; }
        public float Elasticity { get; set; }
        public float FrictionWeight { get; set; }
        public float ElasticityWeight { get; set; }

        public bool Equals(PhysicalProperties other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Enabled == other.Enabled && Density.Equals(other.Density) && Friction.Equals(other.Friction) && Elasticity.Equals(other.Elasticity) && FrictionWeight.Equals(other.FrictionWeight) && ElasticityWeight.Equals(other.ElasticityWeight);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PhysicalProperties) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Enabled.GetHashCode();
                hashCode = (hashCode*397) ^ Density.GetHashCode();
                hashCode = (hashCode*397) ^ Friction.GetHashCode();
                hashCode = (hashCode*397) ^ Elasticity.GetHashCode();
                hashCode = (hashCode*397) ^ FrictionWeight.GetHashCode();
                hashCode = (hashCode*397) ^ ElasticityWeight.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PhysicalProperties left, PhysicalProperties right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PhysicalProperties left, PhysicalProperties right)
        {
            return !Equals(left, right);
        }
    }
}
