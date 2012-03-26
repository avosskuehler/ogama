using Emgu.CV;

namespace GTLibrary.Calibration
{
    public class CalibrationData
    {
        public bool Calibrated { get; set; }

        public int AverageError { get; set; }

        public Matrix<double> CoeffsX { get; set; }

        public Matrix<double> CoeffsY { get; set; }

        public Matrix<double> Homography { get; set; }
    }
}