using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class TerrainRegion : Instance
    {
        public override string ClassName => "TerrainRegion";

        public bool IsSmooth { get; set; }
        public Vector3 SizeInCells { get; set; }
    }
}
