namespace LibRbxl.Instances
{
    public class ImageLabel : GuiLabel
    {
        public override string ClassName => "ImageLabel";

        public string Image { get; set; }
        public Color3 ImageColor3 { get; set; }
        public Vector2 ImageRectOffset { get; set; }
        public Vector2 ImageRectSize { get; set; }
        public float ImageTransparency { get; set; }
        public ScaleType ScaleType { get; set; }
        public Rect SliceCenter { get; set; }
    }
}
