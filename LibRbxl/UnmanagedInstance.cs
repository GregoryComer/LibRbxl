using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class UnmanagedInstance : Instance
    {
        public override string ClassName { get; }

        public UnmanagedInstance(string className)
        {
            ClassName = className;
        }
    }
}
