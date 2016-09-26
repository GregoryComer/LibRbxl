using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class LuaSourceContainer : Instance
    {
        [RobloxIgnore]
        public Player CurrentEditor { get; set; }
    }
}
