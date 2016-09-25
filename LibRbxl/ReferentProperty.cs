using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class ReferentProperty : Property<int>
    {
        public ReferentProperty(string name, int value) : base(name, PropertyType.Referent, value)
        {
        }
    }
}
