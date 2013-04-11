using System.Windows;

namespace GTCommons.Events
{
    public class CalibrationPointEventArgs : RoutedEventArgs
    {
        #region Delegates

        public delegate void CalibrationPointEventHandler(object sender, CalibrationPointEventArgs e);

        #endregion

        #region Variables

        private readonly int number;
        private readonly Point point;

        #endregion //FIELDS

        #region Constructor

        public CalibrationPointEventArgs(int number, Point point)
        {
            this.number = number;
            this.point = point;
        }

        #endregion

        #region Get/Set

        public Point Point
        {
            get { return point; }
        }

        public int Number
        {
            get { return number; }
        }

        #endregion
    }
}