namespace LibRbxl.Instances
{
    public struct Ray
    {
        public Vector3 Direction { get; }
        public Vector3 Origin { get; }

        public Ray Unit => new Ray(Direction, Origin.Unit);

        public Ray(Vector3 origin, Vector3 direction)
        {
            Direction = direction;
            Origin = origin;
        }
    }
}
