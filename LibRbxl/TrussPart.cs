using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class TrussPart : BasePart
    {
        public override string ClassName => "TrussPart";

        public TrussStyle Style { get; set; }
    }
}
