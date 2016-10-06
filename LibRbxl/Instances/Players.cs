namespace LibRbxl.Instances
{
    public class Players : Instance, ISingleton
    {
        public override string ClassName => "Players";

        [RobloxIgnore]
        public bool BubbleChat { get; set; }
        public bool CharacterAutoLoads { get; set; }
        [RobloxIgnore]
        public bool ClassicChat { get; set; }
        [RobloxIgnore]
        public Player LocalPlayer { get; set; }
        [RobloxProperty("MaxPlayersInternal", PropertyType.Int32)]
        public int MaxPlayers { get; set; }
        [RobloxIgnore]
        public int NumPlayers { get; set; }
        [RobloxProperty("PreferredPlayersInternal", PropertyType.Int32)]
        public int PreferredPlayers { get; set; }
    }
}
