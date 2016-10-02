using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl.Instances;
using NUnit.Framework;

namespace LibRbxl.Test
{
    [TestFixture]
    public class ParentChildTests
    {
        [Test]
        public void AddingChild_SetsParent()
        {
            var workspace = new Workspace();
            var part = new Part();
            workspace.Children.Add(part);
            Assert.AreEqual(workspace, part.Parent);
        }

        [Test]
        public void SettingParent_AddsChild()
        {
            var workspace = new Workspace();
            var part = new Part();
            part.Parent = workspace;
            Assert.Contains(part, workspace.Children.ToList());
        }

        [Test]
        public void RemovingParent_RemovesChild()
        {
            var workspace = new Workspace();
            var part = new Part();
            workspace.Children.Add(part);
            part.Parent = null;
            Assert.IsEmpty(workspace.Children);
        }

        [Test]
        public void RemovingChild_RemovesParent()
        {
            var workspace = new Workspace();
            var part = new Part();
            workspace.Children.Add(part);
            workspace.Children.Remove(part);
            Assert.IsNull(part.Parent);
        }

        [Test]
        public void ChangingParent_UpdatesChildren()
        {
            var model1 = new Model();
            var model2 = new Model();
            var part = new Part();

            part.Parent = model1;
            Assert.Contains(part, model1.Children.ToList());

            part.Parent = model2;
            Assert.IsEmpty(model1.Children);
            Assert.Contains(part, model2.Children.ToList());
        }
    }
}
