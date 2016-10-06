namespace LibRbxl.Instances
{
    public class StarterPlayer : Instance, ISingleton
    {
        public override string ClassName => "StarterPlayer";

        public bool AutoJumpEnabled { get; set; }
        public float CameraMaxZoomDistance { get; set; }
        public float CameraMinZoomDistance { get; set; }
        public CameraMode CameraMode { get; set; }
        public DevCameraOcclusionMode DevCameraOcclusionMode { get; set; }
        public DevComputerCameraMovementMode DevComputerCameraMovementMode { get; set; }
        public DevComputerMovementMode DevComputerMovementMode { get; set; }
        [RobloxIgnore]
        public bool DevEnableMouseLock { get; set; }
        public DevTouchCameraMovementMode DevTouchCameraMovementMode { get; set; }
        public DevTouchMovementMode DevTouchMovementMode { get; set; }
        public bool EnableMouseLockOption { get; set; }
        public float HealthDisplayDistance { get; set; }
        public bool LoadCharacterAppearance { get; set; }
        public float NameDisplayDistance { get; set; }
    }
}
