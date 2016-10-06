namespace LibRbxl.Instances
{
    public class ServerScriptService : Instance, ISingleton
    {
        public override string ClassName => "ServerScriptService";

        public bool LoadStringEnabled { get; set; }
    }
}
