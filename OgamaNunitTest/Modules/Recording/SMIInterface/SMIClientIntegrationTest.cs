using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace OgamaTrackerTests.Modules.Recording.SMIInterface
{
  //[TestClass]
  [TestFixture]
  public class SMIClientIntegrationTest
  {
		
		Ogama.Modules.Recording.SMIInterface.SMIClient cut = new Ogama.Modules.Recording.SMIInterface.SMIClient(1000, 500);
    //[TestMethod]
    [Test]
    public void TestExtractTrackerData()
    {
			string testdata = "ET_SPL 91555722812 b 420 420 564 564 17.219431 17.855097 17.219431 17.855097 -53.704  9.674 13.589 15.935 624.140 612.472 419.313368 680.213202 455.167761 443.716013 4.72 4.72 3";

			Ogama.Modules.Recording.GazeData gazeData = parseData(testdata);
			Assert.IsNotNull(gazeData);
			Assert.AreEqual(91555722812, gazeData.Time);

    }

		

		//[Test]
		public void TestExtractNoEyeData()
		{
			string testdata = "ET_SPL 92880206786 b 0 0 0 0 15.894208 16.633007 15.894208 16.633007 -60.177  3.029 20.306 21.726 606.911 595.593 391.127022 657.610615 417.781071 409.706189 4.24 4.27 3";
			Ogama.Modules.Recording.GazeData gazeData = parseData(testdata);
			Assert.IsNotNull(gazeData);
			Assert.AreEqual(92880206786, gazeData.Time);
		}

		protected Ogama.Modules.Recording.GazeData parseData(string testdata)
		{
			

			Ogama.Modules.Recording.SMIInterface.SMISetting settings = new Ogama.Modules.Recording.SMIInterface.SMISetting();
			settings.AvailableEye = Ogama.Modules.Recording.SMIInterface.AvailableEye.Both;
			cut.Settings = settings;


			Ogama.Modules.Recording.GazeData gazeData = cut.ExtractTrackerData(testdata);
			return gazeData;
		}
  }
}
