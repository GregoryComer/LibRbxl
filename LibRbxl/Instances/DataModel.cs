namespace LibRbxl.Instances
{
    public class DataModel : ServiceProvider
    {
        public override string ClassName => "DataModel";

        public int CreatorId { get; set; }
        public CreatorType CreatorType { get; set; }
        public GearGenreSetting GearGenreSetting { get; set; }
        public Genre Genre { get; set; }
        public string JobId { get; set; }
        public int PlaceId { get; set; }
        public int PlaceVersion { get; set; }
        public string VIPServerId { get; set; }
        public int VIPServerOwnerId { get; set; }
        public Workspace Workspace { get; set; }
    }
}
