using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta.RtaReplay;
using OgamaDao.Model.Rta;
using System.Windows.Forms;

namespace OgamaTestProject.Modules.Rta.RtaReplay
{
    [TestClass]
    public class FormRtaViewControllerTest
    {
        [TestMethod]
        public void TestOnAddTreeNode()
        {
            
            FormRtaViewController cut = new FormRtaViewController();
            RtaModel rtaModel = new RtaModel();
            RtaSettings rtaSettings = new RtaSettings();
            cut.SetRtaModel(rtaModel);
            cut.SetRtaSettings(rtaSettings);
            TreeView treeView = new TreeView();

            TreeNode parent = null;
            TreeNodeCollection nodes = treeView.Nodes;
            Assert.AreEqual(0, nodes.Count);
            Assert.AreEqual(0, rtaModel.getRtaCategories().Count);

            cut.onAddTreeNode(parent, nodes);
            Assert.AreEqual(1, nodes.Count);
            Assert.AreEqual(1, rtaModel.getRtaCategories().Count);


            parent = new TreeNode();
            cut.onAddTreeNode(parent, nodes);
            Assert.AreEqual(1, nodes.Count);
            Assert.AreEqual(1, parent.Nodes.Count);
            Assert.AreEqual(2, rtaModel.getRtaCategories().Count);


        }
    }
}
