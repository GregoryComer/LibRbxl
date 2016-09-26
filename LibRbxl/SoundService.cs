using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SoundService : Instance
    {
        public override string ClassName => "SoundService";

        public ReverbType AmbientReverb { get; set; }
        public float DistanceFactor { get; set; }
        public float DopplerScale { get; set; }
        public float RolloffScale { get; set; }
    }
}
