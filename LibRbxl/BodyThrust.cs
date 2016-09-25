using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BodyThrust : BodyMover
    {
        public override string ClassName => "BodyThrust";

        public Vector3 Force { get; set; }
        public Vector3 Location { get; set; }
    }
}
