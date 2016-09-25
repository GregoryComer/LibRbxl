using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Color3Value : Instance
    {
        public override string ClassName => "Color3Value";

        public Color3 Value { get; set; }
    }
}
