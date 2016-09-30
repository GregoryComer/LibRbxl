namespace LibRbxl.Instances
{
    public class Team : Instance
    {
        public override string ClassName => "Team";

        public bool AutoAssignable { get; set; }
        public BrickColor TeamColor { get; set; }
    }
}
