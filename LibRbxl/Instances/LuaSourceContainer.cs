namespace LibRbxl.Instances
{
    public abstract class LuaSourceContainer : Instance
    {
        [RobloxIgnore]
        public Player CurrentEditor { get; set; }
    }
}
