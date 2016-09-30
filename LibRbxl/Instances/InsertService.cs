namespace LibRbxl.Instances
{
    public class InsertService : Instance
    {
        public override string ClassName => "InsertService";

        public bool AllowClientInsertModels { get; set; }
        public bool AllowInsertFreeModels { get; set; }
    }
}
