using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct Ray
    {
        public Vector3 Direction { get; }
        public Vector3 Origin { get; }

        public Ray Unit => new Ray(Direction, Origin.Unit);

        public Ray(Vector3 direction, Vector3 origin)
        {
            Direction = direction;
            Origin = origin;
        }
    }
}
