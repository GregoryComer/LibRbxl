using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;

namespace LibRbxl
{
    public class RectangleProperty : Property<Rectangle>
    {
        public RectangleProperty(string name, Rectangle value) : base(name, PropertyType.Rectangle, value)
        {
        }
    }
}
