using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OgamaTestProject.DataSet
{
    [TestClass]
    public class OgamaDataSetTest
    {
        [TestMethod]
        public void TestAOI()
        {
            
            Ogama.DataSet.OgamaDataSet cut = new Ogama.DataSet.OgamaDataSet();
            Assert.IsNotNull(cut);

            Ogama.DataSet.OgamaDataSet.AOIsDataTable aoiTable = new Ogama.DataSet.OgamaDataSet.AOIsDataTable();
            

        }
    }
}
