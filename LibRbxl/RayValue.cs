using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RayValue : Instance
    {
        public override string ClassName => "RayValue";

        public Ray Value { get; set; }
    }
}
