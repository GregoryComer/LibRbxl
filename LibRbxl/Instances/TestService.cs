namespace LibRbxl.Instances
{
    public class TestService : Instance, IService
    {
        public override string ClassName => "TestService";

        public bool AutoRuns { get; set; }
        public string Description { get; set; }
        [RobloxIgnore]
        public int ErrorCount { get; set; }
        [RobloxIgnore]
        public bool Is30FpsThrottleEnabled { get; set; }
        [RobloxIgnore]
        public bool IsPhysicsEnvironmentThrottled { get; set; }
        public bool IsSleepAllowed { get; set; }
        [RobloxIgnore]
        public int NumberOfPlayers { get; set; }
        public double SimulateSecondsLag { get; set; }
        [RobloxIgnore]
        public int TestCount { get; set; }
        public double Timeout { get; set; }
        [RobloxIgnore]
        public int WarnCount { get; set; }
    }
}
