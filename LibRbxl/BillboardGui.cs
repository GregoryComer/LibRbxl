using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class BillboardGui : LayerCollector
    {
        public override string ClassName => "BillboardGui";

        public bool Active { get; set; }
        public BasePart Adornee { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool Enabled { get; set; }
        public Vector3 ExtentsOffset { get; set; }
        public Player PlayerToHideFrom { get; set; }
        public UDim2 Size { get; set; }
        public Vector2 SizeOffset { get; set; }
        public Vector3 StudsOffset { get; set; }
    }
}
