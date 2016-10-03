using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl.Instances
{
    public struct Rectangle
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }

        public Vector2 Min => new Vector2(MinX, MinY);
        public Vector2 Max => new Vector2(MaxX, MaxY);
        public float Width => MaxX - MinX;
        public float Height => MaxY - MinY;

        public Rectangle(float minX, float minY, float maxX, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
    }
}
