using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SpecialMesh : FileMesh
    {
        public override string ClassName => "SpecialMesh";

        public MeshType MeshType { get; set; }
    }
}
