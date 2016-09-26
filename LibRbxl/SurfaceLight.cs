using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SurfaceLight : Light
    {
        public override string ClassName => "SurfaceLight";

        public float Angle { get; set; }
        public NormalId Face { get; set; }
        public float Range { get; set; }
    }
}
