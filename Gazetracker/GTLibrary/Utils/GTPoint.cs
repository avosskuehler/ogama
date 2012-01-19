using System;
using System.Drawing;
using Emgu.CV;

namespace GTLibrary.Utils
{
    /// <summary>
    /// This class defines a point with X and Y coordinates as double
    /// </summary>
    public class GTPoint
    {
        public double X; // most frequent call in the lib
        public double Y; // made these public to avoid the method call that a get/set actually is

        public GTPoint()
        {
        }

        public GTPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public GTPoint(GTPoint point)
        {
            X = point.X;
            Y = point.Y;
        }

        public GTPoint(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point ToPoint()
        {
            return new Point((int) Math.Round(X), (int) Math.Round(Y));
        }

        public static GTPoint operator +(GTPoint p1, GTPoint p2)
        {
            return new GTPoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static GTPoint operator -(GTPoint p1, GTPoint p2)
        {
            return new GTPoint(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static GTPoint operator /(GTPoint p1, int k)
        {
            return new GTPoint(p1.X/k, p1.Y/k);
        }

        public static GTPoint operator *(GTPoint p1, int k)
        {
            return new GTPoint(p1.X*k, p1.Y*k);
        }

        public Matrix<double> ToMatrix()
        {
            var matrix = new Matrix<double>(3, 1);
            matrix[0, 0] = X;
            matrix[1, 0] = Y;
            matrix[2, 0] = 1.0;

            return matrix;
        }
    }
}