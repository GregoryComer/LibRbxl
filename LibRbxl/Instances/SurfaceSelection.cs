namespace LibRbxl.Instances
{
    public class SurfaceSelection : PartAdornment
    {
        public override string ClassName => "SurfaceSelection";

        public NormalId TargetSurface { get; set; }
    }
}
