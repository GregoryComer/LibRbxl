using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RodConstraint : Constraint
    {
        public override string ClassName => "RodConstraint";

        [RobloxIgnore]
        public float CurrentDistance { get; set; }
        public float Length { get; set; }
        public float Thickness { get; set; }
    }
}
