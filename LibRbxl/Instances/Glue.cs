namespace LibRbxl.Instances
{
    public class Glue : JointInstance
    {
        public override string ClassName => "Glue";

        public Vector3 F0 { get; set; }
        public Vector3 F1 { get; set; }
        public Vector3 F2 { get; set; }
        public Vector3 F3 { get; set; }
    }
}
