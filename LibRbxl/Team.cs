using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Team : Instance
    {
        public override string ClassName => "Team";

        public bool AutoAssignable { get; set; }
        public BrickColor TeamColor { get; set; }
    }
}
