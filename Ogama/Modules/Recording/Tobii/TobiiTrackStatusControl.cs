using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Eyetracking.Sdk;

namespace Ogama.Modules.Recording.TobiiDevice
{
    public partial class TobiiTrackStatusControl : UserControl
    {
        private Point3D _leftEye;
        private Point3D _rightEye;
        private int _leftValidity;
        private int _rightValidity;
        private SolidBrush _brush;
        private SolidBrush _eyeBrush;
        private Queue<GazeDataItem> _dataHistory;

        private static int HistorySize = 30;
        private static int BarHeight = 25;
        private static int EyeRadius = 8;

        public TobiiTrackStatusControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            _dataHistory = new Queue<GazeDataItem>(HistorySize);

            _brush = new SolidBrush(Color.Red);
            _eyeBrush = new SolidBrush(Color.White);

            _leftValidity = 4;
            _rightValidity = 4;
        }

        public void OnGazeData(GazeDataItem gd)
        {
            // Add data to history
            _dataHistory.Enqueue(gd);

            // Remove history item if necessary
            while (_dataHistory.Count > HistorySize)
            {
                _dataHistory.Dequeue();
            }

            _leftValidity = gd.LeftValidity;
            _rightValidity = gd.RightValidity;

            _leftEye = gd.LeftEyePosition3DRelative;
            _rightEye = gd.RightEyePosition3DRelative;
            
            Invalidate();
        }

        public void Clear()
        {
            _dataHistory.Clear();
            _leftValidity = 0;
            _rightValidity = 0;
            _leftEye = new Point3D();
            _rightEye = new Point3D();

            Invalidate();
        }


        /// <summary>
        /// Gets the current brush
        /// </summary>
        private SolidBrush Brush
        {
            get
            {
                if (_leftValidity == 4 && _rightValidity == 4)
                {
                    _brush.Color = Color.Red;
                }
                else if (_leftValidity == 0 && _rightValidity == 0)
                {
                    _brush.Color = Color.Lime;
                }
                else if (_leftValidity == 2 && _rightValidity == 2)
                {
                    _brush.Color = Color.Orange;
                }
                else
                {
                    _brush.Color = Color.Yellow;
                }

                return _brush;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Compute status bar color
            _brush.Color = ComputeStatusColor();
            
            // Draw bottom bar
            e.Graphics.FillRectangle(_brush, new Rectangle(0, Height - BarHeight, Width, BarHeight));
        
            // Draw eyes

            if (_leftValidity <= 2)
            {
                RectangleF r = new RectangleF((float) ((1.0 - _leftEye.X) * Width - EyeRadius), (float) (_leftEye.Y * Height - EyeRadius), 2 * EyeRadius, 2 * EyeRadius);
                e.Graphics.FillEllipse(_eyeBrush, r);
            }

            if(_rightValidity <= 2)
            {
                RectangleF r = new RectangleF((float) ((1 - _rightEye.X) * Width - EyeRadius), (float) (_rightEye.Y * Height - EyeRadius), 2 * EyeRadius, 2 * EyeRadius);
                e.Graphics.FillEllipse(_eyeBrush, r);
            }
        }

        private Color ComputeStatusColor()
        {
            if (!Enabled)
                return Color.Gray;

            int quality = 0;
            int count = 0;

            foreach (GazeDataItem item in _dataHistory)
            {
                if(item.LeftValidity == 4 && item.RightValidity == 4)
                {
                    quality += 0;
                }
                else if (item.LeftValidity == 0 && item.RightValidity == 0)
                {
                    quality += 2;
                }
                else
                {
                    quality++;
                }

                count++;
            }

            float q = (count == 0 ? 0 : quality / (2F*count));
            
            if(q > 0.8)
            {
                return Color.Lime;
            }
            if(q < 0.1)
            {
                return Color.Red;
            }

            return Color.Red;
        }
    }
}