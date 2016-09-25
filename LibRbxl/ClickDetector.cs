using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ClickDetector : Instance
    {
        public override string ClassName => "ClickDetector";

        public float MaxActivationDistance { get; set; }
    }
}
