using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.ImportExport.Common;
using System.IO;

namespace OgamaTestProject.Modules.ImportExport.Common
{
    [TestClass]
    public class ASCIISettingsTest
    {
        ASCIISettings cut = new ASCIISettings();
        
        [TestMethod]
        public void TestPreconditions()
        {
            Assert.IsNotNull(cut);
        
        }

        [TestMethod]
        public void TestProcessContent()
        {
            int numberOfImportLines = 200;
            List<string> columnHeaders = new List<string>();
            List<string[]> fileRows = new List<string[]>(); 
            string line = String.Empty;
            int counter = 0;
            int columncount = 0;
            string testdata = "";
            StreamReader importReader = null;
            try
            {
                


            }
            catch (Exception e)
            {

            }
             
            
            cut.processContent(numberOfImportLines,
                columnHeaders,
                fileRows,
                ref counter,
                ref columncount,
                importReader);

            Assert.IsNotNull(fileRows);
            Assert.IsTrue(fileRows.Count > 0);

        }

        [TestMethod]
        public void TestParseFile()
        {
            
            
            string importFile = "c:/data/projects/ogama/ogama_rta_branch-clone/OgamaTestProject/resources/Rec 04/Rec 04-All-Data.tsv";
            int numberOfImportLines = 200;
            List<string> columnHeaders = new List<string>();
            List<string[]> resultList = cut.ParseFile(importFile, numberOfImportLines, ref columnHeaders);
            Assert.IsNotNull(resultList);

            Assert.IsTrue(hasNoEmptyLines(resultList));

        }
       

        private Boolean hasNoEmptyLines(List<string[]> list)
        {
            foreach (string[] line in list)
            {
                if (line.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
