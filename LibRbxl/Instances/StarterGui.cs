namespace LibRbxl.Instances
{
    public class StarterGui : BasePlayerGui
    {
        public override string ClassName => "StarterGui";

        public bool ResetPlayerGuiOnSpawn { get; set; }
        public bool ShowDevelopmentGui { get; set; }
    }
}
