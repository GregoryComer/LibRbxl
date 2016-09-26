using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class PlayerGui : BasePlayerGui
    {
        public override string ClassName => "PlayerGui";

        public GuiObject SelectionImageObject { get; set; }
    }
}
