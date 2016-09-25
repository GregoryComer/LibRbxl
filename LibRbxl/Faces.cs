using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    [Flags]
    public enum Faces
    {
        Front = 0x1,
        Bottom = 0x2,
        Left = 0x4,
        Back = 0x8,
        Top = 0x10,
        Right = 0x20
    }
}
