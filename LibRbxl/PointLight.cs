using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class PointLight : Light
    {
        public override string ClassName => "PointLight";

        public float Range { get; set; }
    }
}
