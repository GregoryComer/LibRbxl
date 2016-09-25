using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BrickColorValue : Instance
    {
        public override string ClassName => "BrickColorValue";

        public BrickColor Value { get; set; }
    }
}
