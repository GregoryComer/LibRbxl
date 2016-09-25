using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class Instance : RobloxObject
    {
        public bool Archivable { get; set; }

        public abstract string ClassName { get; }

        public string Name { get; set; }

        public Instance Parent { get; set; }
    }
}

