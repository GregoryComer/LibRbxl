using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Handles : HandlesBase
    {
        public override string ClassName => "Handles";

        public Faces Faces { get; set; }
        public HandlesStyle Style { get; set; }
    }
}
