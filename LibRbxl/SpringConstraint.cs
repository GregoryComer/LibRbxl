using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SpringConstraint : Constraint
    {
        public override string ClassName => "SpringConstraint";

        public float Coils { get; set; }
        public float CurrentLength { get; set; }
        public float Damping { get; set; }
        public float FreeLength { get; set; }
        public bool LimitsEnabled { get; set; }
        public float MaxForce { get; set; }
        public float MaxLength { get; set; }
        public float MinLength { get; set; }
        public float Radius { get; set; }
        public float Stiffness { get; set; }
        public float Thickness { get; set; }
    }
}
