using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public struct NumberSequence
    {
        public NumberSequenceKeypoint[] Keypoints { get; }

        public NumberSequence(NumberSequenceKeypoint[] keypoints)
        {
            Keypoints = keypoints;
        }

        public NumberSequence(double val) : this(new [] { new NumberSequenceKeypoint(0, val, 0), new NumberSequenceKeypoint(1, val, 0) })
        {
            
        }

        public NumberSequence(double n0, double n1) : this(new[] { new NumberSequenceKeypoint(0, n0, 0), new NumberSequenceKeypoint(1, n1, 0) })
        {
            
        }
    }
}
