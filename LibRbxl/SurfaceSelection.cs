using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SurfaceSelection : PartAdornment
    {
        public override string ClassName => "SurfaceSelection";

        public NormalId TargetSurface { get; set; }
    }
}
