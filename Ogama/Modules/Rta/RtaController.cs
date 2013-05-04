using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ogama.Modules.Recording.Presenter;
using Ogama.ExceptionHandling;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DirectShowLib;
using DirectShowLib.DMO;
using GTHardware.Cameras.DirectShow;
using Ogama.Modules.Common.Tools;

using OgamaControls;
using System.Drawing;

namespace Ogama.Modules.Rta
{
    /// <summary>
    /// 
    /// </summary>
    public class RtaController
    {
        //components

        private IBaseFilter muxFilter = null;
        private IFileSinkFilter fileWriterFilter = null;
        private IMediaControl mediaControl = null;
        private ICaptureGraphBuilder2 captureGraphBuilder = null;
        private IGraphBuilder graphBuilder = null;
        private IBaseFilter dmoFilter = null;
        private IDMOWrapperFilter dmoWrapperFilter = null;
        private DMOHelper dmoHelper = new DMOHelper();
        private IMediaParams dmoParams = null;
        private MPData mouseX;
        private MPData mouseY;

        //settings
        private RtaSettings rtaSettings = null;


        //state
        private Boolean running = false;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean IsRunning()
        {
            return this.running;
        }

        /// <summary>
        /// 
        /// </summary>
        public RtaController()
        {

        }

        protected void Log(string s)
        {
            StreamWriter sw = System.IO.File.AppendText("c:/log.txt");
            sw.WriteLine(s);
            sw.Close();
        }

        private static List<string> availableVideoFilterNames = null;

        private IRtaControllerListener listener;

        public void register(IRtaControllerListener li)
        {
            this.listener = li;
        }

