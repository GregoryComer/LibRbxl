using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    /// <summary>
    /// Causes the serializer to ignore this value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RobloxIgnoreAttribute : Attribute
    {

    }
}
