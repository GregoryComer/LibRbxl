using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BodyGyro : BodyMover
    {
        public override string ClassName => "BodyGyro";

        public CFrame CFrame { get; set; }
        public float D { get; set; }
        public Vector3 MaxTorque { get; set; }
        public float P { get; set; }
    }
}
