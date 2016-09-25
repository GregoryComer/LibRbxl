using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class FacesProperty : Property<Faces>
    {
        public FacesProperty(string name, Faces value) : base(name, PropertyType.Faces, value)
        {
        }
    }
}
