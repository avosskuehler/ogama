using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GTSettings;
using Size = System.Drawing.Size;

namespace GazeTrackerUI.SettingsUI
{
    // currently not used, but contains UI for loading up classification defs. (haar cascades) for eye roi detection

    public partial class EyetrackerSettingsWindow : Window
    {
        private static EyetrackerSettingsWindow instance;
        private bool hasBeenMoved;

        private EyetrackerSettingsWindow()
        {
            InitializeComponent();
            LayoutRoot.DataContext = GTSettings.Settings.Instance.Eyetracker;
        }

        #region private methods

        private int Min
        {
            // Exposes one side of the size rectangle for slider binding..
            get { return GTSettings.Current.Eyetracker.SizeMin.Width; }
            set { GTSettings.Current.Eyetracker.SizeMin = new Size(value, value); }
        }

        private int Max
        {
            // Exposes one side of the size rectangle for slider binding..
            get { return GTSettings.Current.Eyetracker.SizeMax.Width; }
            set { GTSettings.Current.Eyetracker.SizeMax = new Size(value, value); }
        }

        private void LoadClassifier(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Select the Haar Cascade Classifier file";
            ofd.Multiselect = false;
            ofd.Filter = "XML files (*.xml)|*.xml*;*.xml|All Files|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filePath = ofd.FileNames;
                GTSettings.Current.Eyetracker.LoadHaarCascade(filePath[0]);
            }
        }

        #endregion

        #region Get/Set 

        public static EyetrackerSettingsWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EyetrackerSettingsWindow();
                }

                return instance;
            }
        }

        #endregion

        #region Misc UI stuff

        public bool HasBeenMoved
        {
            get { return hasBeenMoved; }
        }

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            GTSettings.Current.WriteConfigFile();
        }

        private void DragWindow(object sender, MouseButtonEventArgs args)
        {
            hasBeenMoved = true;
            DragMove();
        }

        #endregion
    }
}