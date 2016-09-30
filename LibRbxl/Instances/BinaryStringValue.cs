using LibRbxl.Internal;

namespace LibRbxl.Instances
{
    public class BinaryStringValue : Instance
    {
        public override string ClassName => "BinaryStringValue";

        [RobloxIgnore]
        public byte[] BinaryData { get; set; }

        public string Value
        {
            get { return Util.RobloxEncoding.GetString(BinaryData); }
            set { BinaryData = Util.RobloxEncoding.GetBytes(value); }
        }
    }
}
