namespace LibRbxl.Instances
{
    public class Lighting : Instance, ISingleton
    {
        public override string ClassName => "Lighting";

        public Color3 Ambient { get; set; }
        public float Brightness { get; set; }
        public Color3 ColorShift_Bottom { get; set; }
        public Color3 ColorShift_Top { get; set; }
        public Color3 FogColor { get; set; }
        public float FogEnd { get; set; }
        public float FogStart { get; set; }
        public float GeographicLatitude { get; set; }
        public bool GlobalShadows { get; set; }
        public Color3 OutdoorAmbient { get; set; }
        public bool Outlines { get; set; }
        public string TimeOfDay { get; set; }
    }
}
