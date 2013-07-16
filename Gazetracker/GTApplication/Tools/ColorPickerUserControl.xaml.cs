using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GTApplication.Tools
{
    /// <summary>
    /// Interaction logic for ColorPickerUserControl.xaml
    /// </summary>
    public partial class ColorPickerUserControl : UserControl
    {
        #region Properties

        #region Delegates

        public delegate void ColorSelectedEventHandler(Color color);

        #endregion

        private byte[] _pixels;
        private Color selectedColor;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public event DragDeltaEventHandler Drag;

        private void OnDragDelta(DragDeltaEventArgs e)
        {
            DragDeltaEventHandler dragEventHandler = Drag;
            if (dragEventHandler != null) dragEventHandler(this, e);
        }

        public event ColorSelectedEventHandler OnColorSelected;

        #endregion

        private readonly TranslateTransform _markerTransform = new TranslateTransform();

        public ColorPickerUserControl()
        {
            DataContext = this;
            InitializeComponent();
            brightnessSlider.ValueChanged += BrightnessSliderValueChanged;
            colorMarker.RenderTransform = _markerTransform;
            colorMarker.RenderTransformOrigin = new Point(1, 1);
            borderColorChart.Cursor = Cursors.Cross;
            brightnessSlider.Value = 0.5;
        }

        private void moveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            OnDragDelta(e);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var cb = new CroppedBitmap((BitmapSource) (((Image) e.Source).Source),
                                           new Int32Rect((int) Mouse.GetPosition(e.Source as Image).X,
                                                         (int) Mouse.GetPosition(e.Source as Image).Y, 1, 1));
                _pixels = new byte[4];
                try
                {
                    cb.CopyPixels(_pixels, 4, 0);
                    SetColor(Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]));
                    UpdateMarkerPosition();

                    if (OnColorSelected != null)
                        OnColorSelected(SelectedColor);
                }
                catch
                {
                    // not logged
                }
                UpdateSlider();
            }
            catch (Exception)
            {
                // not logged
            }
        }

        //private void UpdateInstanceColor()
        //{
        //    //brightnessSlider.Value = 0.5; // default half-way (set on new color selected)
        //    SetColor(Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]));
        //}

        private void UpdateMarkerPosition()
        {
            _markerTransform.X = Mouse.GetPosition(borderColorChart).X - (borderColorChart.ActualWidth/2);
            _markerTransform.Y = Mouse.GetPosition(borderColorChart).Y - (borderColorChart.ActualHeight/2);
        }

        private void BrightnessSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_pixels == null)
            {
                _pixels = new byte[3];
                _pixels[2] = SelectedColor.R;
                _pixels[1] = SelectedColor.G;
                _pixels[0] = SelectedColor.B;
            }

            Color nc = Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]);
            var f = (float) e.NewValue;
            float r, g, b;
            const float a = 1;

            if (f >= 0.5f)
            {
                r = nc.ScR + (1 - nc.ScR)*(f - 0.5f)*2;
                g = nc.ScG + (1 - nc.ScG)*(f - 0.5f)*2;
                b = nc.ScB + (1 - nc.ScB)*(f - 0.5f)*2;
            }
            else
            {
                r = nc.ScR*f*2;
                g = nc.ScG*f*2;
                b = nc.ScB*f*2;
            }

            selectedColor = Color.FromScRgb(a, r, g, b);

            if (OnColorSelected != null)
                OnColorSelected(SelectedColor);
        }

        private void SetColor(Color color)
        {
            SelectedColor = Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]);

            if (OnColorSelected != null)
                OnColorSelected(SelectedColor);
        }

        private void UpdateSlider()
        {
            const float f = 1;
            const float a = 1;

            Color nc = Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]);
            float r = f*nc.ScR;
            float g = f*nc.ScG;
            float b = f*nc.ScB;

            // Update LGB for brightnessSlider
            Border sb1 = brightnessSliderBorder;
            var lgb1 = sb1.Background as LinearGradientBrush;
            if (lgb1 != null)
            {
                lgb1.StartPoint = new Point(0, 1);
                lgb1.EndPoint = new Point(0, 0);

                lgb1.GradientStops[0].Color = Color.FromScRgb(a, 0, 0, 0);
                lgb1.GradientStops[1].Color = Color.FromScRgb(a, r, g, b);
                lgb1.GradientStops[2].Color = Color.FromScRgb(a, 1, 1, 1);
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (e.Source.GetType().Equals(typeof (Image)))
                {
                    var cb = new CroppedBitmap();

                    try
                    {
                        cb = new CroppedBitmap((BitmapSource) (((Image) e.Source).Source),
                                               new Int32Rect((int) Mouse.GetPosition(e.Source as Image).X,
                                                             (int) Mouse.GetPosition(e.Source as Image).Y, 1, 1));
                    }
                    catch (Exception)
                    {
                        // no need to log it..
                        return;
                    }

                    _pixels = new byte[4];
                    try
                    {
                        cb.CopyPixels(_pixels, 4, 0);
                        UpdateMarkerPosition();
                        SetColor(Color.FromRgb(_pixels[2], _pixels[1], _pixels[0]));

                        Mouse.Synchronize();
                        UpdateSlider();

                        if (OnColorSelected != null)
                            OnColorSelected(SelectedColor);
                    }
                    catch
                    {
                        // no need to log it
                    }
                }
            }
        }
    }
}