using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Diagnostics;
using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Ogama.Modules.Rta.Model;

namespace OgamaTestProject
{
    [TestClass()]
    public class RtaCategoryTest
    {

        [TestMethod]
        public void TestToXml()
        {
            RtaCategory cut = new RtaCategory();
            cut.name = "aName";
            cut.description = "aDescription";

            RtaCategory cut2 = new RtaCategory();
            cut2.name = "aName2";
            cut2.description = "aDescription2";

            cut.Add(cut2);


            System.Xml.Serialization.XmlSerializer
                serializer =
                    new
                    System.Xml.Serialization.XmlSerializer(
                    typeof(RtaCategory));

            System.IO.StringWriter sWriter = new System.IO.StringWriter();
            
            System.IO.FileStream stream =
                new System.IO.FileStream("c:/test.xml",
                System.IO.FileMode.Create);
            
            serializer.Serialize(stream, cut);
            

        }
    }
}
