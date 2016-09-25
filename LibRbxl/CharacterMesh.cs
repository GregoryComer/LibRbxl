using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class CharacterMesh : CharacterAppearance
    {
        public override string ClassName => "CharacterMesh";

        public int BaseTextureId { get; set; }
        public BodyPart BodyPart { get; set; }
        public int MeshId { get; set; }
        public int OverlayTextureId { get; set; }
    }
}
