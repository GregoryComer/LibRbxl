using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Sparkles : Instance
    {
        public override string ClassName => "Sparkles";

        public bool Enabled { get; set; }
        public Color3 SparkleColor { get; set; }
    }
}
