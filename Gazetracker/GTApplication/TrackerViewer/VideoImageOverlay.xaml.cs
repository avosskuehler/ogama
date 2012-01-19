using System.Windows;
using System.Windows.Controls;
using GTCommons.Enum;
using GTSettings;

namespace GTApplication.TrackerViewer 
{
    public partial class VideoImageOverlay : Window 
    {

        #region Constructor

        public VideoImageOverlay() 
        {
            InitializeComponent();
            HookUpEvents();
            GridPerformanceCounters.Visibility = System.Windows.Visibility.Collapsed;
            GridVisualization.IsVisibleChanged += new DependencyPropertyChangedEventHandler(GridVisualization_IsVisibleChanged);
        }

        #endregion


        #region Private methods

        private void GridVisualization_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) 
        {
            switch (Settings.Instance.Visualization.VideoMode)
            {
                case VideoModeEnum.Normal:
                    CheckBoxRenderNormal.IsChecked = true;
                    break;
                case VideoModeEnum.Processed:
                    CheckBoxRenderProcessed.IsChecked = true;
                    break;
                case VideoModeEnum.RawNoTracking:
                    CheckBoxRenderRaw.IsChecked = true;
                    break;       
            }
        }


        private void HookUpEvents() 
        {
            CheckBoxRenderRaw.Checked += VideoModeChange;
            CheckBoxRenderNormal.Checked += VideoModeChange;
            CheckBoxRenderProcessed.Checked += VideoModeChange;
            CheckBoxRenderRaw.Unchecked += VideoModeChangeDummyUnchecked;
            CheckBoxRenderNormal.Unchecked += VideoModeChangeDummyUnchecked;
            CheckBoxRenderProcessed.Unchecked += VideoModeChangeDummyUnchecked;
        }


        private void VideoModeChange(object sender, RoutedEventArgs e)
        {
            CheckBox videoModeCb = sender as CheckBox;
           
            // Switch video mode

            if (videoModeCb != null)
                switch (videoModeCb.Name)
                {
                    case "CheckBoxRenderRaw":
                        Settings.Instance.Visualization.VideoMode = VideoModeEnum.RawNoTracking;
                        CheckBoxRenderNormal.IsChecked = false;
                        CheckBoxRenderProcessed.IsChecked = false;
                        break;
                    case "CheckBoxRenderNormal":
                        Settings.Instance.Visualization.VideoMode = VideoModeEnum.Normal;
                        CheckBoxRenderProcessed.IsChecked = false;
                        CheckBoxRenderRaw.IsChecked = false;
                        break;
                    case "CheckBoxRenderProcessed":
                        Settings.Instance.Visualization.VideoMode = VideoModeEnum.Processed;
                        CheckBoxRenderNormal.IsChecked = false;
                        CheckBoxRenderRaw.IsChecked = false;
                        break;
                }
        }

        private void VideoModeChangeDummyUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox videoModeCb = sender as CheckBox;

            // Clicking same button twice? Somethings got to be turned on!
            if(videoModeCb.Name == "CheckBoxRenderRaw" && Settings.Instance.Visualization.VideoMode == VideoModeEnum.RawNoTracking)
            {
                videoModeCb.IsChecked = true;
                return;
            }
            if(videoModeCb.Name == "CheckBoxRenderNormal" && Settings.Instance.Visualization.VideoMode == VideoModeEnum.Normal)
            {
                videoModeCb.IsChecked = true;
                return;
            }
            if(videoModeCb.Name == "CheckBoxRenderProcessed" && Settings.Instance.Visualization.VideoMode == VideoModeEnum.Processed)
            {
                videoModeCb.IsChecked = true;
                return;
            } 
        }

        #endregion

    }
}
