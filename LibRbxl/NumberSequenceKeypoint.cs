namespace LibRbxl
{
    public struct NumberSequenceKeypoint
    {
        public double Time { get; }
        public double Value { get; }
        public double Envelope { get; }

        public NumberSequenceKeypoint(double time, double value, double envelope)
        {
            Time = time;
            Value = value;
            Envelope = envelope;
        }
    }
}