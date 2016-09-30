namespace LibRbxl.Instances
{
    public class AnimationTrack : Instance
    {
        public override string ClassName => "AnimationTrack";

        public Animation Animation { get; set; }
        public bool IsPlaying { get; set; }
        public float Length { get; set; }
        public AnimationPriority Priority { get; set; }
        public float TimePosition { get; set; }
    }
}
