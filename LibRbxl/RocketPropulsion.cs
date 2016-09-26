using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RocketPropulsion : BodyMover
    {
        public override string ClassName => "RocketPropulsion";

        public float CartoonFactor { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxThrust { get; set; }
        public Vector3 MaxTorque { get; set; }
        public BasePart Target { get; set; }
        public Vector3 TargetOffset { get; set; }
        public float TargetRadius { get; set; }
        public float ThrustD { get; set; }
        public float ThrustP { get; set; }
        public float TurnD { get; set; }
        public float TurnP { get; set; }
    }
}
