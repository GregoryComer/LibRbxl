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
        public PoseEasingStyle EasingStyle { get; set; }
        [RobloxProperty("MaskWeight", PropertyType.Float)]
        public float Weight { get; set; }
    }
}
