namespace LibRbxl.Instances
{
    public class NetworkClient : NetworkPeer
    {
        public override string ClassName => "NetworkClient";

        public string Ticket { get; set; }
    }
}
