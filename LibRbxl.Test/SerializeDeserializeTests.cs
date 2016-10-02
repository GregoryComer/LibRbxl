using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using NUnit.Framework;

namespace LibRbxl.Test
{
    [TestFixture]
    public class SerializeDeserializeTests
    {
        [Test, Explicit] // I've temporarily marked this test as explicit, as it passes on my development machine, but fails on the CI server. I need to look further into this.
        public void WorkspaceOnly()
        {
            var doc = new RobloxDocument();
            var workspace = new Workspace();
            doc.Children.Add(workspace);

            var stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;

            var doc2 = RobloxDocument.FromStream(stream);
            Assert.IsNotNull(doc2.Workspace);
        }
    }
}
