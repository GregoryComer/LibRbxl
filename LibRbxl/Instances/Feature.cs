namespace LibRbxl.Instances
{
    public abstract class Feature : Instance
    {
        public NormalId FaceId { get; set; }
        public InOut InOut { get; set; }
        public LeftRight LeftRight { get; set; }
        public TopBottom TopBottom { get; set; }
    }
}
