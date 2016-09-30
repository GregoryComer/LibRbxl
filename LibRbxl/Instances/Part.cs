namespace LibRbxl.Instances
{
    public class Part : BasePart
    {
        public override string ClassName => "Part";

        public PartType PartType { get; set; }
    }
}