        public static bool hasNotLoadedVideoFilterNames()
        {
            if (availableVideoFilterNames == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// return a list of video filter names, which can be used by recording
        /// </summary>
        /// <returns></returns>
        public List<string> getAvailbleVideoFilterNames()
        {
            if (availableVideoFilterNames == null)
            {
                List<string> result = new List<string>();

                DsDevice[] deviceList = DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory);
                for (int i = 0; i < deviceList.Length; i++)
                {
                 
                    string filterName = deviceList[i].Name;
                    Log("filterName:" + filterName + " (" + (i + 1) + " / " + deviceList.Length + ")");
                    RtaSettings rtaTestSettings = new RtaSettings();
                    rtaTestSettings.VideoCompressorName = filterName;
                    rtaTestSettings.MonitorIndex = 0;
                    try
                    {
                        rtaTestSettings.TempFilename = System.IO.Path.GetTempFileName();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Log("Error: " + e.ToString());
                    }

                    Boolean setupOk = setup(rtaTestSettings);
                    Log("setupOk:" + setupOk);

                    if (setupOk)
                    {
                        result.Add(filterName);
                    }

                    if (this.listener != null)
                    {
                        this.listener.onVideoFilterNameDetectionProgress(i, deviceList.Length, filterName);
                    }
                }
                availableVideoFilterNames = result;
            }

            return availableVideoFilterNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtaTestSettings"></param>
        /// <returns></returns>
        public bool setup(RtaSettings rtaSettings)
        {
            this.rtaSettings = rtaSettings;
            bool useMouseFilter = false;

            try
            {
                GC.Collect();
                // Get the graphbuilder object
                graphBuilder = new FilterGraph() as IFilterGraph2;

                // Get a ICaptureGraphBuilder2 to help build the graph
                captureGraphBuilder = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;

                // Link the CaptureGraphBuilder to the filter graph
                int hr = captureGraphBuilder.SetFiltergraph(graphBuilder);
                DsError.ThrowExceptionForHR(hr);

                // Get the ogama screen capture device and add it to the filter graph
                IBaseFilter screenCaptureFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.VideoInputDevice,
                    "OgamaScreenCapture Filter");
                hr = graphBuilder.AddFilter(screenCaptureFilter,
                    "Ogama Screen Capture Filter");
                DsError.ThrowExceptionForHR(hr);

                // Query the interface for the screen capture Filter
                Ogama.Modules.Recording.Presenter.DSScreenCapture.IOgamaScreenCapture
                    ogamaFilter = screenCaptureFilter
                    as Ogama.Modules.Recording.Presenter.DSScreenCapture.IOgamaScreenCapture;


                hr = ogamaFilter.set_Monitor(rtaSettings.MonitorIndex);
                DsError.ThrowExceptionForHR(hr);

                hr = ogamaFilter.set_Framerate(rtaSettings.Framerate);
                DsError.ThrowExceptionForHR(hr);

                IBaseFilter smartTeeFilter = new SmartTee() as IBaseFilter;
                hr = graphBuilder.AddFilter(smartTeeFilter, "Smart Tee");
                DsError.ThrowExceptionForHR(hr);

                // Get the video compressor and add it to the filter graph
                // Create the filter for the selected video compressor
                IBaseFilter videoCompressorFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.VideoCompressorCategory,
                    rtaSettings.VideoCompressorName);

                hr = graphBuilder.AddFilter(videoCompressorFilter, "Video Compressor");
                DsError.ThrowExceptionForHR(hr);


                // Add the Audio input device to the graph
                IBaseFilter AudioDeviceFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.AudioInputDevice,
                    rtaSettings.AudioInputDeviceName);

                if (AudioDeviceFilter != null)
                {
                    hr = graphBuilder.AddFilter(AudioDeviceFilter, "Audio Source");
                    DsError.ThrowExceptionForHR(hr);
                }
                // Get the audio compressor and add it to the filter graph
                // Create the filter for the selected audio compressor
                IBaseFilter AudioCompressorFilter = DirectShowUtils.CreateFilter(
                  FilterCategory.AudioCompressorCategory,
                  rtaSettings.AudioCompressorName);
                if (AudioCompressorFilter != null)
                {
                    hr = graphBuilder.AddFilter(AudioCompressorFilter, "Audio Compressor");
                    DsError.ThrowExceptionForHR(hr);
                }

                // Render the file writer portion of graph (mux -> file)
                hr = captureGraphBuilder.SetOutputFileName(
                  MediaSubType.Avi,
                  rtaSettings.TempFilename,
                  out muxFilter,
                  out fileWriterFilter);
                if (hr != 0)
                {
                    return false;
                }
                //DsError.ThrowExceptionForHR(hr);

                hr = captureGraphBuilder.AllocCapFile(rtaSettings.TempFilename,
                    10000000);
                DsError.ThrowExceptionForHR(hr);


                if (useMouseFilter)
                {
                    this.setupDmoFilter(graphBuilder);

                    hr = captureGraphBuilder.RenderStream(
                         null,
                         null,
                         screenCaptureFilter,
                         videoCompressorFilter,
                         muxFilter);
                    DsError.ThrowExceptionForHR(hr);

                    /*
                    hr = this.captureGraphBuilder.RenderStream(
                        null,
                        null,
                        screenCaptureFilter,
                        null,
                        smartTeeFilter);
                        DsError.ThrowExceptionForHR(hr);

                    hr = this.captureGraphBuilder.RenderStream(
                      null,
                      null,
                      smartTeeFilter,
                      videoCompressorFilter,
                      this.muxFilter);
                    DsError.ThrowExceptionForHR(hr);

                    hr = this.captureGraphBuilder.RenderStream(
                      null,
                      null,
                      smartTeeFilter,
                      null,
                      this.dmoFilter);
                    DsError.ThrowExceptionForHR(hr);

                    hr = this.captureGraphBuilder.RenderStream(
                      null,
                      null,
                      this.dmoFilter,
                      null,
                      null);
                    DsError.ThrowExceptionForHR(hr);
                     * */
                }
                else
                {
                    hr = captureGraphBuilder.RenderStream(
                     null,
                     null,
                     screenCaptureFilter,
                     videoCompressorFilter,
                     muxFilter);

                    //DsError.ThrowExceptionForHR(hr);
                    if (hr != 0)
                    {
                        return false;
                    }
                }





                // Render audio (audio -> mux)
                if (AudioDeviceFilter != null)
                {

                    hr = captureGraphBuilder.RenderStream(
                      PinCategory.Capture,
                      MediaType.Audio,
                      AudioDeviceFilter,
                      AudioCompressorFilter,
                      muxFilter);

                    DsError.ThrowExceptionForHR(hr);
                }


                hr = graphBuilder.SetDefaultSyncSource();
                DsError.ThrowExceptionForHR(hr);

                mediaControl = (IMediaControl)graphBuilder;

                return true;
            }
            catch (Exception e)
            {
                ExceptionMethods.HandleException(e);
                return false;
            }

        }



        private void setupDmoFilter(IGraphBuilder graphBuilder)
        {
            // Add a DMO Wrapper Filter
            this.dmoFilter = (IBaseFilter)new DMOWrapperFilter();
            this.dmoWrapperFilter = (IDMOWrapperFilter)this.dmoFilter;

            // But it is more useful to show how to scan for the DMO
            Guid g = this.dmoHelper.FindGuid("DmoOverlay", DMOCategory.VideoEffect);

            int hr = this.dmoWrapperFilter.Init(g, DMOCategory.VideoEffect);
            DMOError.ThrowExceptionForHR(hr);

            this.SetDMOParams(this.dmoFilter);

            // Add it to the Graph
            hr = this.graphBuilder.AddFilter(this.dmoFilter, "DMO Filter");
            DsError.ThrowExceptionForHR(hr);
        }



