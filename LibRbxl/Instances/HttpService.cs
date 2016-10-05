namespace LibRbxl.Instances
{
    public class HttpService : Instance, IService
    {
        public override string ClassName => "HttpService";

        public bool HttpEnabled { get; set; }
    }
}
