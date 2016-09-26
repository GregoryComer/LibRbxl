using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class NetworkClient : NetworkPeer
    {
        public override string ClassName => "NetworkClient";

        public string Ticket { get; set; }
    }
}
