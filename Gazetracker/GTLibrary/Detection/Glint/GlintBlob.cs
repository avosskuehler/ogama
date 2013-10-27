using System.Linq;
using Emgu.CV;

//using AForge.Imaging;

namespace GTLibrary.Detection.Glint
{
  using GTLibrary.Detection.BlobAnalysis;
  using GTLibrary.Utils;

  /// <summary>
    /// Glint class, with center and area
    /// </summary>
    //public class GlintBlob
    //{
    //    public Blob blob;
    //    public GTPoint center;
    //    public GlintBlob()
    //    {
    //        center = new GTPoint();
    //    }
    //    public GlintBlob(GlintBlob src)
    //    {
    //        center = new GTPoint(src.center);
    //        blob = src.blob;
    //    }
    //}
    /// <summary>
    /// GlintConfiguration class.
    /// Stores the information of N glints (centers and blobs, blobs not implemented yet)
    /// </summary>
    public class GlintConfiguration
    {
        #region Variables

        private readonly int count;
        private Blobs blobs;
        private GTPoint[] centers;

        #endregion

        #region Constructors

        public GlintConfiguration(int numGlints)
        {
            count = numGlints;
            centers = new GTPoint[numGlints];
            blobs = new Blobs();

            for (int i = 0; i < numGlints; i++)
            {
                centers[i] = new GTPoint();
                blobs.BlobDir[i] = new Blob();
            }
        }

        public GlintConfiguration(GlintConfiguration src)
        {
            count = src.Count;
            centers = new GTPoint[count];
            blobs = new Blobs();

            for (int i = 0; i < count; i++)
            {
                centers[i] = new GTPoint(src.centers[i]);
                blobs.BlobDir[i] = new Blob();
                blobs.BlobDir[i] = src.blobs.BlobDir.ElementAt(i).Value;
            }
        }

        public GlintConfiguration(Blob blob)
        {
            count = 1;
            centers = new GTPoint[1];
            blobs.BlobDir[0] = blob;

            centers[0] = new GTPoint(blob.CenterOfGravity.X, blob.CenterOfGravity.Y);
        }

        public GlintConfiguration(Blobs blobs)
        {
            count = blobs.Count;
            centers = new GTPoint[blobs.Count];
            int i = 0;

            foreach (Blob b in blobs.BlobList)
            {
                centers[i] = new GTPoint(b.CenterOfGravity.X, b.CenterOfGravity.Y);
                i++;
            }

            //OrderGlints();

            this.blobs = blobs;
        }

        public GlintConfiguration(Blob blob0, Blob blob1)
        {
            count = 2;
            centers = new GTPoint[2];
            blobs = new Blobs();

            blobs.BlobDir[0] = blob0;
            blobs.BlobDir[1] = blob1;

            centers[0] = new GTPoint(blob0.CenterOfGravity.X, blob0.CenterOfGravity.Y);
            centers[1] = new GTPoint(blob1.CenterOfGravity.X, blob1.CenterOfGravity.Y);
        }


        /// <summary>
        /// Returns the glints centers in a matrix form, in columns and in homogeneous coordinates
        /// </summary>
        /// <returns>Glints centers in matrix form</returns>
        public Matrix<double> ToMatrix()
        {
            var matrix = new Matrix<double>(3, count);

            for (int i = 0; i < count; i++)
            {
                matrix[0, i] = Centers[i].X;
                matrix[1, i] = Centers[i].Y;
                matrix[2, i] = 1.0;
            }
            return matrix;
        }

        #endregion

        #region Get/Set

        public GTPoint[] Centers
        {
            get { return centers; }
            set { centers = value; }
        }

        public Blobs Blobs
        {
            get { return blobs; }
            set { blobs = value; }
        }

        public int Count
        {
            get { return count; }
        }

        public GTPoint AverageCenter
        {
            get
            {
                if (centers != null && centers.Length != 0)
                    return Operations.Mean(centers);
                else
                    return new GTPoint();
            }
        }

        public int UnfilteredCount { get; set; }

        public double UnfilteredTotalArea { get; set; }

        #endregion
    }
}