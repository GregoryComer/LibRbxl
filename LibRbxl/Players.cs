using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Players : Instance
    {
        public override string ClassName => "Players";

        public bool BubbleChat { get; set; }
        public bool CharacterAutoLoads { get; set; }
        public bool ClassicChat { get; set; }
        [RobloxIgnore]
        public Player LocalPlayer { get; set; }
        public int MaxPlayers { get; set; }
        public int NumPlayers { get; set; }
        public int PreferredPlayers { get; set; }
    }
}
