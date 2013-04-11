using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace GTLibrary.Utils
{
    /// <summary>
    /// This class defines some basic operations
    /// </summary>
    public class Operations
    {
        #region Mean

        /// <summary>
        /// Mean of an array of doubles
        /// </summary>
        /// <param name="num">Array of doubles</param>
        /// <returns>Mean of the array</returns>
        public static double Mean(double[] num)
        {
            double sum = 0;
            for (int i = 0; i < num.Length; i++)
            {
                sum = sum + num[i];
            }

            return sum/num.Length;
        }

        /// <summary>
        /// Mean GTPoint of an array of GTPoints
        /// </summary>
        /// <param name="points">Array of GTPoints</param>
        /// <returns>GTPoint containing the mean</returns>
        public static GTPoint Mean(GTPoint[] points)
        {
            double x = 0;
            double y = 0;

            for (int i = 0; i < points.Length; i++)
            {
                x = x + points[i].X;
                y = y + points[i].Y;
            }

            return new GTPoint(x/points.Length, y/points.Length);
        }


        /// <summary>
        /// Mean GTPoint of an array of Points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static GTPoint Mean(Point[] points)
        {
            double x = 0;
            double y = 0;

            for (int i = 0; i < points.Length; i++)
            {
                x = x + points[i].X;
                y = y + points[i].Y;
            }

            return new GTPoint(x/points.Length, y/points.Length);
        }

        #endregion

        #region Min

        /// <summary>
        /// Returns the index of the array that contains the minimum
        /// </summary>
        /// <param name="num">Array of doubles</param>
        /// <returns>Position of the array that contains the minimum value</returns>
        public static int Min(double[] num)
        {
            int indexMin = 0;
            double min = 10000000000000000;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] < min)
                {
                    indexMin = i;
                    min = num[i];
                }
            }
            return indexMin;
        }

        #endregion

        #region Max

        /// <summary>
        /// Returns the index of the array that contains the maximum
        /// </summary>
        /// <param name="num">Array of doubles</param>
        /// <returns>Position of the array that contains the maximum value</returns>
        public static int Max(double[] num)
        {
            int indexMax = 0;
            double max = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] > max)
                {
                    indexMax = i;
                    max = num[i];
                }
            }
            return indexMax;
        }

        #endregion

        #region Variance

        /// <summary>
        /// Variance of an array of doubles
        /// </summary>
        /// <param name="num">Array of doubles</param>
        /// <returns>Variance</returns>
        public static double Variance(double[] num)
        {
            if (num.Length < 2)
                return 0;

            double Sum = 0.0, SumOfSqrs = 0.0;
            for (int i = 0; i < num.Length; i++)
            {
                Sum += num[i];
                SumOfSqrs += Math.Pow(num[i], 2);
            }
            double topSum = (num.Length*SumOfSqrs) - (Math.Pow(Sum, 2));
            double n = num.Length;
            return (topSum/(n*(n - 1)));
        }

        /// <summary>
        /// Variance of an array of Points
        /// </summary>
        /// <param name="num">Array of type Point</param>
        /// <returns>Variance (GTPoint)</returns>
        public static GTPoint Variance(Point[] num)
        {
            if (num.Length < 2)
                return new GTPoint(0, 0);

            double SumX = 0.0, SumOfSqrsX = 0.0;
            double SumY = 0.0, SumOfSqrsY = 0.0;

            for (int i = 0; i < num.Length; i++)
            {
                SumX += num[i].X;
                SumY += num[i].Y;

                SumOfSqrsX += Math.Pow(num[i].X, 2);
                SumOfSqrsY += Math.Pow(num[i].Y, 2);
            }
            double topSumX = (num.Length*SumOfSqrsX) - (Math.Pow(SumX, 2));
            double topSumY = (num.Length*SumOfSqrsY) - (Math.Pow(SumY, 2));

            double n = num.Length;
            return new GTPoint((topSumX/(n*(n - 1))), topSumY/(n*(n - 1)));
        }


        /// <summary>
        /// Variance of an array of GTPoints
        /// </summary>
        /// <param name="num">Array of GTPoint</param>
        /// <returns>Variance</returns>
        public static GTPoint Variance(GTPoint[] num)
        {
            if (num.Length < 2)
                return new GTPoint(0, 0);

            double SumX = 0.0, SumOfSqrsX = 0.0;
            double SumY = 0.0, SumOfSqrsY = 0.0;

            for (int i = 0; i < num.Length; i++)
            {
                SumX += num[i].X;
                SumY += num[i].Y;

                SumOfSqrsX += Math.Pow(num[i].X, 2);
                SumOfSqrsY += Math.Pow(num[i].Y, 2);
            }
            double topSumX = (num.Length*SumOfSqrsX) - (Math.Pow(SumX, 2));
            double topSumY = (num.Length*SumOfSqrsY) - (Math.Pow(SumY, 2));

            double n = num.Length;
            return new GTPoint((topSumX/(n*(n - 1))), topSumY/(n*(n - 1)));
        }

        #endregion

        #region Standard deviation

        /// <summary>
        /// Standard deviation of an array of doubles
        /// </summary>
        /// <param name="num"></param>
        /// <returns>double</returns>
        public static double StandardDeviation(double[] num)
        {
            return Math.Sqrt(Variance(num));
        }

        /// <summary>
        /// Standard deviation of an array of Points
        /// </summary>
        /// <param name="num"></param>
        /// <returns>GTPoint</returns>
        public static GTPoint StandardDeviation(Point[] num)
        {
            GTPoint variance = Variance(num);
            return new GTPoint(Math.Sqrt(variance.X), Math.Sqrt(variance.Y));
        }

        /// <summary>
        /// Standard deviation of an array of GTPoints
        /// </summary>
        /// <param name="num"></param>
        /// <returns>GTPoint</returns>
        public static GTPoint StandardDeviation(GTPoint[] num)
        {
            GTPoint variance = Variance(num);
            return new GTPoint(Math.Sqrt(variance.X), Math.Sqrt(variance.Y));
        }

        #endregion

        #region Sum

        /// <summary>
        /// Sum of an array of ints
        /// </summary>
        /// <param name="numbers">Array of ints</param>
        /// <returns>Sum</returns>
        public static int Sum(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum = sum + numbers[i];
            }
            return sum;
        }

        /// <summary>
        /// Sum of an array of doubles
        /// </summary>
        /// <param name="numbers">Array of doubles</param>
        /// <returns>Sum</returns>
        public static double Sum(double[] numbers)
        {
            double sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum = sum + numbers[i];
            }
            return sum;
        }

        #endregion

        #region Convert matrix to array

        /// <summary>
        /// Converts a Matrix of points into an array of GTPoints
        /// </summary>
        /// <param name="pointsMatrix">Nx2 matrix, with first column being X coordinate
        /// and second column being Y coordinate</param>
        /// <returns>Array of GTpoints</returns>
        public static GTPoint[] ConvertToArray(Matrix<double> pointsMatrix)
        {
            var pointsArray = new GTPoint[pointsMatrix.Rows];

            for (int i = 0; i < pointsMatrix.Rows; i++)
            {
                pointsArray[i] = new GTPoint(pointsMatrix[i, 0], pointsMatrix[i, 1]);
            }

            return pointsArray;
        }

        #endregion

        #region Permutation

        /// <summary>
        /// Permutate a List of integers
        /// </summary>
        /// <param name="orgArray">Original array</param>
        /// <returns>Permutated list</returns>
        public static List<int> Permutate(List<int> orgArray)
        {
            var PermutationArray = new List<int>();
            var randomGenerator = new Random();
            int randNum;
            while (true)
            {
                randNum = randomGenerator.Next(orgArray.Count);
                PermutationArray.Add(orgArray[randNum]);
                orgArray.RemoveAt(randNum);
                if (orgArray.Count == 0)
                    break;
            }
            return PermutationArray;
        }

        /// <summary>
        /// Permutate a List of doubles
        /// </summary>
        /// <param name="orgArray">Original array</param>
        /// <returns>Permutated list</returns>
        public static List<double> Permutate(List<double> orgArray)
        {
            var PermutationArray = new List<double>();
            var randomGenerator = new Random();
            int randNum;
            while (true)
            {
                randNum = randomGenerator.Next(orgArray.Count);
                PermutationArray.Add(orgArray[randNum]);
                orgArray.RemoveAt(randNum);
                if (orgArray.Count == 0)
                    break;
            }
            return PermutationArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="numPoints"></param>
        /// <returns></returns>
        public static GTPoint[] GetRandomPermutation(GTPoint[] points, int numPoints)
        {
            var randPoints = new GTPoint[numPoints];

            var randomGenerator = new Random();

            var mask = new int[points.Length];
            int randomNumber;

            for (int i = 0; i < mask.Length; i++)
                mask[i] = 0;

            while (Sum(mask) < numPoints)
            {
                randomNumber = randomGenerator.Next(points.Length);
                if (mask[randomNumber] == 0)
                    mask[randomNumber] = 1;
            }

            int counter = 0;
            for (int k = 0; k < mask.Length; k++)
            {
                if (mask[k] == 1)
                {
                    randPoints[counter] = new GTPoint(points[k]);
                    counter++;
                }
            }

            return randPoints;
        }

        #endregion

        #region Geometry

        #region Distance

        /// <summary>
        /// Distance between two GTPoints
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double Distance(GTPoint p1, GTPoint p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static double Distance(Point p1, GTPoint p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static double Distance(GTPoint p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        #endregion

        #region Angle

        /// <summary>
        /// Calculate the angle between 2 GTPoints
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="oldPoint"></param>
        /// <returns></returns>
        public static double CalculateAngle(GTPoint newPoint, GTPoint oldPoint)
        {
            double angle;

            if (newPoint.X - oldPoint.X == 0)
            {
                if (newPoint.Y - oldPoint.Y > 0)
                    angle = 90;
                else
                    angle = -90;
            }
            else
            {
                angle = Math.Atan((newPoint.Y - oldPoint.Y)/(newPoint.X - oldPoint.X));
                angle = angle*180/Math.PI;
            }

            return angle;
        }


        //public static double CalculateAngle(GTPoint p1, GTPoint p2) 
        //{
        //    // Negate X and Y values
        //    double pxRes = p1.X - p2.X;
        //    double pyRes = p1.Y - p2.Y;
        //    double angle = 0.0;

        //    // Calculate the angle
        //    if (pxRes == 0.0)
        //    {
        //       if (pxRes == 0.0)
        //             angle = 0.0;
        //       else if (pyRes > 0.0)          
        //           angle = System.Math.PI / 2.0;
        //       else
        //           angle = System.Math.PI * 3.0 / 2.0;
        //    }

        //    else if (pyRes == 0.0)
        //    {
        //       if (pxRes > 0.0)
        //           angle = 0.0;
        //       else
        //           angle = System.Math.PI;
        //    }
        //    else
        //    {
        //       if (pxRes < 0.0)
        //           angle = System.Math.Atan(pyRes / pxRes) + System.Math.PI;
        //       else if (pyRes < 0.0)          
        //           angle = System.Math.Atan(pyRes / pxRes) + (2 * System.Math.PI);
        //       else
        //           angle = System.Math.Atan(pyRes / pxRes);
        //    }

        //    // Convert to degrees
        //    angle = angle * 180 / System.Math.PI;
        //    return angle;
        //}

        // Overload

        public static double CalculateAngle(Point p1, Point p2)
        {
            return CalculateAngle(new GTPoint(p1.X, p1.Y), new GTPoint(p2.X, p2.Y));
        }

        public static double CalculateAngle(System.Windows.Point p1, System.Windows.Point p2)
        {
            return CalculateAngle(new GTPoint(p1.X, p1.Y), new GTPoint(p2.X, p2.Y));
        }

        #endregion

        #region IsWithinBounds

        public static bool IsWithinBounds(Rectangle small, Rectangle large)
        {
            if (
                small.Y >= large.Y &&
                small.Y + small.Height <= large.Y + large.Height &&
                small.X >= large.X &&
                small.X + small.Width <= large.X + large.Width)

                return true;
            else
                return false;
        }

        #endregion

        #region Get Max Distance

        public static double GetMaxDistanceOnWindow(List<GTPoint> data)
        {
            GTPoint centroid = Mean(data.ToArray());
            int maxDistance = 150;

            double maxDist = 0;
            double dist = 0;

            for (int i = 0; i < data.Count; i++)
            {
                dist = Distance(centroid, data[i]);
                if (dist > maxDistance)
                    maxDist = dist;
            }

            return maxDist;
        }

        public static double GetMaxDistanceOnWindow(List<Point> data)
        {
            GTPoint centroid = Mean(data.ToArray());

            int maxDistance = 150;

            double maxDist = 0;
            double dist = 0;

            for (int i = 0; i < data.Count; i++)
            {
                dist = Distance(centroid, data[i]);
                if (dist > maxDistance)
                    maxDist = dist;
            }

            return maxDist;
        }

        #endregion

        #region Order Points

        /// <summary>
        /// Order any 4 points
        /// </summary>
        /// <param name="points">Array of 4 points</param>
        /// <returns>Ordered array of the 4 points</returns>
        public static GTPoint[] OrderPoints(GTPoint[] points)
        {
            int N = 4;
            var orderedPoints = new GTPoint[N];
            var center = new GTPoint();

            if (points.Length != N)
            {
                Console.WriteLine("error ordering 4 glints");
            }
            else
            {
                center = Mean(points);
                var distances = new double[N - 1];
                for (int i = 0; i < N; i++)
                {
                    if (points[i].X <= center.X && points[i].Y <= center.Y)
                        orderedPoints[0] = points[i];
                    else if (points[i].X >= center.X && points[i].Y <= center.Y)
                        orderedPoints[1] = points[i];
                    else if (points[i].X >= center.X && points[i].Y >= center.Y)
                        orderedPoints[2] = points[i];
                    else if (points[i].X <= center.X && points[i].Y >= center.Y)
                        orderedPoints[3] = points[i];
                }

                try
                {
                    for (int i = 0; i < N; i++)
                    {
                        if (orderedPoints[i] == null)
                        {
                            //Console.WriteLine("Incorrect 4 points detected");
                            orderedPoints = new GTPoint[1];
                            return orderedPoints;
                        }
                    }
                }
                catch
                {
                    //Console.WriteLine("Exception ordering points");
                    orderedPoints = new GTPoint[1];
                }
            }

            return orderedPoints;
        }

        /// <summary>
        /// Order any 4 points
        /// </summary>
        /// <param name="points">Array of 4 points</param>
        /// <returns>Ordered array of the 4 points</returns>
        public static Point[] OrderPoints(Point[] points)
        {
            int N = 4;
            var orderedPoints = new Point[N];

            if (points.Length != N)
            {
                // Error
            }
            else
            {
                var distances = new double[N - 1];

                distances[0] = Distance(points[0], points[1]);
                distances[1] = Distance(points[0], points[2]);
                distances[2] = Distance(points[0], points[3]);

                int distMax = Max(distances) + 1;
                int distMin = Min(distances) + 1;

                orderedPoints[0] = points[0];
                orderedPoints[1] = points[distMin];
                orderedPoints[2] = points[distMax];
                orderedPoints[3] = points[6 - distMin - distMax];
            }

            return orderedPoints;
        }

        #endregion

        #endregion

        #region Solve least squares

        public static Matrix<double> SolveLeastSquares(Matrix<double> A, Matrix<double> b)
        {
            int M = A.Rows;
            int N = A.Cols;

            var x = new Matrix<double>(N, 1);

            var U = new Matrix<double>(M, M);
            var F = new Matrix<double>(M, N);
            var V = new Matrix<double>(N, N);

            var c = new Matrix<double>(M, 1);
            var y = new Matrix<double>(N, 1);

            CvInvoke.cvSVD(A.Ptr, F.Ptr, U.Ptr, V.Ptr, SVD_TYPE.CV_SVD_DEFAULT);

            c = U.Transpose().Mul(b);

            for (int i = 0; i < N; i++)
            {
                y[i, 0] = c[i, 0]/F[i, i];
            }

            x = V.Mul(y);

            return x;
        }

        #endregion

        #region Homographies

        public static Matrix<double> Homography2D(Matrix<double> m1, Matrix<double> m2)
        {
            //int N = m1.Cols;

            var H = new Matrix<double>(3, 3);
            //Matrix<double> A = new Matrix<double>(2 * N, 8);
            //Matrix<double> B = new Matrix<double>(2 * N, 1);

            //A.SetZero();

            //for (int i = 0; i < N; i++)
            //{
            //    A[i, 0] = m1[0, i];
            //    A[i + N, 3] = m1[0, i];
            //    A[i, 1] = m1[1, i];
            //    A[i + N, 4] = m1[1, i];
            //    A[i, 2] = 1.0;
            //    A[i + N, 5] = 1.0;
            //    A[i, 6] = -m1[0, i] * m2[0, i];
            //    A[i, 7] = -m1[1, i] * m2[0, i];
            //    A[i + N, 6] = -m1[0, i] * m2[1, i];
            //    A[i + N, 7] = -m1[1, i] * m2[1, i];
            //    B[i, 0] = m2[0, i];
            //    B[i + N, 0] = m2[1, i];
            //}

            CvInvoke.cvFindHomography(m1.Ptr, m2.Ptr, H.Ptr, HOMOGRAPHY_METHOD.DEFAULT, 0, new IntPtr());


            return H;
        }

        #endregion
    }
}