namespace LibRbxl.Instances
{
    public class KeyframeSequence : Instance
    {
        public override string ClassName => "KeyframeSequence";

        public bool Loop { get; set; }
        public AnimationPriority Priority { get; set; }
    }
}
