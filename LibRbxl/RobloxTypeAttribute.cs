using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RobloxTypeAttribute : Attribute
    {
        public string RobloxTypeName { get; set; }

        public RobloxTypeAttribute(string robloxTypeName)
        {
            RobloxTypeName = robloxTypeName;
        }
    }
}
