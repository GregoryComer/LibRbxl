using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct UDim2
    {
        public UDim X { get; }
        public UDim Y { get; }

        public float OffsetX => X.Offset;
        public float ScaleX => X.Scale;
        public float OffsetY => Y.Offset;
        public float ScaleY => Y.Scale;

        public UDim2(UDim x, UDim y)
        {
            X = x;
            Y = y;
        }

        public UDim2(float offsetX, float scaleX, float offsetY, float scaleY)
        {
            X = new UDim(offsetX, scaleX);
            Y = new UDim(offsetY, scaleY);
        }
    }
}
