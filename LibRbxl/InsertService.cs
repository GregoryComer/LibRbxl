using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class InsertService : Instance
    {
        public override string ClassName => "InsertService";

        public bool AllowClientInsertModels { get; set; }
        public bool AllowInsertFreeModels { get; set; }
    }
}
