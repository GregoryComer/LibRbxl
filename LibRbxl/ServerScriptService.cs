using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ServerScriptService : Instance
    {
        public override string ClassName => "ServerScriptService";

        public bool LoadStringEnabled { get; set; }
    }
}
