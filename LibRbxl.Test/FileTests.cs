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

        [Test]
        public void TestFile_CustomPhysicalProperties()
        {
            var doc = LoadTestFile("CustomPhysicalProperties.rbxl");
            var workspace = doc.Workspace;
            Assert.IsNotNull(workspace);

            var part1 = workspace.FindFirstChild("Part1") as Part;
            Assert.IsNotNull(part1);
            var part2 = workspace.FindFirstChild("Part2") as Part;
            Assert.IsNotNull(part2);
            var part3 = workspace.FindFirstChild("Part3") as Part;
            Assert.IsNotNull(part3);
            var part4 = workspace.FindFirstChild("Part4") as Part;
            Assert.IsNotNull(part4);

            Assert.IsFalse(part1.CustomPhysicalProperties.Enabled);

            Assert.IsTrue(part2.CustomPhysicalProperties.Enabled);
            Assert.AreEqual(.7f, part2.CustomPhysicalProperties.Density);
            Assert.AreEqual(.3f, part2.CustomPhysicalProperties.Friction);
            Assert.AreEqual(.5f, part2.CustomPhysicalProperties.Elasticity);
            Assert.AreEqual(1f, part2.CustomPhysicalProperties.FrictionWeight);
            Assert.AreEqual(1f, part2.CustomPhysicalProperties.ElasticityWeight);

            Assert.IsFalse(part3.CustomPhysicalProperties.Enabled);

            Assert.IsTrue(part4.CustomPhysicalProperties.Enabled);
            Assert.AreEqual(.1f, part4.CustomPhysicalProperties.Density);
            Assert.AreEqual(.2f, part4.CustomPhysicalProperties.Friction);
            Assert.AreEqual(.3f, part4.CustomPhysicalProperties.Elasticity);
            Assert.AreEqual(.4f, part4.CustomPhysicalProperties.FrictionWeight);
            Assert.AreEqual(.5f, part4.CustomPhysicalProperties.ElasticityWeight);
        }

        [Test]
        public void TestFile_ParticleEmitter()
        {
            var doc = LoadTestFile("ParticleEmitter.rbxl");
            var workspace = doc.Workspace;
            var part = workspace.FindFirstChild("Part") as Part;
            Assert.IsNotNull(part);
            var emitter = part.FindFirstChild("ParticleEmitter") as ParticleEmitter;
            Assert.IsNotNull(emitter);

            Assert.AreEqual(new NumberRange(2, 7), emitter.Lifetime);
            Assert.AreEqual(new NumberRange(1, 2), emitter.Rotation);
            Assert.AreEqual(new NumberRange(0, 6), emitter.RotSpeed);
            Assert.AreEqual(new NumberRange(5, 6), emitter.Speed);
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
