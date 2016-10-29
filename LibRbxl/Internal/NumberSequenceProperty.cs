using LibRbxl.Instances;

namespace LibRbxl.Internal
{
    public class NumberSequenceProperty : Property<NumberSequence>
    {
        public NumberSequenceProperty(string name, NumberSequence value) : base(name, PropertyType.NumberSequence, value)
        {
        }
    }
}