        private void SetDMOParams(IBaseFilter dmoWrapperFilter)
        {
            if (dmoWrapperFilter == null)
            {
                return;
            }

            int hr;
            this.dmoParams = dmoWrapperFilter as IMediaParams;

            /*MPData gazeX = new MPData();
            gazeX.vInt = 0;
            hr = dmoParams.SetParam(0, gazeX);
            DMOError.ThrowExceptionForHR(hr);

            MPData gazeY = new MPData();
            gazeY.vInt = 0;
            hr = dmoParams.SetParam(1, gazeY);
            DMOError.ThrowExceptionForHR(hr);
            */

            this.mouseX = new MPData();
            this.mouseX.vInt = 0;
            hr = this.dmoParams.SetParam(0, mouseX);
            DMOError.ThrowExceptionForHR(hr);

            this.mouseY = new MPData();
            this.mouseY.vInt = 0;
            hr = this.dmoParams.SetParam(1, mouseY);
            DMOError.ThrowExceptionForHR(hr);
        }


        public System.Windows.Forms.MouseEventHandler GetMouseListener()
        {
            System.Windows.Forms.MouseEventHandler MouseListener =
            new System.Windows.Forms.MouseEventHandler(this.onMouseEvent);

            return MouseListener;
        }

        private void onMouseEvent(object sender, MouseEventArgs e)
        {
            Point point = new Point();
            point.X = e.X;
            point.Y = e.Y;
            this.UpdateDMOParams(point);
        }

        /// <summary>
        /// Updates the GazeOverlay DMO filter parameters with the new positions
        /// of the gaze and mouse locations.
        /// </summary>
        /// <param name="gazeLocation">A <see cref="Point"/> with the new gaze location.</param>
        /// <param name="mouseLocation">A <see cref="Point"/> with the new mouse location.</param>
        public void UpdateDMOParams(Point mouseLocation)
        {
            if (this.dmoParams == null || this.mediaControl == null)
            {
                return;
            }

            int hr;

            /*MPData gazeX.vInt = gazeLocation.X;
            hr = this.dmoParams.SetParam(0, this.gazeX);
            DMOError.ThrowExceptionForHR(hr);

            this.gazeY.vInt = gazeLocation.Y;
            hr = this.dmoParams.SetParam(1, this.gazeY);
            DMOError.ThrowExceptionForHR(hr);
            */

            this.mouseX.vInt = mouseLocation.X;
            hr = this.dmoParams.SetParam(0, this.mouseX);
            DMOError.ThrowExceptionForHR(hr);

            this.mouseY.vInt = mouseLocation.Y;
            hr = this.dmoParams.SetParam(1, this.mouseY);
            DMOError.ThrowExceptionForHR(hr);
        }



        /// <summary>
        /// start recording process
        /// </summary>
        public void start()
        {
            if (this.mediaControl == null)
            {
                return;
            }
            if (this.running)
            {
                throw new System.InvalidOperationException("The process is alrady running!");
            }
            int hr = this.mediaControl.Run();
            DsError.ThrowExceptionForHR(hr);
            this.running = true;
        }

        /// <summary>
        /// stop recording process
        /// </summary>
        public void stop()
        {
            if (!this.running)
            {
                return;
            }
            if (this.mediaControl == null)
            {
                return;
            }
            int hr = this.mediaControl.Stop();

            hr = this.captureGraphBuilder.CopyCaptureFile(rtaSettings.TempFilename,
                rtaSettings.Filename, false, null);

            DsError.ThrowExceptionForHR(hr);

            this.running = false;

        }


