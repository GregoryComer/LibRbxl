namespace LibRbxl.Instances
{
    public class Debris : Instance, ISingleton
    {
        public override string ClassName => "Debris";

        public int MaxItems { get; set; }
    }
}
