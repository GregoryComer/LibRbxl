using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class PartOperation : BasePart
    {
        public CollisionFidelity CollisionFidelity { get; set; }
        public int TriangleCount { get; set; }
        public bool UsePartColor { get; set; }
    }
}
