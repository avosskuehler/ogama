using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Ogama.Modules.Rta;

namespace OgamaTestProject.Modules.Rta
{
    [TestClass]
    public class ProgressDialogTest
    {
       // [TestMethod]
        public void TestRunProgressDialog()
        {
            Ogama.Modules.Rta.ProgressDialog cut = new Ogama.Modules.Rta.ProgressDialog();
            cut.start();
            List<string> filternames = cut.getFilterNames();
            Assert.IsNotNull(filternames);
            Assert.IsNotNull(filternames[0]);
           
            
        }
      
    }
}
