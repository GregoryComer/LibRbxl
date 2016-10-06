namespace LibRbxl.Instances
{
    public class StarterGui : BasePlayerGui, ISingleton
    {
        public override string ClassName => "StarterGui";

        public bool ResetPlayerGuiOnSpawn { get; set; }
        public bool ShowDevelopmentGui { get; set; }
    }
}
