using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Pose : Instance
    {
        public override string ClassName => "Pose";

        public CFrame CFrame { get; set; }
        public PoseEasingDirection EasingDirection { get; set; }
        public PoseEasingStyle EastingStyle { get; set; }
        public float Weight { get; set; }
    }
}
