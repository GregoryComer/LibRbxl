using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class StarterGui : BasePlayerGui
    {
        public override string ClassName => "StarterGui";

        public bool ResetPlayerGuiOnSpawn { get; set; }
        public bool ShowDevelopmentGui { get; set; }
    }
}
