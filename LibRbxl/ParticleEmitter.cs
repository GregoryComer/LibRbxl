using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ParticleEmitter : Instance
    {
        public override string ClassName => "ParticleEmitter";

        public Vector3 Acceleration { get; set; }
        public ColorSequence Color { get; set; }
        public float Drag { get; set; }
        public NormalId EmissionDirection { get; set; }
        public NumberRange Lifetime { get; set; }
        public float LightEmission { get; set; }
        public bool LockedToPart { get; set; }
        public float Rate { get; set; }
        public NumberRange RotSpeed { get; set; }
        public NumberRange Rotation { get; set; }
        public NumberSequence Size { get; set; }
        public NumberRange Speed { get; set; }
        public string Texture { get; set; }
        public NumberSequence Transparency { get; set; }
        public float VelocityInheritance { get; set; }
        public float VelocitySpread { get; set; }
        public float ZOffset { get; set; }
    }
}
