using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class UserInputService : Instance
    {
        public override string ClassName => "UserInputService";

        public bool AccelerometerEnabled { get; set; }
        public bool GamepadEnabled { get; set; }
        public bool GyroscopeEnabled { get; set; }
        public bool KeyboardEnabled { get; set; }
        public bool ModalEnabled { get; set; }
        public MouseBehavior MouseBehavior { get; set; }
        public float MouseDeltaSensitivity { get; set; }
        public bool MouseEnabled { get; set; }
        public bool MouseIconEnabled { get; set; }
        public bool TouchEnabled { get; set; }
        public bool VREnabled { get; set; }
    }
}
