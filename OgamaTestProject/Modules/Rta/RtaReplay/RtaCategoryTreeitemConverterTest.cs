using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;

namespace OgamaTestProject
{
    
    
    [TestClass()]
    public class RtaCategoryTreeitemConverterTest
    {

        [TestMethod]
        public void TestConvert()
        {
            RtaCategoryTreeitemConverter cut = new RtaCategoryTreeitemConverter();
            
            RtaModel model = new RtaModel();

            int N = 3;
            int M = 2;
            for (int i = 0; i < N; i++)
            {
                RtaCategory cat = createTestCategory(i);
                for (int k = 0; k < M; k++)
                {
                    RtaCategory cat1 = createTestCategory(i*k+1);
                    cat.Add(cat1);
                }
                model.getRtaCategories().Add(cat);
            }

            TreeView treeView = new TreeView();
            
            TreeNodeCollection nodes = treeView.Nodes;

            cut.LoadModelIntoTreeNodeCollection(model, nodes);

            Assert.IsTrue(nodes.Count == N);

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
