namespace LibRbxl.Instances
{
    public class NetworkServer : NetworkPeer
    {
        public override string ClassName => "NetworkServer";

        public int Port { get; set; }
    }
}
