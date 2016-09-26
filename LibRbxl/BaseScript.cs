using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class BaseScript : LuaSourceContainer
    {
        public bool Disabled { get; set; }
        public string LinkedSource { get; set; }
    }
}
