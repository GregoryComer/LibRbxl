namespace LibRbxl.Instances
{
    public class DialogChoice : Instance
    {
        public override string ClassName => "DialogChoice";

        public string GoodbyeDialog { get; set; }
        public string ResponseDialog { get; set; }
        public string UserDialog { get; set; }
    }
}
