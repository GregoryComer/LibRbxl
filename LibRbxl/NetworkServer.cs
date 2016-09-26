using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class NetworkServer : NetworkPeer
    {
        public override string ClassName => "NetworkServer";

        public int Port { get; set; }
    }
}
