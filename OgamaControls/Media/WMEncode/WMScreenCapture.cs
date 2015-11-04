//using System;
//using System.Collections.Generic;
//using System.Text;
//using WMEncoderLib;
//using WMPREVIEWLib;
//using DirectShowLib;

//namespace OgamaControls
//{
//  /// <summary>
//  /// 
//  /// </summary>
//  public class WMScreenCapture
//  {
//    /// <summary>
//    /// 
//    /// </summary>
//    public WMScreenCapture()
//    {
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="handleToCaptureWnd"></param>
//    /// <param name="handleToPreviewWnd"></param>
//    /// <param name="handleToPostviewWnd"></param>
//    public static void StartCapturing(
//      IntPtr handleToCaptureWnd, 
//      IntPtr handleToPreviewWnd,
//      IntPtr handleToPostviewWnd)
//    {
//      // Create a WMEncoder object.
//      WMEncoder encoder = new WMEncoder();

//      // Retrieve the source group collection and add a source group. 
//      IWMEncSourceGroup2 SrcGrp;
//      IWMEncSourceGroupCollection SrcGrpColl;
//      SrcGrpColl = encoder.SourceGroupCollection;
//      SrcGrp = (IWMEncSourceGroup2)SrcGrpColl.Add("SG_1");

//      // Add a video and audio source to the source group.
//      IWMEncVideoSource2 SrcVid;
//      //IWMEncAudioSource SrcAud;
//      SrcVid = (IWMEncVideoSource2)SrcGrp.AddSource(WMENC_SOURCE_TYPE.WMENC_VIDEO);
//      //SrcAud = SrcGrp.AddSource(WMENC_SOURCE_TYPE.WMENC_AUDIO);

//      // Identify the source files to encode.
//      SrcVid.SetInput("ScreenCap://ScreenCapture1", "", "");
//      //SrcAud.SetInput("Device://Default_Audio_Device");

//      // Set input size
//      IPropertyBag ipb = (IPropertyBag)SrcVid;
//      object value;
//      value = false;
//      ipb.Write("Screen", ref value);
//      value = handleToCaptureWnd;
//      ipb.Write("CaptureWindow", ref value);

//   //   CComBSTR bstrBuffer = "WindowTitle";
//   //CComBSTR bstrTop = "Top";
//   //CComBSTR bstrLeft = "Left"; 
//   //CComBSTR bstrRight= "Right"; 
//   //CComBSTR bstrBottom = "Bottom"; 
//   //CComBSTR bstrFrame = "Frame"; 
//   //CComBSTR bstrFlash = "FlashRect";

//   //valueProp = L"Inbox - Microsoft Outlook";
//   //valueLeft = 0;
//   //valueTop = 0;
//   //valueRight = 1024;
//   //valueBottom = 768;
//   //valueFrame = 0.2000;
//   //valueFlash.boolVal = VARIANT_TRUE;
//   //valueFlash.vt = VT_BOOL; 

//      // Choose a profile from the collection.
//      IWMEncProfileCollection ProColl;
//      IWMEncProfile Pro;
//      long length;

//      ProColl = encoder.ProfileCollection;
//      length = ProColl.Count;
//      for (int i = 0; i < length; i++)
//      {
//        Console.WriteLine(ProColl.Item(i).Name);
//      }

//      for (int i = 0; i < length; i++)
//      {
//        Pro = ProColl.Item(i);
//        // Screen Video/Audio High (CBR)
//        //if (Pro.Name == "Windows Media Video 8 for Local Area Network (384 Kbps)")
//        //if (Pro.Name == "Screen Video Medium (CBR)")
//        //if (Pro.Name == "Screen Video/Audio High (CBR)")
//        //if (Pro.Name == "Screen Video/Audio")
//        //if (Pro.Name == "Screen Video (CBR)")
//        if (Pro.Name == "Windows Media Video 8 for Dial-up Modems (56 Kbps)")
//        {
//          SrcGrp.set_Profile(Pro);
//          break;
//        }
//      }

//      //// Fill in the description object members.
//      //IWMEncDisplayInfo Descr;
//      //Descr = encoder.DisplayInfo;
//      //Descr.Author = "Ogama application";
//      //Descr.Copyright = "no Copyright";
//      //Descr.Description = "Screencopy";
//      //Descr.Rating = "no rating";
//      //Descr.Title = "Ogama screencopy";

//      //// Add an attribute to the collection.
//      //IWMEncAttributes Attr;
//      //Attr = encoder.Attributes;
//      //Attr.Add("URL", "www.adnare.com");

//      //// Specify a file object in which to save encoded content.
//      //IWMEncFile File;
//      //File = encoder.File;
//      //File.LocalFileName = @"C:\OutputFile.avi";
      
//      //// Crop 2 pixels from each edge of the video image.
//      //SrcVid.CroppingBottomMargin = 2;
//      //SrcVid.CroppingTopMargin = 2;
//      //SrcVid.CroppingLeftMargin = 2;
//      //SrcVid.CroppingRightMargin = 2;

//      // Retrieve an IWMEncDataViewCollection object from the
//      // video source object to display a postview.
//      IWMEncDataViewCollection DVColl_Postview;
//      DVColl_Postview = SrcVid.PostviewCollection;

//      // Create a WMEncDataView object.
//      WMEncDataView Postview;
//      Postview = new WMEncDataView();

//      // Add the WMEncDataView object to the collection.
//      int lPostviewStream;
//      lPostviewStream = DVColl_Postview.Add(Postview);


//      //// Retrieve an IWMEncDataViewCollection object from the
//      //// video source object to display a postview.
//      //IWMEncDataViewCollection DVColl_Preview;
//      //DVColl_Preview = SrcVid.PreviewCollection;

//      //// Create a WMEncDataView object.
//      //WMEncDataView Preview;
//      //Preview = new WMEncDataView();

//      //// Add the WMEncDataView object to the collection.
//      //int lPreviewStream;
//      //lPreviewStream = DVColl_Preview.Add(Preview);

//      // Start encoding.
//      encoder.Start();

//      // Display the postview in a panel named PostviewPanel.
//      Postview.SetViewProperties(lPostviewStream, (int)handleToPostviewWnd);
//      Postview.StartView(lPostviewStream);

//      //// Display the postview in a panel named PostviewPanel.
//      //Preview.SetViewProperties(lPreviewStream, handleToPreviewWnd.ToInt32());
//      //Preview.StartView(lPreviewStream);

//    }
//  }
//}
