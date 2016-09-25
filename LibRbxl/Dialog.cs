using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Dialog : Instance
    {
        public override string ClassName => "Dialog";

        public float ConversationDistance { get; set; }
        public string GoodbyteDialog { get; set; }
        public bool InUse { get; set; }
        public string InitialPrompt { get; set; }
        public DialogPurpose DialogPurpose { get; set; }
        public DialogTone DialogTone { get; set; }
    }
}
