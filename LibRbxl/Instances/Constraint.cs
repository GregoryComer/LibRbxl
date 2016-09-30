namespace LibRbxl.Instances
{
    public abstract class Constraint : Instance
    {
        public Attachment Attachment0 { get; set; }
        public Attachment Attachment1 { get; set; }
        public BrickColor Color { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
    }
}