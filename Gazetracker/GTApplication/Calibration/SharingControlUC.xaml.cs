using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GTLibrary;
using GTSettings;
using GTCommons;

namespace GTApplication.CalibrationUI
{
    public partial class SharingControlUC : UserControl
    {
        #region Variables

        private BitmapSource bmp;
        private string calibrationData;
        private const string ftpAddress = "ftp://ftp.xtreemhost.com";
        private const string ftpFolder = "/htdocs/";
        private const string ftpP = "gazeers";
        private const string ftpU = "xth_6272206";
        private int id;
        //private bool isSendSuccess = true;
        private const int timeoutMs = 10000;
        private BackgroundWorker worker_data;

        #endregion

        #region Events

        public static readonly RoutedEvent DataSentEvent = EventManager.RegisterRoutedEvent("DataSentEvent",
                                                                                            RoutingStrategy.Bubble,
                                                                                            typeof(RoutedEventHandler),
                                                                                            typeof(SharingControlUC));

        public static readonly RoutedEvent CancelEvent = EventManager.RegisterRoutedEvent("CancelEvent",
                                                                                          RoutingStrategy.Bubble,
                                                                                          typeof(RoutedEventHandler),
                                                                                          typeof(SharingControlUC));

        #endregion

        #region Constructor

        public SharingControlUC()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventhandlers

        public event RoutedEventHandler OnDataSent
        {
            add { base.AddHandler(DataSentEvent, value); }
            remove { base.RemoveHandler(DataSentEvent, value); }
        }

        public event RoutedEventHandler OnCancel
        {
            add { base.AddHandler(CancelEvent, value); }
            remove { base.RemoveHandler(CancelEvent, value); }
        }

        #endregion

        #region Public methods

        public void SendData(Tracker tracker, BitmapSource bitmapImage)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GTHardware.Camera.Instance.Device.Name);
            sb.AppendLine(GTHardware.Camera.Instance.Device.Width + "\t" + GTHardware.Camera.Instance.Device.Height + "\t" +
                          GTHardware.Camera.Instance.Device.FPS + "\t");
            sb.AppendLine(Settings.Instance.Processing.TrackingMethod + "\t" + tracker.FPSTracking);
            //sb.AppendLine(tracker.CalibrationDataAsString());

            SendDataGazeGroup(sb.ToString(), bitmapImage);
        }

        #endregion

        #region Private methods

        private void SendDataGazeGroup(string calibrationData, BitmapSource bitmapImage)
        {
            bmp = bitmapImage;

            // Generate ID
            var rand = new Random(DateTime.Now.Millisecond);
            id = rand.Next(9999999);
            this.calibrationData = id + "\n" + calibrationData;

            // Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string directory = GTPath.GetLocalApplicationDataPath();
            directory += Path.DirectorySeparatorChar + "Community";
            var dirUri = new Uri(directory);

            // Check dir
            if (!Directory.Exists(dirUri.LocalPath))
            {
                Directory.CreateDirectory(dirUri.LocalPath);
            }

            // Save to files
            var fileCalibrationData = new FileInfo(dirUri.LocalPath + Path.DirectorySeparatorChar + id + ".txt");
            SaveCalibrationData(fileCalibrationData, calibrationData);

            var fileImage = new FileInfo(dirUri.LocalPath + Path.DirectorySeparatorChar + id + ".jpg");
            SaveImage(fileImage, bitmapImage);

            // Send data
            worker_data = new BackgroundWorker();
            worker_data.DoWork += worker_data_DoWork;
            worker_data.RunWorkerCompleted += worker_data_Completed;
            worker_data.RunWorkerAsync(id.ToString());
        }

        private static void SaveCalibrationData(FileInfo file, string dataStr)
        {
            try
            {
                var sw = new StreamWriter(file.FullName);
                sw.Write(dataStr);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private static void SaveImage(FileInfo file, BitmapSource bmpScr)
        {
            if (file == null) return;

            try
            {
                var fileStream = new FileStream(file.FullName, FileMode.Create);

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmpScr));
                encoder.QualityLevel = 100;
                encoder.Save(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void ShareDataCancel(object sender, RoutedEventArgs e)
        {
            if (worker_data != null)
                worker_data.CancelAsync();

            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CancelEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        #region DoWork - Send data

        //private void worker_data_DoWork(object sender, DoWorkEventArgs e) 
        //{
        // used for the gazegroup php upload script
        //string id = e.Argument as string;

        //string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        //directory += "\\Community";

        //Uri dirUri = new Uri(directory);

        //FileInfo fileNfoImage = new FileInfo(dirUri.LocalPath + "\\" + id + ".jpg");
        //FileInfo fileNfoData = new FileInfo(dirUri.LocalPath + "\\" + id + ".txt");

        //UploadFile[] files = new UploadFile[] 
        //{ 
        //    new UploadFile(fileNfoData.FullName, "data", "text/plain"), 
        //    new UploadFile(fileNfoImage.FullName, "image", "image/jpeg"),  
        //}; 

        //HttpWebRequest req = HttpWebRequest.Create(new Uri("http://www.gazegroup.org/community/calibrationupload.php"));

        //NameValueCollection form = new NameValueCollection(); 
        //form["name1"] = "value1"; 

        //    try
        //    {
        //        string response = HttpUploadHelper.Upload("http://www.gazegroup.org/community/calibrationupload.php", files, form);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.Out.WriteLine("CalibrationSharing: " + ex.Message);
        //    }
        //}


        private void worker_data_DoWork(object sender, DoWorkEventArgs e)
        {
            var id = e.Argument as string;

            // string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var directory = GTPath.GetLocalApplicationDataPath();
            directory += Path.DirectorySeparatorChar + "Community";

            var dirUri = new Uri(directory);

            var fileNfoImage = new FileInfo(dirUri.LocalPath + Path.DirectorySeparatorChar + id + ".jpg");
            SendFile(fileNfoImage, true);

            var fileNfoData = new FileInfo(dirUri.LocalPath + Path.DirectorySeparatorChar + id + ".txt");
            SendFile(fileNfoData, false);
        }


        private void SendFile(FileInfo fileNfo, bool isBinary)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpAddress + ftpFolder + fileNfo.Name);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpU, ftpP);
            request.UsePassive = true;
            request.UseBinary = isBinary;
            request.KeepAlive = false;
            request.Timeout = timeoutMs;

            var buffer = new byte[1];

            //Load the file
            try
            {
                FileStream stream = File.Open(fileNfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            //Upload file
            try
            {
                if (buffer.Length > 2)
                {
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                //isSendSuccess = false;
            }
        }

        #endregion

        #region DoWork - Completed

        private void worker_data_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(DataSentEvent, this);
            RaiseEvent(args1);
        }

        #endregion

        #endregion
    }
}