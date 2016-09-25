using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class MeshPart : BasePart
    {
        public override string ClassName => "MeshPart";

        public CollisionFidelity CollisionFidelity { get; set; }
        public string MeshId { get; set; }
        public string TextureId { get; set; }
    }
}
