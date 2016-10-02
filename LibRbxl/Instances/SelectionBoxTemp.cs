namespace LibRbxl.Instances
{
    public class SelectionBox : PVAdornment
    {
        public override string ClassName => "SelectionBox";

        public float LineThickness { get; set; }
        public Color3 SurfaceColor3 { get; set; }
        public float SurfaceTransparency { get; set; }
    }
}
