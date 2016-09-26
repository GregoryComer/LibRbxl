using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class RobloxDocument
    {
        public RobloxDocument()
        {
            ReferentProvider = new ReferentProvider();
        }

        public List<RobloxObject> Objects { get; set; }
        public ReferentProvider ReferentProvider { get; }

        public static RobloxDocument FromStream(Stream stream)
        {
            
        }

        public static RobloxDocument FromFile(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            var document = FromStream(fileStream);
            fileStream.Close();
            return document;
        }
    }
}
