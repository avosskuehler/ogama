using Ogama.Modules.Rta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace OgamaTestProject
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "RtaExtension4ReplayModuleTest" und soll
    ///alle RtaExtension4ReplayModuleTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class RtaExtension4ReplayModuleTest
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
        ///Ein Test für "startRtaSession"
        ///</summary>
        [TestMethod()]
        public void startRtaSessionTest()
        {
            RtaExtension4ReplayModule target = new RtaExtension4ReplayModule(); // TODO: Passenden Wert initialisieren
            ToolStripButton btnRTA = new ToolStripButton(); // TODO: Passenden Wert initialisieren
            string selectedTrial = string.Empty; // TODO: Passenden Wert initialisieren

            target.configureRtaSettings("");

            target.startRtaSession(btnRTA, selectedTrial);
            
        }
    }
}
