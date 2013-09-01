using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta;
namespace OgamaTestProject.Modules.Rta
{
    [TestClass]
    public class LoggerFacadeTest
    {
        [TestMethod]
        public void TestLogInfo()
        {
            LoggerFacade cut = new LoggerFacade();
            cut.info("hello world");

        }
    }
}
