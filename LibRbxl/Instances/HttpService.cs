namespace LibRbxl.Instances
{
    public class HttpService : Instance, ISingleton
    {
        public override string ClassName => "HttpService";

        public bool HttpEnabled { get; set; }
    }
}
