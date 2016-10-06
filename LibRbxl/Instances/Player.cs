namespace LibRbxl.Instances
{
    public class Player : Instance
    {
        public override string ClassName => "Player";

        public int AccountAge { get; set; }
        public bool AutoJumpEnabled { get; set; }
        public float CameraMaxZoomDistance { get; set; }
        public float CameraMinZoomDistance { get; set; }
        public CameraMode CameraMode { get; set; }
        public bool CanLoadCharacterAppearance { get; set; }
        public Model Character { get; set; }
        public string CharacterAppearance { get; set; }
        public int CharacterAppearanceId { get; set; }
        public DevCameraOcclusionMode DevCameraOcclusionMode { get; set; }
        public DevComputerCameraMovementMode DevComputerCameraMode { get; set; }
        public DevComputerMovementMode DevComputerMovementMode { get; set; }
        [RobloxIgnore]
        public bool DevEnableMouseLock { get; set; }
        public DevTouchCameraMovementMode DevTouchCameraMode { get; set; }
        public DevTouchMovementMode DevTouchMovementMode { get; set; }
        public int FollowUserId { get; set; }
        public float HealthDisplayDistance { get; set; }
        public MembershipType MembershipType { get; set; }
        public float NameDisplayDistance { get; set; }
        public bool Neutral { get; set; }
        public SpawnLocation RespawnLocation { get; set; }
        public Team Team { get; set; }
        public BrickColor TeamColor { get; set; }
        public int UserId { get; set; }
    }
}