namespace LibRbxl.Instances
{
    public class Tool : BackpackItem
    {
        public override string ClassName => "Tool";

        public bool CanBeDropped { get; set; }
        public bool Enabled { get; set; }
        public CFrame Grip { get; set; }
        public Vector3 GripForward { get; set; }
        public Vector3 GripPos { get; set; }
        public Vector3 GripRight { get; set; }
        public Vector3 GripUp { get; set; }
        public bool ManualActivationOnly { get; set; }
        public bool RequiresHandle { get; set; }
        public string ToolTip { get; set; }
    }
}
