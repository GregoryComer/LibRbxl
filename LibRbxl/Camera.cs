using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class Camera : Instance
    {
        public override string ClassName => "Camera";

        public CFrame CFrame { get; set; }
        public ICameraSubject CameraSubject { get; set; }
        public CameraType CameraType { get; set; }
        public float FieldOfView { get; set; }
        public CFrame Focus { get; set; }
        public bool HeadLocked { get; set; }
        public float HeadScale { get; set; }
        public Vector2 ViewportSize { get; set; }
    }
}
