using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GazeTrackerUI;
using System.Collections;
using System.Globalization;
using System.Net;
using GazeTrackingLibrary;
using GazeTrackingLibrary.Settings;
using GazeTrackingLibrary.Utils;
using GazeTracker.Tools;
using GTCommons;
//using GazeTrackingLibrary.Illumination;

namespace GazeTrackerUI.Settings
{

    public partial class IlluminationSettingsWindow : Window
    {

        #region Varibles

        private static IlluminationSettingsWindow instance;
        //private LEDGroup selectedGroup; 
        private bool hasBeenMoved = false;

        #endregion


        #region Constructor

        private IlluminationSettingsWindow()
        {
            InitializeComponent();
            SetValues();
        }

        #endregion


        #region Initialize

        public void SetValues()
        {
            //comboBoxGlobalAmp.ItemsSource = Enum.GetValues(typeof(GazeTrackingLibrary.Illumination.AmpEnum));
            //comboBoxGlobalVolt.ItemsSource = Enum.GetValues(typeof(GazeTrackingLibrary.Illumination.VoltEnum));
            //comboBoxFunction.ItemsSource = Enum.GetValues(typeof(GazeTrackingLibrary.Illumination.IlluminatorFunction));

            comboBoxTurnAll.Items.Add("");
            comboBoxTurnAll.Items.Add("On");
            comboBoxTurnAll.Items.Add("Off");
            comboBoxTurnAll.SelectedIndex = 0;
        }


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //Illuminator.Instance.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Illuminator_PropertyChanged);
            //Illuminator.Instance.LEDManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(LEDManager_PropertyChanged);

            comboBoxGroups.SelectionChanged += new SelectionChangedEventHandler(AssignLEDsToGroup);
            comboBoxTurnAll.SelectionChanged += new SelectionChangedEventHandler(comboBoxTurnAll_SelectionChanged);
            comboBoxGlobalAmp.SelectionChanged += new SelectionChangedEventHandler(comboBoxGlobalAmp_SelectionChanged);
            comboBoxGlobalVolt.SelectionChanged += new SelectionChangedEventHandler(comboBoxGlobalVolt_SelectionChanged);
            SliderBrightness.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderBrightness_ValueChanged);

            RectangleGroups.MouseDown += new MouseButtonEventHandler(RectangleGroups_MouseDown);

