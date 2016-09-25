using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BodyVelocity : BodyMover
    {
        public override string ClassName => "BodyVelocity";

        public Vector3 MaxForce { get; set; }
        public float P { get; set; }
        public Vector3 Velocity { get; set; }
    }
}
