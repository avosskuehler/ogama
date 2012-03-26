namespace GTLibrary.Detection.Pupil
{
    //public class Ellipse
    //{
    //    public double a, b, c, d, e, f;
    //    public GTPoint center;
    //    public double angle;
    //    public double semiaxeX;
    //    public double semiaxeY;

    //    public int numberOfPoints;

    //    public Ellipse()
    //    {
    //    }


    //    public Ellipse(Ellipse src)
    //    {
    //        a = src.a;
    //        b = src.b;
    //        c = src.c;
    //        d = src.d;
    //        e = src.e;
    //        f = src.f;

    //        center = new GTPoint(src.center);

    //        angle = src.angle;

    //        semiaxeX = src.semiaxeX;
    //        semiaxeY = src.semiaxeY;
    //    }


    //    private GTPoint CenterFromPolynomial(double[] poly)
    //    {
    //        GTPoint center = new GTPoint();

    //        double a = poly[0];
    //        double b = poly[1];
    //        double c = poly[2];
    //        double d = poly[3];
    //        double f = poly[4];
    //        double g = poly[5];

    //        center.X = (c * d - b * f) / (b * b - a * c);
    //        center.Y = (a * f - b * d) / (b * b - a * c);

    //        return center;

    //    }

    //    private double AngleFromPolynomial(double[] poly)
    //    {
    //        double angle;

    //        double a = poly[0];
    //        double b = poly[1];
    //        double c = poly[2];
    //        double d = poly[3];
    //        double f = poly[4];
    //        double g = poly[5];

    //        angle = 1 / 2;

    //        return angle;
    //    }

    //    private double[] SemiaxesFromPolynomial(double[] poly)
    //    {
    //        double[] semiaxes = new double[2];

    //        double a = poly[0];
    //        double b = poly[1];
    //        double c = poly[2];
    //        double d = poly[3];
    //        double f = poly[4];
    //        double g = poly[5];

    //        semiaxes[0] = Math.Sqrt(2 * (a * f * f + c * d * d + g * b * b - 2 * b * d * f - a * c * g) /
    //            ((b * b - a * c) * ((c - a) * Math.Sqrt(1 + (4 * b * b) / Math.Pow(a - c, 2)) - (c + a))));

    //        semiaxes[1] = Math.Sqrt(2 * (a * f * f + c * d * d + g * b * b - 2 * b * d * f - a * c * g) /
    //            ((b * b - a * c) * ((a - c) * Math.Sqrt(1 + (4 * b * b) / Math.Pow(a - c, 2)) - (c + a))));

    //        return semiaxes;

    //    }

    //    //static public double EuclideanDistanceToPoint(double[] poly, GTPoint point)
    //    static public double EuclideanDistanceToPoint(Ellipse ellipse, GTPoint point)
    //    {
    //        double distance;

    //        int maxIter = 10;

    //        double x = Math.Abs(point.X);
    //        double y = Math.Abs(point.Y);

    //        //double[] semiaxes = SemiaxesFromPolynomial(poly);

    //        //double a = semiaxes[0];
    //        //double b = semiaxes[1];

    //        double a = ellipse.semiaxeY / 2;
    //        double b = ellipse.semiaxeX / 2;

    //        if (b > a)
    //        {
    //            double c = a;
    //            a = b;
    //            b = c;
    //        }

    //        double t;

    //        double f;
    //        double fp;

    //        GTPoint pn = new GTPoint();	//This is the closest point on the ellipse

    //        if (y == 0 && a * a - b * b <= a * x)
    //            t = 0;

    //        else if (x == 0 && b * b - a * a <= b * y)
    //            t = Math.PI / 2;
    //        else
    //        {
    //            t = Math.Atan2(y, x);

    //            int iteration = 0;

    //            while (true)
    //            {
    //                double ct = Math.Cos(t);
    //                double st = Math.Sin(t);

    //                f = (x - Math.Abs(a) * ct) * Math.Abs(a) * st - (y - Math.Abs(b) * st) * Math.Abs(b) * ct;

    //                if (Math.Abs(f) <= 100.0 * r8_epsilon())
    //                    break;

    //                if (iteration > maxIter)
    //                    break;

    //                iteration++;

    //                fp = a * a * st * st + b * b * ct * ct
    //                    + (x - Math.Abs(a) * ct) * Math.Abs(a) * ct
    //                    + (y - Math.Abs(b) * st) * Math.Abs(b) * st;

    //                t = t - f / fp;
    //            }


    //        }

    //        pn.X = Math.Abs(a) * Math.Cos(t);
    //        pn.Y = Math.Abs(b) * Math.Sin(t);

    //        pn.X = Math.Sign(point.X) * pn.X;
    //        pn.Y = Math.Sign(point.Y) * pn.Y;


    //        distance = Operations.Distance(point, pn);

    //        return distance;
    //    }


    //    /// <summary>
    //    /// We use RANSAC to fit an array of GTPoints to an ellipse
    //    /// </summary>
    //    /// <param name="points"></param>
    //    //public Ellipse EllipseRansac(GTPoint[] points, double maxDist)
    //    //{
    //    //    Ellipse finalEllipse /*= new Ellipse(points.Length)*/;

    //    //    int numOfInliers;

    //    //    int N = points.Length;

    //    //    int numIterations = 15;
    //    //    int numPoints = 8;

    //    //    int maxNumInliers = 0;

    //    //    GTPoint[] goodPoints = new GTPoint[numPoints];

    //    //    GTPoint[] randPoints = new GTPoint[numPoints];
    //    //    Ellipse ellipse /*= new DWEllipse(randPoints.Length)*/;

    //    //    double[] distances = new double[points.Length];

    //    //    double[] euclideanDistances = new double[points.Length];

    //    //    for (int i = 0; i < numIterations; i++)
    //    //    {
    //    //        randPoints = Operations.GetRandomPermutation(points, numPoints);

    //    //        ellipse.FitPoints(randPoints);
    //    //        if (ellipse != null)
    //    //        {
    //    //            //distances = CalculateDistanceToEllipse(ellipse, points);
    //    //            double[] semiaxes = new double[2];


    //    //            GTPoint p = new GTPoint();

    //    //            for (int j = 0; j < points.Length; j++)
    //    //            {
    //    //                p.X = points[j].X - ellipse.X;
    //    //                p.Y = points[j].Y - ellipse.Y;

    //    //                euclideanDistances[j] = Ellipse.EuclideanDistanceToPoint(ellipse, p);
    //    //            }

    //    //            numOfInliers = CalculateNumberOfInliers(euclideanDistances, maxDist);


    //    //            if (numOfInliers > maxNumInliers)
    //    //            {
    //    //                for (int k = 0; k < randPoints.Length; k++)
    //    //                    goodPoints[k] = new GTPoint(randPoints[k]);

    //    //                maxNumInliers = numOfInliers;
    //    //            }
    //    //            // If more than 80% of the points are inliers, we stop
    //    //            if (numOfInliers > points.Length - Math.Round(points.Length * 0.2))
    //    //                break;
    //    //        }

    //    //    }

    //    //    // If we have a set of good points, we fit them to the final ellipse, pupilEllipse
    //    //    if (goodPoints[0] != null)
    //    //        finalEllipse.FitPoints(goodPoints);

    //    //    return finalEllipse;

    //    //}

    //    private int CalculateNumberOfInliers(double[] distances, double maxDist)
    //    {
    //        int numberOfInliers = 0;

    //        for (int i = 0; i < distances.Length; i++)
    //        {
    //            if (distances[i] < maxDist)
    //                numberOfInliers++;
    //        }

    //        return numberOfInliers;

    //    }


    //    static private double r8_epsilon()
    //    {
    //        double r;

    //        r = 1.0E+00;

    //        while (1.0E+00 < (double)(1.0E+00 + r))
    //            r = r / 2.0E+00;

    //        return (2.0E+00 * r);
    //    }
    //}
}