using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using NUnit.Framework;

namespace LibRbxl.Test
{
    [TestFixture]
    public class FileTests
    {
        private const string TestFileDirectory = @"TestFiles";

        [Test]
        public void TestFile_Baseplate_Deserialize()
        {
            var doc = LoadTestFile("Baseplate.rbxl");
            var workspace = doc.Workspace;
            Assert.IsNotNull(workspace);
            var baseplate = (Part) workspace.Children.First(n => n.Name == "Baseplate");
            Assert.AreEqual(new Vector3(0, -10, 0), baseplate.Position);
            Assert.AreEqual(new Vector3(512, 20, 512), baseplate.Size);
            Assert.AreEqual(true, baseplate.Anchored);
            Assert.AreEqual(BrickColor.DarkStoneGrey, baseplate.BrickColor);
        }

        [Test]
        public void TestFile_1000Parts_Deserialize()
        {
            var doc = LoadTestFile("1000 Parts.rbxl");
            var workspace = doc.Workspace;
            Assert.IsNotNull(workspace);
            var partCount = workspace.Children.Count(n => n is Part);
            Assert.AreEqual(1000, partCount);
        }

        private string GetTestFilePath(string filename)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var dir = Path.GetDirectoryName(path);
            return Path.Combine(dir, TestFileDirectory, filename);
        }

        private RobloxDocument LoadTestFile(string filename)
        {
            var path = GetTestFilePath(filename);
            var document = RobloxDocument.FromFile(path);
            return document;
        }
    }
}
