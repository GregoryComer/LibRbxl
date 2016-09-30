namespace LibRbxl.Instances
{
    public class BodyColors : Instance
    {
        public override string ClassName => "BodyColors";

        public BrickColor HeadColor { get; set; }
        public BrickColor LeftArmColor { get; set; }
        public BrickColor LeftLegColor { get; set; }
        public BrickColor RightArmColor { get; set; }
        public BrickColor RightLegColor { get; set; }
        public BrickColor TorsoColor { get; set; }
    }
}
