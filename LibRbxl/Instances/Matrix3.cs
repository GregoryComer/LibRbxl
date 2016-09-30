using System;

namespace LibRbxl.Instances
{
    public struct Matrix3
    {
        private readonly float[,] _values;

        public float[,] Values => _values;
        public float R00 => _values[0, 0];
        public float R01 => _values[0, 1];
        public float R02 => _values[0, 2];
        public float R10 => _values[1, 0];
        public float R11 => _values[1, 1];
        public float R12 => _values[1, 2];
        public float R20 => _values[2, 0];
        public float R21 => _values[2, 1];
        public float R22 => _values[2, 2];

        public static Matrix3 Identity => new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public Matrix3(float[,] values)
        {
            if (values.GetLength(0) != 3 || values.GetLength(1) != 3)
                throw new ArgumentException("Values array must be of size 3x3.", nameof(values));

            _values = values;
        }

        public Matrix3(float r00, float r01, float r02, float r10, float r11, float r12, float r20, float r21, float r22) : this(new float[3, 3]
            {
                {r00, r01, r02},
                {r10, r11, r12},
                {r20, r21, r22}
            })
        {
            
        }
    }
}
