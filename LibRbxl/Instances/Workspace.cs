namespace LibRbxl.Instances
{
    public class Workspace : Model
    {
        public override string ClassName => "Workspace";

        public bool AllowThirdPartySales { get; set; }
        public Camera CurrentCamera { get; set; }
        [RobloxIgnore]
        public double DistributedGameTime { get; set; }
        public double FallenPartsDestroyHeight { get; set; }
        public bool FilteringEnabled { get; set; }
        public float Gravity { get; set; }
        public bool PGSPhysicsSolverEnabled { get; set; }
        public bool StreamingEnabled { get; set; }
        public Terrain Terrain { get; set; }
        [RobloxProperty("ExpSolverEnabled_Replicate", PropertyType.Boolean)]
        public bool ExpSolverEnabledReplicate { get; set; } // TODO Figure out what this is
    }
}
