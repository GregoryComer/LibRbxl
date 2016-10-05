namespace LibRbxl.Instances
{
    public class ServerScriptService : Instance, IService
    {
        public override string ClassName => "ServerScriptService";

        public bool LoadStringEnabled { get; set; }
    }
}
