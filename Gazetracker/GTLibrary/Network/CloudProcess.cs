using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using GTSettings;

namespace GTLibrary.Network
{
    public class CloudProcess
    {
        #region Variables

        private static CloudProcess instance;
        //private Process cloudProc;

        #endregion

        #region Constructor

        private CloudProcess()
        {
            Settings.Instance.CloudSettings.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CloudSettings_PropertyChanged);
        }

        #endregion

        #region Get/Set

        public static CloudProcess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CloudProcess();
                }

                return instance;
            }
        }

        public bool IsCloudProcessRunning
        {
            get
            {
                bool isRunning = false;
                Process[] processlist = Process.GetProcesses();

                foreach (Process proc in processlist)
                {
                    if (proc.ProcessName == "GTCloud")
                        isRunning = true;
                }

                return isRunning;
            }
        }

        #endregion

        #region Public methods

        public void Start()
        {
            if (Settings.Instance.CloudSettings.IsSharingCalibrationData && IsCloudProcessRunning == false)
            {
                try
                {
                    FileInfo gtCloudExe = new FileInfo(Directory.GetCurrentDirectory() + "\\GTCloud.exe");

                    if (gtCloudExe.Exists == false)
                    {
                        var ofd = new OpenFileDialog();
                        ofd.Title = "Please locate the GTCloud.exe file";
                        ofd.Multiselect = false;
                        ofd.Filter = "Executables (*.exe)|*.exe*;|All Files|*.*";

                        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string[] filePath = ofd.FileNames;
                            gtCloudExe = new FileInfo(filePath[0]);
                        }
                    }

                    if (gtCloudExe.Exists)
                    {
                        Process gtCloudProc = new Process();

                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = gtCloudExe.FullName;

                        //if (Settings.Instance.CloudSettings.IsSharingUIVisible == false)
                        //{
                        //    psi.WindowStyle = ProcessWindowStyle.Minimized;
                        //    psi.CreateNoWindow = true;
                        //    psi.Arguments = "Hidden";
                        //}

                        //psi.WindowStyle = ProcessWindowStyle.Minimized;
                        //psi.CreateNoWindow = true;
                        //psi.Arguments = "Hidden";


                        gtCloudProc.StartInfo = psi;
                        gtCloudProc.Start();

                        // after launched, lower priority 
                        gtCloudProc.PriorityClass = ProcessPriorityClass.BelowNormal;

                    }
                    else
                    {
                        Console.Out.WriteLine("Could not find GTCloud.exe");
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Unable to start CloudService. Message:" + ex.Message);
                }

            }
        }

        internal void Stop()
        {
            //cloudProc.Kill();
        }

        #endregion

        #region Private methods

        private void CloudSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsSharingUIVisible":
                    if (Settings.Instance.CloudSettings.IsSharingUIVisible)
                        CloudClient.Instance.ShowSharingUI();
                    else
                        CloudClient.Instance.HideSharingUI();
                    break;
            }
        }

        #endregion
    }
}
