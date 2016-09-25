using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public abstract class GuiObject : GuiBase2d
    {
        public bool Active { get; set; }
        public Color3 BackgroundColor3 { get; set; }
        public float BackgroundTrasparency { get; set; }
        public Color3 BorderColor3 { get; set; }
        public int BorderSizePixel { get; set; }
        public bool ClipsDescendants { get; set; }
        public bool Draggable { get; set; }
        public GuiObject NextSelectionDown { get; set; }
        public GuiObject NextSelectionLeft { get; set; }
        public GuiObject NextSelectionRight { get; set; }
        public GuiObject NextSelectionUp { get; set; }
        public UDim2 Position { get; set; }
        public float Rotation { get; set; }
        public bool Selectable { get; set; }
        public GuiObject SelectionImageObject { get; set; }
        public UDim2 Size { get; set; }
        public SizeConstraint SizeConstraint { get; set; }
        public bool Visible { get; set; }
        public int ZIndex { get; set; }
    }
}
