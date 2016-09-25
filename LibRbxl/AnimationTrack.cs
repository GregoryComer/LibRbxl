using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class AnimationTrack : Instance
    {
        public override string ClassName => "AnimationTrack";

        public Animation Animation { get; set; }
        public bool IsPlaying { get; set; }
        public float Length { get; set; }
        public AnimationPriority Priority { get; set; }
        public float TimePosition { get; set; }
    }
}
