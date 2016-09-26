using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class StarterPlayer : Instance
    {
        public override string ClassName => "StarterPlayer";

        public bool AutoJumpEnabled { get; set; }
        public float CameraMaxZoomDistance { get; set; }
        public float CameraMinZoomDistance { get; set; }
        public CameraMode CameraMode { get; set; }
        public DevCameraOcclusionMode DevCameraOcclusionMode { get; set; }
        public DevComputerCameraMovementMode DevComputerCameraMode { get; set; }
        public DevComputerMovementMode DevComputerMovementMode { get; set; }
        public bool DevEnableMouseLock { get; set; }
        public DevTouchCameraMovementMode DevTouchCameraMode { get; set; }
        public DevTouchMovementMode DevTouchMovementMode { get; set; }
        public bool EnableMouseLockOption { get; set; }
        public float HealthDisplayDistance { get; set; }
        public bool LoadCharacterAppearance { get; set; }
        public float NameDisplayDistance { get; set; }
    }
}
