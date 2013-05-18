using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Collections.Generic;
using Ogama.Modules.Rta.Model;

namespace OgamaTestProject 
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "RtaCategoryModelTest" und soll
    ///alle RtaCategoryModelTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class RtaCategoryModelTest
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
        ///Ein Test für "RtaCategoryModel-Konstruktor"
        ///</summary>
        [TestMethod()]
        public void RtaCategoryModelConstructorTest()
        {
            RtaCategoryModel cut = new RtaCategoryModel();
            Assert.IsNotNull(cut);
        }

        [TestMethod]
        public void TestAddCategory()
        {
            RtaCategoryModel cut = new RtaCategoryModel();
            RtaCategory category = new RtaCategory();
            int nChildren = cut.CountChildren();
            Assert.AreEqual(0, nChildren);
            
            cut.Add(category);

            nChildren = cut.CountChildren();
            Assert.AreEqual(1, nChildren);
        }

        [TestMethod]
        public void TestRemoveCategory()
        {
            RtaCategoryModel cut = new RtaCategoryModel();
            RtaCategory category = new RtaCategory();
            Assert.AreEqual(0, cut.CountChildren());
            cut.RemoveCategory(category);
            Assert.AreEqual(0, cut.CountChildren());
            cut.RemoveCategory(null);
            Assert.AreEqual(0, cut.CountChildren());

            cut.Add(category);
            Assert.AreEqual(1, cut.CountChildren());
            cut.RemoveCategory(category);
            Assert.AreEqual(0, cut.CountChildren());
        }

        [TestMethod]
        public void TestWriteToXml()
        {
            //@see: http://www.agiledeveloper.com/articles/XMLSerialization.pdf

            RtaCategoryModel cut = getTestEntity();

            
            string xml = cut.WriteToXml();
            Assert.IsNotNull(xml);

        }

    

        [TestMethod]
        public void TestWriteAndReadXmlFile()
        {
            RtaCategoryModel cut = getTestEntity();
            int nChildren1 = cut.CountChildren();

            string filename = "c:/temp/testRta.xml";
            cut.WriteToXmlFile(filename);
            cut.ReadFromXmlFile(filename);

            Assert.AreEqual(nChildren1, cut.CountChildren());

        }

        [TestMethod]
        public void TestReadEmptyXmlFile()
        {
            RtaCategoryModel cut = getTestEntity();
            string filename = System.IO.Path.GetTempFileName();
            filename += ".xml";
            System.IO.FileStream fstream = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate);
            fstream.Close();

            try
            {
                cut.ReadFromXmlFile(filename);
            }
            catch (System.InvalidOperationException e)
            {
                Assert.Fail(e.ToString());
            }
 
        }

        [TestMethod]
        public void TestReadNonExistingXmlFile()
        {
            try
            {
                getTestEntity().ReadFromXmlFile("c:/temp/12309uiui23900.xml");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Assert.Fail(e.ToString());
            } 
        }

        [TestMethod]
        public void TestModelHandling()
        {
            RtaCategoryModel model = new RtaCategoryModel();
            RtaCategory cat01 = new RtaCategory("Cat01");
            model.Add(cat01);

            RtaCategory cat02 = new RtaCategory("Cat02");
            model.Add(cat02, cat01);

            model.WriteToXmlFile("c:/temp/testRtaModel.xml");

            
            
        }


        private static RtaCategoryModel getTestEntity()
        {
            RtaCategoryModel cut = new RtaCategoryModel();
            int N = 3;
            for (int i = 0; i < N; i++)
            {
                RtaCategory c = new RtaCategory("Node:" + i);
                RtaCategory c2 = new RtaCategory("subcat:" + i);
                c.Add(c2);
                cut.Add(c);
                
                RtaEvent e1 = new RtaEvent();
                e1.fkRtaCategory = c;
                e1.Xstart = 10;
                e1.Xend = 50;

                cut.Add(e1);

            }
                
            return cut;
        }

        /// <summary>
        ///Ein Test für "visit"
        ///</summary>
        [TestMethod()]
        public void visitTest()
        {
            RtaCategoryModel target = new RtaCategoryModel();
            RtaCategory c1 = new RtaCategory("c1");
            RtaCategory c12 = new RtaCategory("c12");
            c1.Add(c12);
            target.Add(c1);
            RtaCategory c2 = new RtaCategory("c2");
            target.Add(c2);

            VisitorImpl visitor = new VisitorImpl();
            target.visit(visitor);

            Assert.AreEqual(3, visitor.list.Count);
            
            RtaCategory[] a = visitor.list.ToArray();
            Assert.AreEqual("c1", a[0].name);
            Assert.AreEqual("c12", a[1].name);
            Assert.AreEqual("c2", a[2].name);

        }

        class VisitorImpl : IRtaCategoryVisitor
        {
            public List<RtaCategory> list = new List<RtaCategory>();

            public void onVisit(RtaCategory rtaCategory)
            {
                list.Add(rtaCategory);
            }
            public void onVisit(RtaEvent rtaEvent)
            {

            }
        }

    }
}
