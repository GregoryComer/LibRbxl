using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Texture : Decal
    {
        public override string ClassName => "Texture";

        public float StudsPerTileU { get; set; }
        public float StudsPerTileV { get; set; }
    }
}
