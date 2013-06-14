using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;

namespace OgamaTestProject
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "RtaCategoryTreeitemConverterTest" und soll
    ///alle RtaCategoryTreeitemConverterTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class RtaCategoryTreeitemConverterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Ein Test für "RtaCategoryTreeitemConverter-Konstruktor"
        ///</summary>
        //[TestMethod()]
        public void TestConvert()
        {
            RtaCategoryTreeitemConverter classUnderTest = new RtaCategoryTreeitemConverter();
            
            RtaCategoryModel model = new RtaCategoryModel();
            
            model.ReadFromXmlFile("c:/temp/testRta.xml");

            TreeView treeView = new TreeView();
            TreeNodeCollection nodes = treeView.Nodes;

            classUnderTest.LoadModelIntoTreeNodeCollection(model, nodes);

            Assert.IsTrue(nodes.Count > 0);

            IEnumerator e = nodes.GetEnumerator();

            while(e.MoveNext())
            {
                object item = e.Current;
                Assert.IsNotNull(item);
                Assert.IsTrue(item is TreeNode);
                TreeNode treeNode = (TreeNode)item;
                Assert.IsTrue(treeNode.Nodes.Count > 0);

            }
            

        }


    }
}
