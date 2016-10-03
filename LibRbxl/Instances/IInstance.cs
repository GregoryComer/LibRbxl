using System.Collections.Generic;

namespace LibRbxl.Instances
{
    public interface IInstance
    {
        bool Archivable { get; set; }
        ChildCollection Children { get; }
        string ClassName { get; }
        string Name { get; set; }
        Instance Parent { get; set; }
        Dictionary<string, Property> UnmanagedProperties { get; }

        Instance FindFirstChild(string name);
    }
}