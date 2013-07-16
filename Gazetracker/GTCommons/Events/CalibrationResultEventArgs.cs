namespace GTCommons.Events
{
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using System.IO;

    /// <summary>
    /// Derived from <see cref="System.EventArgs"/>
    /// Class that contains the data for the CalibrationResult event. 
    /// </summary>
    public class CalibrationResultEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Delegate. Handles CalibrationResult event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A <see cref="CalibrationResultEventArgs"/> with the new calibration result.</param>
        public delegate void CalibrationResultEventHandler(object sender, CalibrationResultEventArgs e);

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////
        #region FIELDS

        private readonly int ratingValue;
        private readonly BitmapSource resultBitmapSource;

        #endregion //FIELDS

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region CONSTRUCTION

        /// <summary>
        /// Initializes a new instance of the CalibrationResultEventArgs class (bitmapsource overload)
        /// </summary>
        /// <param name="newResultBitmapSource">The calibration result as a bitmap.</param>
        /// <param name="newRatingValue">The rating value.</param>
        public CalibrationResultEventArgs(BitmapSource newResultBitmapSource, int newRatingValue)
        {
            resultBitmapSource = newResultBitmapSource;
            ratingValue = newRatingValue;
        }

        #endregion //CONSTRUCTION

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////
        #region PROPERTIES

        /// <summary>
        /// Gets the new calibration result as a Bitmap.
        /// </summary>
        /// <value>The calibration result as a GDI+ <see cref="System.Drawing.Bitmap"/>.</value>
        public Bitmap ResultBitmap
        {
            get
            {
                // Do the BitmapSource to Bitmap convertion
                return BitmapFromSource(resultBitmapSource);
            }
        }

        /// <summary>
        /// Gets the new calibration result as a BitmapSource.
        /// </summary>
        /// <value>The calibration result as a WPF <see cref="System.Media.Imaging.BitmapSource"/>.</value>
        public BitmapSource ResultBitmapSource
        {
            get { return resultBitmapSource; }
        }

        /// <summary>
        /// Gets the new calibration result rating value.
        /// </summary>
        /// <value>The calibration result as a rating value.</value>
        public int RatingValue
        {
            get { return ratingValue; }
        }

        #endregion //PROPERTIES

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }

            return bitmap;
        }


    }
}