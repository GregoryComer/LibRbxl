using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Part : BasePart
    {
        public override string ClassName => "Part";

        public PartType PartType { get; set; }
    }
}
