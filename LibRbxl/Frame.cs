using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Frame : GuiObject
    {
        public override string ClassName => "Frame";

        public FrameStyle Style { get; set; }
    }
}
