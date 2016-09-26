using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Mouse : Instance
    {
        public override string ClassName => "Mouse";

        public CFrame Hit { get; set; }
        public string Icon { get; set; }
        public CFrame Origin { get; set; }
        public BasePart Target { get; set; }
        public Instance TargetFilter { get; set; }
        public NormalId TargetSurface { get; set; }
        public Ray UnitRay { get; set; }
        public int ViewSizeX { get; set; }
        public int ViewSizeY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
