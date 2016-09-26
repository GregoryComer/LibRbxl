using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct UDim
    {
        public int Offset { get; }
        public float Scale { get; }

        public UDim(int offset, float scale)
        {
            Offset = offset;
            Scale = scale;
        }
    }
}
