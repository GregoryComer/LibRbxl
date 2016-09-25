using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Part : BasePart
    {
        public PartType PartType { get; set; }

        public Part() : base("Part")
        {
        }

        public Part(string className) : base(className)
        {
        }
    }
}
