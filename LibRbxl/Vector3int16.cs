using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct Vector3int16
    {
        public Vector3int16(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public short X { get; }
        public short Y { get; }
        public short Z { get; }

        // TODO: Implement operators
    }
}
