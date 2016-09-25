using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class StringProperty : Property<string>
    {
        public StringProperty(string name, string value) : base(name, PropertyType.String, value)
        {
        }
    }
}
