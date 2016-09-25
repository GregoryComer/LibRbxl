using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class SpawnLocation : Part
    {
        public override string ClassName => "SpawnLocation";

        public bool AllowTeamChangeOnTouch { get; set; }
        public int Duration { get; set; }
        public bool Enabled { get; set; }
        public bool Neutral { get; set; }
        public BrickColor TeamColor { get; set; }
    }
}
