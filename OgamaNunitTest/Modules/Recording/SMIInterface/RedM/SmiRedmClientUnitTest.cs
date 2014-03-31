using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ogama.Modules.Recording.SMIInterface.RedM;
using Ogama.Modules.Recording;

namespace OgamaNunitTest.Modules.Recording.SMIInterface.RedM
{
	[TestFixture]	
	public class SmiRedmClientUnitTest
	{

		private SMIRedMClient cut = null;
		private int screenWidth = 1680;
		private int screenHeight = 1080;

		[SetUp]
		public void setup()
		{
			cut = new SMIRedMClient(true, screenWidth, screenHeight);
			Assert.IsNotNull(cut);
		}

		[Test]
		public void testExtractTrackerDataWithZeroValues()
		{
			EyeTrackingController.SampleStruct testdata = getTestdata();
			testdata.rightEye.gazeX = 0;
			testdata.rightEye.gazeY = 0;
			testdata.leftEye.gazeX = 0;
			testdata.leftEye.gazeY = 0;
	
			GazeData gazeData = cut.ExtractTrackerData(testdata);

			Assert.IsNotNull(gazeData);

			Assert.AreEqual(0, gazeData.GazePosX);
			Assert.AreEqual(0, gazeData.GazePosY);
		
		}

		[Test]
		public void testExtractTrackerDataBothEyes()
		{
				EyeTrackingController.SampleStruct testdata = getTestdata();
				testdata.rightEye.gazeX = 800;
				testdata.rightEye.gazeY = 10;	
				
				testdata.leftEye.gazeX = 900;
				testdata.leftEye.gazeY = 10;

				GazeData gazeData = cut.ExtractTrackerData(testdata);
				Assert.IsNotNull(gazeData);

				assertEqualDecimal(850f, gazeData.GazePosX);
				assertEqualDecimal(10, gazeData.GazePosY);
				
		}

		

		[Test]
		public void testExtractTrackerDataLeftEyeZero()
		{
			EyeTrackingController.SampleStruct testdata = getTestdata();
			testdata.leftEye.gazeX = 0;
			testdata.leftEye.gazeY = 0;
			testdata.rightEye.gazeX = 1000;
			testdata.rightEye.gazeY = 1000;

			GazeData gazeData = cut.ExtractTrackerData(testdata);
			
			Assert.IsNotNull(gazeData);
			assertEqualDecimal(500f, gazeData.GazePosX);
			assertEqualDecimal(500f, gazeData.GazePosY);
			
		}

		[Test]
		public void testExtractTrackerDataRightEyeZero()
		{
			EyeTrackingController.SampleStruct testdata = getTestdata();
			testdata.leftEye.gazeX = 10;
			testdata.leftEye.gazeY = 10;
			testdata.rightEye.gazeX = 0;
			testdata.rightEye.gazeY = 0;

			GazeData gazeData = cut.ExtractTrackerData(testdata);

			Assert.IsNotNull(gazeData);
			assertEqualDecimal(5f, gazeData.GazePosX);
			assertEqualDecimal(5f, gazeData.GazePosY);

		}



		protected void assertEqualDecimal(float f1, float? f2)
		{
			decimal d1 = Math.Round((decimal)f1, 1);
			decimal d2 = Math.Round((decimal)f2, 1);
			Assert.AreEqual(d1, d2);
		}

		protected EyeTrackingController.SampleStruct getTestdata()
		{
			EyeTrackingController.SampleStruct testdata = new EyeTrackingController.SampleStruct();
			testdata.leftEye.diam = 10;
			testdata.leftEye.eyePositionX = 2;
			testdata.leftEye.eyePositionY = 3;
			testdata.leftEye.eyePositionZ = 4;

			testdata.rightEye.diam = 10;
			testdata.rightEye.eyePositionX = 8;
			testdata.rightEye.eyePositionY = 9;
			testdata.rightEye.eyePositionZ = 10;

			testdata.leftEye.gazeX = 10;
			testdata.leftEye.gazeY = 10;
			testdata.rightEye.gazeX = 20;
			testdata.rightEye.gazeY = 10;

			testdata.planeNumber = 13;
			testdata.timestamp = 12340000;
			return testdata;
		}
	}
}
