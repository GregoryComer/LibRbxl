using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class UDim2Property : Property<UDim2>
    {
        public UDim2Property(string name, UDim2 value) : base(name, PropertyType.UDim2, value)
        {
        }
    }
}
