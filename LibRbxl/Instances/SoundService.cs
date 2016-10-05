namespace LibRbxl.Instances
{
    public class SoundService : Instance, IService
    {
        public override string ClassName => "SoundService";

        public ReverbType AmbientReverb { get; set; }
        public float DistanceFactor { get; set; }
        public float DopplerScale { get; set; }
        public float RolloffScale { get; set; }
    }
}
