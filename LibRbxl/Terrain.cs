using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Terrain : BasePart
    {
        public override string ClassName => "Terrain";

        // TODO: Serialize terrain data
        public bool IsSmooth { get; set; }
        public Region3int16 MaxExtents { get; set; }
        public Color3 WaterColor { get; set; }
        public float WaterTransparency { get; set; }
        public float WaterWaveSize { get; set; }
        public float WaterWaveSpeed { get; set; }
    }
}
