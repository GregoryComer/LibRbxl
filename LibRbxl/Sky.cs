using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Sky : Instance
    {
        public override string ClassName => "Sky";

        public bool CelestialBodiesShown { get; set; }
        public string SkyboxBk { get; set; }
        public string SkyboxDn { get; set; }
        public string SkyboxFt { get; set; }
        public string SkyboxLf { get; set; }
        public string SkyboxRt { get; set; }
        public string Skyboxup { get; set; }
        public int StarCount { get; set; }
    }
}
