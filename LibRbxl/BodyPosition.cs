using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BodyPosition : BodyMover
    {
        public override string ClassName => "BodyPosition";

        public float D { get; set; }
        public Vector3 MaxForce { get; set; }
        public float P { get; set; }
        public Vector3 Position { get; set; }
    }
    }
}
