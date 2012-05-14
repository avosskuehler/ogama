using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GTCommons;

namespace GTApplication
{
    /// <summary>
    /// Interaction logic for MenubarIconsUserControl.xaml
    /// </summary>
    public partial class MenubarIconsUserControl : UserControl
    {
        public MenubarIconsUserControl()
        {
            InitializeComponent();
            IsAutoTuneVisible = false;
            IsTrackStatsVisibile = false;
            //IsIlluminationVisibile = false;
        }

        #region Private methods (on icon click)

        private void Settings(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.Settings.Settings();
        }

        private void Autotune(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.Autotune.Autotune();
        }

        private void CameraSettings(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.Settings.CameraSettings();
        }

        private void DetachVideo(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.TrackerViewer.VideoDetach();
        }

        private void NetworkClient(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.NetworkClient();
        }

        private void TrackStats(object sender, MouseButtonEventArgs e)
        {
            GTCommands.Instance.TrackQuality();
        }

        #endregion

        #region Get/Set Visibility

        public bool IsAutoTuneVisible
        {
            set {
                IconAutotune.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsSettingsVisible
        {
            set {
                IconSettings.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsDetachVideoVisible
        {
            set {
                IconDetachVideo.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsNetworkClientVisible
        {
            set {
                IconUDPClient.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsTrackStatsVisibile
        {
            set {
                IconTrackStats.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        //public bool IsIlluminationVisibile
        //{
        //    set {
        //        IconIllumination.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        //    }
        //}

        #endregion
    }
}