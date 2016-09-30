namespace LibRbxl.Instances
{
    public class VehicleSeat : SeatBase
    {
        public override string ClassName => "VehicleSeat";

        public int AreHingesDetected { get; set; }
        public bool Disabled { get; set; }
        public bool HeadsUpDisplay { get; set; }
        public float MaxSpeed { get; set; }
        public Humanoid Occupant { get; set; }
        public int Steer { get; set; }
        public int Throttle { get; set; }
        public float Torque { get; set; }
        public float TurnSpeed { get; set; }
    }
}
