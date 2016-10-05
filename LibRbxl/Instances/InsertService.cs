namespace LibRbxl.Instances
{
    public class InsertService : Instance, IService
    {
        public override string ClassName => "InsertService";

        public bool AllowClientInsertModels { get; set; }
        public bool AllowInsertFreeModels { get; set; }
    }
}
