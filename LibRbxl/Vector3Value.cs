using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Vector3Value : Instance
    {
        public override string ClassName => "Vector3Value";

        public Vector3 Value { get; set; }
    }
}
