using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Script : BasePart
    {
        public override string ClassName => "Script";
        
        public string Source { get; set; }
    }
}
