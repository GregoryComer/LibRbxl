using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BodyForce : BodyMover
    {
        public override string ClassName => "BodyForce";

        public Vector3 Force { get; set; }
    }
}
