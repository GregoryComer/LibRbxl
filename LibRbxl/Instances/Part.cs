namespace LibRbxl.Instances
{
    public class Part : BasePart
    {
        public override string ClassName => "Part";

        [RobloxProperty("formFactorRaw", PropertyType.Enumeration)] // Documentation has this as a member of FormFactorPart, which is a descendent of BasePart, but I'm seeing on Parts. Need to look more into this. TODO
        public FormFactor FormFactor { get; set; }

        [RobloxIgnore] // TODO Look into this
        public PartType PartType { get; set; }
    }
}
