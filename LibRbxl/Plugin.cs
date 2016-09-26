using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Plugin : Instance
    {
        public override string ClassName => "Plugin";

        public bool CollisionEnabled { get; set; }
        public float GridSize { get; set; }
    }
}
