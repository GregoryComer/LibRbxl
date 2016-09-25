using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class DataModelMesh : Instance
    {
        public Vector3 Offset { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 VertexColor { get; set; }
    }
}
