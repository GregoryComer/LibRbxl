using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using LibRbxl;

namespace LibRbxl
{
    public abstract class BasePart : PVInstance
    {
        public bool Anchored { get; set; }
        public float BackParamA { get; set; }
        public float BackParamB { get; set; }
        public SurfaceType BackSurface { get; set; }
        public InputType BackSurfaceInput { get; set; }
        public float BottomParamA { get; set; }
        public float BottomParamB { get; set; }
        public SurfaceType BottomSurface { get; set; }
        public InputType BottomSurfaceInput { get; set; }
        public BrickColor BrickColor { get; set; }
        public CFrame CFrame { get; set; }
        public bool CanCollide { get; set; }
        public float Density { get; set; }
        public float Elasticity { get; set; }
        public float ElasticityWeight { get; set; }
        [RobloxProperty("formFactorRaw", PropertyType.Enumeration)] // Documentation has this as a member of FormFactorPart, which is a descendent of BasePart, but I'm seeing on Parts. Need to look more into this. TODO
        public FormFactor FormFactor { get; set; }
        public float Friction { get; set; }
        public float FrictionWeight { get; set; }
        public float FrontParamA { get; set; }
        public float FrontParamB { get; set; }
        public SurfaceType FrontSurface { get; set; }
        public InputType FrontSurfaceInput { get; set; }
        public float LeftParamA { get; set; }
        public float LeftParamB { get; set; }
        public SurfaceType LeftSurface { get; set; }
        public InputType LeftSurfaceInput { get; set; }
        public bool Locked { get; set; }
        public Material Material { get; set; }
        public Vector3 Position { get; set; }
        public float Reflectance { get; set; }
        [RobloxIgnore]
        public int ResizeIncrement { get; set; } // TODO: Is this serialized?
        [RobloxIgnore]
        public Faces ResizableFaces { get; set; } // TODO: Is this serialized?
        public float RightParamA { get; set; }
        public float RightParamB { get; set; }
        public SurfaceType RightSurface { get; set; }
        public InputType RightSurfaceInput { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 RotVelocity { get; set; }
        public Vector3 Size { get; set; }
        public float SpecificGravity { get; set; }
        public float TopParamA { get; set; }
        public float TopParamB { get; set; }
        public SurfaceType TopSurface { get; set; }
        public InputType TopSurfaceInput { get; set; }
        public float Transparency { get; set; }
        public Vector3 Velocity { get; set; }
    }
}
