using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class HttpService : Instance
    {
        public override string ClassName => "HttpService";

        public bool HttpEnabled { get; set; }
    }
}
