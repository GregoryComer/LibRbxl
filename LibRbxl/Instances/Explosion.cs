namespace LibRbxl.Instances
{
    public class Explosion : Instance
    {
        public override string ClassName => "Explosion";

        public float BlastPressure { get; set; }
        public float BlastRadius { get; set; }
        public float DestroyJoinRadiusPercent { get; set; }
        public ExplosionType ExplosionType { get; set; }
        public Vector3 Position { get; set; }
        public bool Visible { get; set; }
    }
}
