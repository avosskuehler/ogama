using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;
using System.Collections.Generic;

namespace OgamaTestProject
{
    
    
    [TestClass()]
    public class RtaCategoryTreeitemConverterTest
    {

        RtaCategoryTreeitemConverter cut = new RtaCategoryTreeitemConverter();


        [TestMethod]
        public void TestLoadFlatModelIntoTreeNodeCollection()
        {
            int N = 2;
            int M = 1;
            RtaModel model = getModel(N, M);
            TreeView treeView = new TreeView();

            TreeNodeCollection nodes = treeView.Nodes;

            cut.LoadFlatModelIntoTreeNodeCollection(model, nodes);

            Assert.AreEqual(N, nodes.Count);
            IEnumerator e = nodes.GetEnumerator();
            while (e.MoveNext())
            {
                object item = e.Current;
                Assert.IsNotNull(item);
                Assert.IsTrue(item is TreeNode);
                TreeNode treeNode = (TreeNode)item;
                Assert.AreEqual(M, treeNode.Nodes.Count);
            }
            
        }
        
        [TestMethod]
        public void TestConvert()
        {
    
            int N = 1;
            int M = 1;
            RtaModel model = getModel(N, M);

            TreeView treeView = new TreeView();
            
            TreeNodeCollection nodes = treeView.Nodes;

            cut.LoadModelIntoTreeNodeCollection(model, nodes);

            Assert.AreEqual(N, nodes.Count);

            IEnumerator e = nodes.GetEnumerator();
            
            while(e.MoveNext())
            {
                object item = e.Current;
                Assert.IsNotNull(item);
                Assert.IsTrue(item is TreeNode);
                TreeNode treeNode = (TreeNode)item;
                Assert.AreEqual(M,treeNode.Nodes.Count);

            }
            

        }

        private static RtaModel getModel(int N, int M)
        {
            RtaModel model = new RtaModel();

            for (int i = 0; i < N; i++)
            {
                RtaCategory cat = createTestCategory(i);
                model.getRtaCategories().Add(cat);
                for (int k = 0; k < M; k++)
                {
                    RtaCategory cat1 = createTestCategory(i * k + 1);
                    cat1.parent = cat;
                    model.getRtaCategories().Add(cat1);
                }
            }
            return model;
        }

        private static RtaCategory createTestCategory(int i)
        {
            RtaCategory cat = new RtaCategory();
            cat.ID = Guid.NewGuid();
            cat.name = "Name:" + i;
            cat.show = true;
            cat.Version = 0;
            return cat;
        }


    }
}
