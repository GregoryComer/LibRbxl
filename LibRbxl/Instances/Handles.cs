namespace LibRbxl.Instances
{
    public class Handles : HandlesBase
    {
        public override string ClassName => "Handles";

        public Faces Faces { get; set; }
        public HandlesStyle Style { get; set; }
    }
}
