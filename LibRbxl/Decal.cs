using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Decal : FaceInstance
    {
        public override string ClassName => "Decal";

        public string Texture { get; set; }
        public float Transparency { get; set; }
    }
}
