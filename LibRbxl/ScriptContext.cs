using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ScriptContext : Instance
    {
        public override string ClassName => "ScriptContext";

        public bool ScriptsDisabled { get; set; }
    }
}
