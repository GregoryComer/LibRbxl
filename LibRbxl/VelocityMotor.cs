using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class VelocityMotor : JointInstance
    {
        public override string ClassName => "VelocityMotor";

        public float CurrentAngle { get; set; }
        public float DesiredAngle { get; set; }
        public Hole Hole { get; set; }
        public float MaxVelocity { get; set; }
    }
}
