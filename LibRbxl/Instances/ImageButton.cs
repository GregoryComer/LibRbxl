﻿namespace LibRbxl.Instances
{
    public class ImageButton : GuiButton
    {
        public override string ClassName => "ImageButton";

        public string Image { get; set; }
        public Color3 ImageColor3 { get; set; }
        public Vector2 ImageRectOffset { get; set; }
        public Vector2 ImageRectSize { get; set; }
        public float ImageTransparency { get; set; }
        public ScaleType ScaleType { get; set; }
        public Rectangle SliceCenter { get; set; } 
    }
}
