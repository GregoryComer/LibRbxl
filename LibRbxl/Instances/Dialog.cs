namespace LibRbxl.Instances
{
    public class Dialog : Instance
    {
        public override string ClassName => "Dialog";

        public float ConversationDistance { get; set; }
        public string GoodbyteDialog { get; set; }
        public bool InUse { get; set; }
        public string InitialPrompt { get; set; }
        public DialogPurpose DialogPurpose { get; set; }
        public DialogTone DialogTone { get; set; }
    }
}
