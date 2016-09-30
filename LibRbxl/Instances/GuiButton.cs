namespace LibRbxl.Instances
{
    public abstract class GuiButton : GuiObject
    {
        public bool AutoButtonColor { get; set; }
        public bool Modal { get; set; }
        public bool Selected { get; set; }
        public ButtonStyle Style { get; set; }
    }
}