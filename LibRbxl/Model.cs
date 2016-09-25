using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Model : PVInstance
    {
        public override string ClassName => "Model";

        public BasePart PrimaryPart { get; set; }
    }
}
