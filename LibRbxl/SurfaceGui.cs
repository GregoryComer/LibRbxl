using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SurfaceGui : LayerCollector
    {
        public override string ClassName => "SurfaceGui";

        public bool Active { get; set; }
        public BasePart Adornee { get; set; }
        public bool AlwaysOnTop { get; set; }
        public  Vector2 CanvasSize { get; set; }
        public bool Enabled { get; set; }
        public NormalId Face { get; set; }
        public float ToolPunchThroughDistance { get; set; }
    }
}
