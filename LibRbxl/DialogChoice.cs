using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class DialogChoice : Instance
    {
        public override string ClassName => "DialogChoice";

        public string GoodbyeDialog { get; set; }
        public string ResponseDialog { get; set; }
        public string UserDialog { get; set; }
    }
}
