namespace LibRbxl.Instances
{
    public abstract class BaseScript : LuaSourceContainer
    {
        public bool Disabled { get; set; }
        public string LinkedSource { get; set; }
        public string ScriptGuid { get; set; } // TODO Convert this to an actual guid
    }
}
