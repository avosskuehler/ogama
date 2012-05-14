using System.Collections.Generic;
using AForge;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GTLibrary.Detection.BlobAnalysis
{
  public class BlobDetector
  {
    #region Variables

    public BlobCounter blobCounter;
    private bool isFiltering = false;

    #endregion

    #region Constructor

    public BlobDetector()
    {
      blobCounter = new BlobCounter();
    }

    #endregion

    #region Get/Set

    public bool IsFiltering
    {
      get { return isFiltering; }
    }

    #endregion

    #region Public method / Detect blobs

    public Blobs DetectBlobs(Image<Gray, byte> image, int threshold, double minSize, double maxSize, bool isGlint)
    {
      // Detect blobs
      if (isGlint) // Todo: Run recursive blob detection on small ROI images for improved detection/performance
        blobCounter.ProcessImage(image.ThresholdBinary(new Gray(threshold), new Gray(255)));
      else
        blobCounter.ProcessImage(image.ThresholdToZeroInv(new Gray(threshold)));

      // Filtering 
      if (isFiltering) // turn off above
      {
        blobCounter.FilterBlobs = true;
        blobCounter.MinHeight = (int)minSize;
        blobCounter.MinWidth = (int)minSize;
        blobCounter.MaxHeight = (int)maxSize;
        blobCounter.MaxWidth = (int)maxSize;
        blobCounter.ObjectsOrder = ObjectsOrder.Area;
      }

      // Get blob info
      var blobs = new Blobs(blobCounter.GetObjectsInformation());

      //grahamScan = new GrahamConvexHull();

      // Set blobs properties (edges etc)
      foreach (Blob blob in blobs.BlobDir.Values)
      {
        var leftEdge = new List<IntPoint>();
        var rightEdge = new List<IntPoint>();
        var topEdge = new List<IntPoint>();
        var bottomEdge = new List<IntPoint>();

        // collect edge points
        blobCounter.GetBlobsLeftAndRightEdges(blob, out leftEdge, out rightEdge);
        blobCounter.GetBlobsTopAndBottomEdges(blob, out topEdge, out bottomEdge);

        blobs.leftEdges.Add(blob.ID, leftEdge);
        blobs.rightEdges.Add(blob.ID, rightEdge);
        blobs.topEdges.Add(blob.ID, topEdge);
        blobs.bottomEdges.Add(blob.ID, bottomEdge);

        // find convex hull
        //List<IntPoint> edgePoints = new List<IntPoint>();
        //edgePoints.AddRange(leftEdge);
        //edgePoints.AddRange(rightEdge);

        //List<IntPoint> hull = grahamScan.FindHull(edgePoints);
        //blobs.hulls.Add(blob.ID, hull);

        // find quadrilateral
        //List<IntPoint> quadrilateral = PointsCloud.FindQuadrilateralCorners(hull);
        //blobs.quadrilaterals.Add(blob.ID, quadrilateral);

        //// shift all points for vizualization
        //AForge.IntPoint shift = new IntPoint( 1, 1 );

        //PointsCloud.Shift( leftEdge, shift );
        //PointsCloud.Shift( rightEdge, shift );
        //PointsCloud.Shift( topEdge, shift );
        //PointsCloud.Shift( bottomEdge, shift );
        //PointsCloud.Shift( hull, shift );
        //PointsCloud.Shift( quadrilateral, shift );
      }

      return blobs;
    }

    #endregion
  }
}