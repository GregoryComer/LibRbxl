namespace LibRbxl.Instances
{
    public class HttpService : Instance
    {
        public override string ClassName => "HttpService";

        public bool HttpEnabled { get; set; }
    }
}
