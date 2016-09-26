using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Shirt : Clothing
    {
        public override string ClassName => "Shirt";

        public string ShirtTemplate { get; set; }
    }
}
