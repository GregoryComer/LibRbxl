namespace LibRbxl.Instances
{
    public class Sound : Instance
    {
        public override string ClassName => "Sound";

        [RobloxIgnore]
        public bool IsLoaded { get; set; }
        [RobloxIgnore]
        public bool IsPaused { get; set; }
        [RobloxIgnore]
        public bool IsPlaying { get; set; }
        public bool Looped { get; set; }
        public float MaxDistance { get; set; }
        public float MinDistance { get; set; }
        public float Pitch { get; set; }
        public bool PlayOnRemove { get; set; }
        [RobloxIgnore]
        public bool Playing { get; set; }
        public string SoundId { get; set; }
        [RobloxIgnore]
        public double TimeLength { get; set; }
        [RobloxIgnore]
        public double TimePosition { get; set; }
        public float Volume { get; set; }
    }
}
