namespace LibRbxl.Instances
{
    public abstract class BasePart : PVInstance
    {
        protected BasePart()
        {
            CustomPhysicalProperties = new PhysicalProperties(false);
        }
        
        public bool Anchored { get; set; }
        public float BackParamA { get; set; }
        public float BackParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType BackSurface { get; set; }
        [RobloxIgnore]
        public InputType BackSurfaceInput { get; set; }
        [RobloxIgnore]
        public float BottomParamA { get; set; }
        [RobloxIgnore]
        public float BottomParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType BottomSurface { get; set; }
        [RobloxIgnore]
        public InputType BottomSurfaceInput { get; set; }
        public BrickColor BrickColor { get; set; }
        [RobloxIgnore]
        public CFrame CFrame { get; set; }
        [RobloxIgnore]
        public bool CanCollide { get; set; }
        [RobloxIgnore]
        public PhysicalProperties CustomPhysicalProperties { get; set; }
        [RobloxIgnore]
        //[RobloxProperty("formFactorRaw", PropertyType.Enumeration)] // Documentation has this as a member of FormFactorPart, which is a descendent of BasePart, but I'm seeing on Parts. Need to look more into this. TODO
        public FormFactor FormFactor { get; set; }
        [RobloxIgnore]
        public float FrontParamA { get; set; }
        [RobloxIgnore]
        public float FrontParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType FrontSurface { get; set; }
        [RobloxIgnore]
        public InputType FrontSurfaceInput { get; set; }
        [RobloxIgnore]
        public float LeftParamA { get; set; }
        [RobloxIgnore]
        public float LeftParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType LeftSurface { get; set; }
        [RobloxIgnore]
        public InputType LeftSurfaceInput { get; set; }
        [RobloxIgnore]
        public bool Locked { get; set; }
        [RobloxIgnore]
        public Material Material { get; set; }
        [RobloxIgnore]
        public Vector3 Position {
            get { return CFrame.Position; }
            set { CFrame = new CFrame(value, CFrame.Matrix);}
        }
        [RobloxIgnore]
        public float Reflectance { get; set; }
        [RobloxIgnore]
        public int ResizeIncrement { get; set; } // TODO: Is this serialized?
        [RobloxIgnore]
        public Faces ResizableFaces { get; set; } // TODO: Is this serialized?
        [RobloxIgnore]
        public float RightParamA { get; set; }
        [RobloxIgnore]
        public float RightParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType RightSurface { get; set; }
        [RobloxIgnore]
        public InputType RightSurfaceInput { get; set; }
        [RobloxIgnore]
        public Vector3 Rotation { get; set; }
        [RobloxIgnore]
        public Vector3 RotVelocity { get; set; }
        //[RobloxProperty("size", PropertyType.Vector3)]
        [RobloxIgnore]
        public Vector3 Size { get; set; }
        [RobloxIgnore]
        public float SpecificGravity { get; set; }
        [RobloxIgnore]
        public float TopParamA { get; set; }
        [RobloxIgnore]
        public float TopParamB { get; set; }
        [RobloxIgnore]
        public SurfaceType TopSurface { get; set; }
        [RobloxIgnore]
        public InputType TopSurfaceInput { get; set; }
        [RobloxIgnore]
        public float Transparency { get; set; }
        [RobloxIgnore]
        public Vector3 Velocity { get; set; }
    }
}
