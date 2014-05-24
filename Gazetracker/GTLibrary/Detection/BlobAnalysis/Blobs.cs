using System;
using System.Collections.Generic;
using AForge;

namespace GTLibrary.Detection.BlobAnalysis
{
  using GTLibrary.Utils;

  public class Blobs
    {
        #region Variables

        private Blob[] blobArray;

        // Use blob ID as key to the lists.
        public Dictionary<int, Blob> blobDir = new Dictionary<int, Blob>();
        public Dictionary<int, List<IntPoint>> bottomEdges = new Dictionary<int, List<IntPoint>>();
        public Dictionary<int, List<IntPoint>> hulls = new Dictionary<int, List<IntPoint>>();
        public Dictionary<int, List<IntPoint>> leftEdges = new Dictionary<int, List<IntPoint>>();
        public Dictionary<int, List<IntPoint>> quadrilaterals = new Dictionary<int, List<IntPoint>>();
        public Dictionary<int, List<IntPoint>> rightEdges = new Dictionary<int, List<IntPoint>>();
        public Dictionary<int, List<IntPoint>> topEdges = new Dictionary<int, List<IntPoint>>();

        #endregion

        #region Constructor

        public Blobs()
        {
            blobArray = new Blob[0];
        }

        public Blobs(Blob[] blobbies)
        {
            foreach (Blob b in blobbies)
                blobDir.Add(b.ID, b);
        }

        #endregion

        #region Get/Set

        public Dictionary<int, Blob> BlobDir
        {
            get { return blobDir; }
            set { blobDir = value; }
        }

        public List<Blob> BlobList
        {
            get
            {
                var blobs = new List<Blob>();

                foreach (Blob b in blobDir.Values)
                    blobs.Add(b);
                return blobs;
            }
        }

        public double TotalArea
        {
            get
            {
                double totalArea = 0;

                foreach (Blob b in blobDir.Values)
                    totalArea += b.Area;

                return totalArea;
            }
        }

        public double AverageFullness
        {
            get
            {
                double averageFullness = 0;

                foreach (Blob b in blobDir.Values)
                    averageFullness += b.Fullness;

                return averageFullness/blobDir.Count;
            }
        }

        public int Count
        {
            get { return blobDir.Count; }
        }

        #endregion

        #region Filter/Eliminate

        public void RemoveBlob(int id)
        {
            try
            {
                if (blobDir.ContainsKey(id))
                    blobDir.Remove(id);

                //if(leftEdges.ContainsKey(id))
                //    leftEdges.Remove(id);

                //if(rightEdges.ContainsKey(id))
                //    rightEdges.Remove(id);

                //if(topEdges.ContainsKey(id))
                //    topEdges.Remove(id);

                //if(bottomEdges.ContainsKey(id))
                //    bottomEdges.Remove(id);

                //if(hulls.ContainsKey(id))
                //    hulls.Remove(id);

                //if(quadrilaterals.ContainsKey(id))
                //    quadrilaterals.Remove(id);
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Error while removing blob");
            }
        }

        #endregion

        #region Filter area / exterior

        public void FilterByArea(int minArea, int maxArea)
        {
            foreach (Blob blob in BlobList)
            {
                if (blob.Area <= 1) // gotta be larger than 1
                    RemoveBlob(blob.ID);

                if (blob.Area < minArea || blob.Area > maxArea)
                    RemoveBlob(blob.ID);
            }
        }


        /// <summary>
        /// This methods eliminates the blobs that are exterior, i.e. those blobs
        /// that have pixels on the edge of the image
        /// </summary>
        public void EliminateExteriorBlobs()
        {
            foreach (Blob b in BlobList)
            {
                if (b.CenterOfGravity.X == 0 || b.CenterOfGravity.Y == 0)
                    RemoveBlob(b.ID);
            }
        }

        #endregion

        #region Filter by distance

        /// <summary>
        /// Select the blob closest to the initial location
        /// </summary>
        /// <param name="initialLocation">GTPoint</param>
        /// <returns>Blob</returns>
        public Blob FilterByDistance(GTPoint initialLocation)
        {
            FilterByDistance(initialLocation, 1);

            return BlobList[0];
        }

        /// <summary>
        /// Select the maxNumber blobs closes to the initial location
        /// </summary>
        /// <param name="initialLocation"></param>
        /// <param name="maxNumber"></param>
        public void FilterByDistance(GTPoint initialLocation, int maxNumber)
        {
            var distances = new double[blobDir.Count];
            var keys = new int[blobDir.Count];
            GTPoint center;

            int i = 0;
            foreach (Blob b in blobDir.Values)
            {
                center = new GTPoint(b.CenterOfGravity.X, b.CenterOfGravity.Y);
                distances[i] = Operations.Distance(center, new GTPoint(initialLocation));
                keys[i] = b.ID;
                i++;
            }

            Array.Sort(distances, keys);

            int j = 0;

            foreach (int key in keys)
            {
                if (j > maxNumber - 1)
                    RemoveBlob(keys[j]);
                j++;
            }
        }

        #endregion

        #region Filter by location (above/below pupil)

        public void FilterByLocation(GTPoint initialLocation, int maxNumber, int position)
        {
            var center = new GTPoint();

            // Glint(s) above pupil, we eliminate blobs below (i.e., when glint.y > pupil.y)
            if (position == 1)
            {
                foreach (Blob blob in BlobList)
                {
                    center = new GTPoint(blob.CenterOfGravity.X, blob.CenterOfGravity.Y);

                    if (center.Y > initialLocation.Y)
                        RemoveBlob(blob.ID);
                }
            }

                // Glint(s) below pupil, we eliminate blobs above (i.e., when glint.y < pupil.y)
            else if (position == -1)
            {
                foreach (Blob blob in BlobList)
                {
                    center = new GTPoint(blob.CenterOfGravity.X, blob.CenterOfGravity.Y);

                    if (center.Y < initialLocation.Y)
                        RemoveBlob(blob.ID);
                }
            }
        }

        #endregion

        #region Filter by eccentricity

        public void FilterByEccentricity(double maxEccentricity)
        {
        }

        #endregion

        internal void FilterByFullness(double minFullness)
        {
            foreach (Blob b in BlobList)
            {
                if (b.Fullness < minFullness)
                    RemoveBlob(b.ID);
            }
        }
    }
}