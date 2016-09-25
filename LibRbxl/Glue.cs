using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Glue : JointInstance
    {
        public override string ClassName => "Glue";

        public Vector3 F0 { get; set; }
        public Vector3 F1 { get; set; }
        public Vector3 F2 { get; set; }
        public Vector3 F3 { get; set; }
    }
}