            //if (Illuminator.Instance.IsConnected == false)
            //    TabItemPhidgetSetup.IsSelected = true;
        }

        void RectangleGroups_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clicked in the emoty area, deselect group-buttons
            foreach (GazeGUIVelocity.Button btn in WrapPanelGroups.Children)
                btn.SelectedGlow = false;

            //selectedGroup = null;

            textBoxGroupID.Text = "";
            comboBoxFunction.SelectedValue = null;
        }


        void SliderBrightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (selectedGroup != null)
            //    Illuminator.Instance.LEDManager.SetBrightness(selectedGroup, Convert.ToInt32(SliderBrightness.Value));

            foreach (GazeGUIVelocity.Button btn in WrapPanelGroups.Children)
            {
                //if (btn.Label == selectedGroup.ID)
                //    btn.Opacity = 0.5 + ((SliderBrightness.Value / 2) / 100);
            }
        }



        #region Global settings (amp, volt, all on/off)

        private void comboBoxGlobalVolt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(comboBoxGlobalVolt.SelectedItem != null)
            //   Illuminator.Instance.LEDManager.SetVolt((VoltEnum)comboBoxGlobalVolt.SelectedItem);
        }

        private void comboBoxGlobalAmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(comboBoxGlobalAmp.SelectedItem != null)
            //   Illuminator.Instance.LEDManager.SetAmp((AmpEnum)comboBoxGlobalAmp.SelectedItem);
        }

        private void comboBoxTurnAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)comboBoxTurnAll.SelectedItem;

            //if (value == "On")
            //    Illuminator.Instance.LEDManager.SetBrightnessAll(100);
            //else if (value == "Off")
            //    Illuminator.Instance.LEDManager.SetBrightnessAll(0);
        }

        #endregion


        private void Illuminator_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "IsConnected")
            //{
            //    //if (Illuminator.Instance.IsConnected)
            //    {
            //        this.Dispatcher.Invoke(
            //            System.Windows.Threading.DispatcherPriority.Normal,
            //            new Action(
            //            delegate()
            //            {
            //                UpdatePhidgetInfo();
            //                UpdateConnectionStatus();
            //                UpdateLEDChannels();
            //                BtnConnect.Visibility = Visibility.Collapsed;
            //            }
            //            ));
            //    }
            //    else
            //    {
            //        this.Dispatcher.Invoke(
            //            System.Windows.Threading.DispatcherPriority.Normal,
            //            new Action(
            //            delegate()
            //            {
            //                BtnConnect.Visibility = Visibility.Visible;
            //            }
            //            ));
            //    }
            //}
        }

        private void LEDManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (Illuminator.Instance.IsConnected == false)
            //    return;

            this.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
              delegate()
              {

                //datagridLEDs.ItemsSource = Illuminator.Instance.LEDManager.Channels;

              }
              ));
        }

        private void btnGroup_Action(object sender, RoutedEventArgs e)
        {
            foreach (GazeGUIVelocity.Button b in WrapPanelGroups.Children)
            {
                b.SelectedGlow = false;
            }

            GazeGUIVelocity.Button selectedBtn = (GazeGUIVelocity.Button) sender;
            selectedBtn.SelectedGlow = true;
            //selectedGroup = Illuminator.Instance.LEDManager.GetLEDGroup(selectedBtn.Label);

            //if (selectedGroup.Channels.Count == 0)
            //{
            //    MessageBox.Show("The group has no LED channels assigned to it, click tab \"Assign LEDs\" to add assign channels");
            //    return;
            //}

            //Illuminator.Instance.LEDManager.SetBrightness(selectedGroup, Convert.ToInt32(SliderBrightness.Value));

            e.Handled = true;
        }


        #endregion


        #region Private U.I methods

        private void ConnectPhidget(object sender, RoutedEventArgs e)
        {
            //Illuminator.Instance.ConnectPhidget();
            // Will trigger event if successful..
        }


        private void CreateGroup(object sender, RoutedEventArgs e)
        {
            if (textBoxGroupID.Text.Length < 1)
            {
                MessageBox.Show("Please enter a unique group id");
                textBoxGroupID.Focus();
                return;
            }

            if (comboBoxFunction.SelectedItem == null)
            {
                MessageBox.Show("Please select a function for the group.");
                comboBoxFunction.Focus();
                return;
            }

            // Create group
            //LEDGroup g = new LEDGroup();
            //g.ID = textBoxGroupID.Text;
            //g.Function = (IlluminatorFunction)comboBoxFunction.SelectedValue;

            //// Add group to manager
            //Illuminator.Instance.LEDManager.LEDGroups.Add(g);
            
            //// Set combobox groups on assign tab
            //comboBoxGroups.ItemsSource = Illuminator.Instance.LEDManager.LEDGroups;
            //comboBoxGroups.DataContext = Illuminator.Instance.LEDManager.LEDGroups;
            //comboBoxGroups.DisplayMemberPath = "ID";
            //comboBoxGroups.SelectedItem = 0;

            // Add group button to wrap panel
            GazeGUIVelocity.Button btn = new GazeGUIVelocity.Button();
            //btn.Label = g.ID;
            try
            {
                btn.Icon = new BitmapImage(GazeTrackerUI.TrackerViewer.GPU.Global.MakePackUri("Graphics/lightbulbWhite.png"));
            }
            catch (Exception ex)
            { }
     
            btn.ActivationMethod = "Mouse";
            btn.Margin = new Thickness(15, 0, 0, 0);
            btn.Action += new RoutedEventHandler(btnGroup_Action);
            WrapPanelGroups.Children.Add(btn);
        }


        private void AssignLEDsToGroup(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxGroups.SelectedItem == null)
            {
                return;
            }

            //if (datagridLEDs.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("No channels are selected for assignment to the group");
            //    return;
            //}

            //LEDGroup g = (LEDGroup) comboBoxGroups.SelectedItem;

            //List<LEDChannel> selectedChannels = new List<LEDChannel>();

            //foreach (object o in datagridLEDs.SelectedItems)
            //{
            //    LEDChannel led = (LEDChannel) o;
            //    led.Group = g;
            //    led.Function = g.Function;
            //    led.Brightness = Convert.ToInt32(SliderBrightness.Value);
            //    g.Channels.Add(led);
            //}

            UpdateLEDChannels();
            comboBoxGroups.SelectedItem = null;
        }


        #region Update values

        private void UpdatePhidgetInfo()
        {
            //LabelServerID.Content = Illuminator.Instance.PhidgetServerID;
            //LabelHost.Content = Illuminator.Instance.PhidgetAddress;
            //LabelPort.Content = Illuminator.Instance.PhidgetPort;
            //LabelSN.Content = Illuminator.Instance.PhidgetSerialNumber;
            //LabelName.Content = Illuminator.Instance.PhidgetName;
            //LabelID.Content = Illuminator.Instance.PhidgetID;
            //LabelType.Content = Illuminator.Instance.PhidgetType;
            //LabelVersion.Content = Illuminator.Instance.PhidgetVersion;
        }

        private void UpdateConnectionStatus()
        {
            //if (Illuminator.Instance.IsConnected)
            //{
            //    labelConnectionStatus.Content = "Connected";
            //    labelConnectionStatus.Foreground = new SolidColorBrush(Colors.Green);
            //}
            //else
            //{
                labelConnectionStatus.Content = "Not connected";
                labelConnectionStatus.Foreground = new SolidColorBrush(Colors.DarkRed);
            //}
        }

        private void UpdateLEDChannels()
        {
            //datagridLEDs.ItemsSource = Illuminator.Instance.LEDManager.Channels;
        }

        #endregion


        #endregion


        #region Get/Set

        public static IlluminationSettingsWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IlluminationSettingsWindow();
                }

                return instance;
            }
        }

        #endregion


        #region Misc UI stuff

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void DragWindow(object sender, MouseButtonEventArgs args)
        {
            try
            {
                hasBeenMoved = true;
                DragMove();
            }
            catch (Exception)
            {   }
        }

        public bool HasBeenMoved
        {
            get { return hasBeenMoved; }
        }

        #endregion

    }
}
