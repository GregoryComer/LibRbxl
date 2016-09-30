namespace LibRbxl.Instances
{
    public class TextButton : GuiButton
    {
        public override string ClassName => "TextButton";
        
        public Font Font { get; set; }
        public FontSize FontSize { get; set; }
        public string Text { get; set; }
        public Vector2 TextBounds { get; set; }
        public Color3 TextColor3 { get; set; }
        [RobloxIgnore]
        public bool TextFits { get; set; }
        public bool TextScaled { get; set; }
        public Color3 TextStrokeColor3 { get; set; }
        public float TextStrokeTransparency { get; set; }
        public float TextTransparency { get; set; }
        public bool TextWrapped { get; set; }
        public TextXAlignment TextXAlignment { get; set; }
        public TextYAlignment TextYAlignment { get; set; }
    }
}
