namespace LibRbxl.Instances
{
    public class Plugin : Instance
    {
        public override string ClassName => "Plugin";

        public bool CollisionEnabled { get; set; }
        public float GridSize { get; set; }
    }
}
