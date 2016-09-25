using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class FileMesh : DataModelMesh
    {
        public override string ClassName => "FileMesh";

        public string MeshId { get; set; }
        public string TextureId { get; set; }
    }
}