        public void handleRtaSettingsForm()
        {

        }
        public string createRtaFilename(string trialName)
        {
            string filename = "";

            return filename;
        }
        /*
        protected bool init2()
        {
          
            try
            {
                GC.Collect();
                // Get the graphbuilder object
                IGraphBuilder graphBuilder = new FilterGraph() as IFilterGraph2;

                // Get a ICaptureGraphBuilder2 to help build the graph
                captureGraphBuilder = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;

                // Link the CaptureGraphBuilder to the filter graph
                int hr = captureGraphBuilder.SetFiltergraph(graphBuilder);
                DsError.ThrowExceptionForHR(hr);

                // Get the ogama screen capture device and add it to the filter graph
                IBaseFilter screenCaptureFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.VideoInputDevice,
                    "OgamaScreenCapture Filter");
                 hr = graphBuilder.AddFilter(screenCaptureFilter, 
                     "Ogama Screen Capture Filter");
                DsError.ThrowExceptionForHR(hr);

                // Query the interface for the screen capture Filter
                Ogama.Modules.Recording.Presenter.DSScreenCapture.IOgamaScreenCapture 
                    ogamaFilter = screenCaptureFilter 
                    as Ogama.Modules.Recording.Presenter.DSScreenCapture.IOgamaScreenCapture;


                hr = ogamaFilter.set_Monitor(monitorIndex);
                DsError.ThrowExceptionForHR(hr);

                hr = ogamaFilter.set_Framerate(frameRate);
                DsError.ThrowExceptionForHR(hr);

                IBaseFilter smartTeeFilter = new SmartTee() as IBaseFilter;
                hr = graphBuilder.AddFilter(smartTeeFilter, "Smart Tee");
                DsError.ThrowExceptionForHR(hr);

                // Get the video compressor and add it to the filter graph
                // Create the filter for the selected video compressor
                IBaseFilter videoCompressorFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.VideoCompressorCategory,
                    this.videoCompressorName);
                hr = graphBuilder.AddFilter(videoCompressorFilter, "Video Compressor");
                DsError.ThrowExceptionForHR(hr);


                // Add the Audio input device to the graph
                IBaseFilter AudioDeviceFilter = DirectShowUtils.CreateFilter(
                    FilterCategory.AudioInputDevice,
                    AudioInputDeviceName);
                if (AudioDeviceFilter != null)
                {
                    hr = graphBuilder.AddFilter(AudioDeviceFilter, "Audio Source");
                    DsError.ThrowExceptionForHR(hr);
                }
                // Get the audio compressor and add it to the filter graph
                // Create the filter for the selected audio compressor
                IBaseFilter AudioCompressorFilter = DirectShowUtils.CreateFilter(
                  FilterCategory.AudioCompressorCategory,
                  this.AudioCompressorName);
                if (AudioCompressorFilter != null)
                {
                    hr = graphBuilder.AddFilter(AudioCompressorFilter, "Audio Compressor");
                    DsError.ThrowExceptionForHR(hr);
                }

                // Render the file writer portion of graph (mux -> file)
                hr = captureGraphBuilder.SetOutputFileName(
                  MediaSubType.Avi,
                  tempFilename,
                  out muxFilter,
                  out fileWriterFilter);
                DsError.ThrowExceptionForHR(hr);

                hr = captureGraphBuilder.AllocCapFile(tempFilename, 10000000);
                DsError.ThrowExceptionForHR(hr);


                //TEST
                // Try interleaved first, because if the device supports it,
                // it's the only way to get audio as well as video
                
                hr = captureGraphBuilder.RenderStream(
                  PinCategory.Capture,
                  MediaType.Interleaved,
                  screenCaptureFilter,
                  videoCompressorFilter,
                  muxFilter);

                // If interleaved fails try video
                if (hr < 0)
                {
                   
                    hr = captureGraphBuilder.RenderStream(
                      PinCategory.Capture,
                      MediaType.Video,
                      screenCaptureFilter,
                      videoCompressorFilter,
                      muxFilter);

                    if (hr == -2147220969)
                    {
                        throw new ArgumentException("Video device is already in use");
                    }

                    //DsError.ThrowExceptionForHR(hr);
                }

                //the old way
               hr = captureGraphBuilder.RenderStream(
                    null,
                    null,
                    screenCaptureFilter,
                    videoCompressorFilter,
                    muxFilter);
                DsError.ThrowExceptionForHR(hr);
               
                // Render audio (audio -> mux)
                if (AudioDeviceFilter != null)
                {
                    
                    hr = captureGraphBuilder.RenderStream(
                      PinCategory.Capture,
                      MediaType.Audio,
                      AudioDeviceFilter,
                      AudioCompressorFilter,
                      muxFilter);

                    DsError.ThrowExceptionForHR(hr);
                }


                hr = graphBuilder.SetDefaultSyncSource();
                DsError.ThrowExceptionForHR(hr);

                mediaControl = (IMediaControl)graphBuilder;
            }
            catch (Exception e)
            {
                ExceptionMethods.HandleException(e);
                return false;
            }
            return true;
        }

        */











        /*
        protected void init()
        {
            dsScreenCapture = new DSScreenCapture("ffdshow video encoder", 10, 0);
            //@see DXCapture.cs

            

           // addAudioSource();


        }
        */
        /* private void addAudioSource()
         {
             DirectShowLib.IGraphBuilder graphBuilder = dsScreenCapture.getGraphBuilder();
             string AudioInputDevice = "Creative Sound Blaster-PCI";

             int hr = 0;

             DirectShowLib.IBaseFilter AudioDeviceFilter = GTHardware.Cameras.DirectShow.DirectShowUtils.CreateFilter(
                 DirectShowLib.FilterCategory.AudioInputDevice,
                 AudioInputDevice);

             if (AudioDeviceFilter != null)
             {

                 hr = graphBuilder.AddFilter(AudioDeviceFilter, "Audio Source");
                 DirectShowLib.DsError.ThrowExceptionForHR(hr);
             }
         }*/
    }
}
