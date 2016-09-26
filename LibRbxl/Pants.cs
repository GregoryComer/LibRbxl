using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Pants : Clothing
    {
        public override string ClassName => "Pants";

        public string PantsTemplate { get; set; }
    }
}
