using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Motor : JointInstance
    {
        public override string ClassName => "Motor";

        public float CurrentAngle { get; set; }
        public float DesiredAngle { get; set; }
        public float MaxVelocity { get; set; }
    }
}
