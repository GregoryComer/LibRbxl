namespace LibRbxl.Instances
{
    public class Part : BasePart
    {
        public override string ClassName => "Part";

        [RobloxProperty("formFactorRaw", PropertyType.Enumeration)]
        public FormFactor FormFactor { get; set; }

        [RobloxIgnore] // TODO Look into this
        public PartType PartType { get; set; }
    }
}
