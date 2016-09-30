namespace LibRbxl.Instances
{
    public class InputObject : Instance
    {
        public override string ClassName => "InputObject";

        public Vector3 Delta { get; set; }
        public KeyCode KeyCode { get; set; }
        public Vector3 Position { get; set; }
        public UserInputState UserInputState { get; set; }
        public UserInputType UserInputType { get; set; }
    }
}
