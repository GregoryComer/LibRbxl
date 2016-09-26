using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ShirtGraphic : CharacterAppearance
    {
        public override string ClassName => "ShirtGraphic";

        public string Graphic { get; set; }
    }
}
