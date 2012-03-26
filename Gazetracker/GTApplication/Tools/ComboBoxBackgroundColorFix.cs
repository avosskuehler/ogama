using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GTApplication.Tools
{
    public static class ComboBoxBackgroundColorFix
    {
        private static bool _isInitialized;

        /// <summary>
        /// Fix for setting the dropdown background color on WPF ComboBoxes.
        /// Implementation source: http://www.codeproject.com/KB/cs/WPFComboBoxFix.aspx
        /// Initialize must be called before any Combo boxes are created
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized) return;
            // Registed the callback methods when the properties change on a ComboBox class
            Control.BackgroundProperty.OverrideMetadata(typeof (ComboBox),
                                                        new FrameworkPropertyMetadata(OnBackgroundChanged));
            Control.ForegroundProperty.OverrideMetadata(typeof (ComboBox),
                                                        new FrameworkPropertyMetadata(OnForegroundChanged));
            _isInitialized = true;
        }

        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Set the drop down background color to match the background
            SetDropDownBackground(d as ComboBox);
        }

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            /* Manually set the foreground (to overcome bug). Apparently the ComboBox does not listen 
      * when the Foreground DepencyProperty is changed and therefore does not 
      * update itself unless the value is changed through the Foreground .net property
      */
            if (d != null) ((ComboBox) d).Foreground = e.NewValue as Brush;
        }

        private static void SetDropDownBackground(ComboBox comboBox)
        {
            // The drop down control uses the WindowBrush to paint its background
            // By overriding that Brush (just for this control)

            if (comboBox.Resources.Contains(SystemColors.WindowBrushKey))
            {
                comboBox.Resources.Remove(SystemColors.WindowBrushKey);
            }
            comboBox.Resources.Add(SystemColors.WindowBrushKey, comboBox.Background);
        }
    }
}