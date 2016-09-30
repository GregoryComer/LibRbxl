namespace LibRbxl.Instances
{
    public class ScrollingFrame : GuiObject
    {
        public override string ClassName => "ScrollingFrame";

        public Vector2 AbsoluteWindowSize { get; set; }
        public string BottomImage { get; set; }
        public Vector2 CanvasPosition { get; set; }
        public UDim2 CanvasSise { get; set; }
        public string MidImage { get; set; }
        public int ScrollBarThickness { get; set; }
        public bool ScrollingEnabled { get; set; }
        public string TopImage { get; set; }
    }
}
