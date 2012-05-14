using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using GazeTrackingLibrary.Utils;

// Partly inspired in http://www.emgu.com/forum/viewtopic.php?f=3&t=205

namespace GazeTrackingLibrary.Detection.Blob
{
	public class BlobResult
	{
		public int numBlobs;

		public Blo[] detectedBlobs;

		public BlobResult()
		{
			numBlobs = 0;
		}


		public BlobResult(Image<Gray, byte> image, int threshold, bool findMoments)
		{
			if (image.IsROISet)
			{
				Image<Gray, byte> roiImage = new Image<Gray, byte>(image.ROI.Size);
				roiImage = image.Copy(image.ROI);
				detectedBlobs = GetBlobs(roiImage, threshold, IntPtr.Zero, false, findMoments);
			}
			else
				detectedBlobs = GetBlobs(image, threshold, IntPtr.Zero, false, findMoments);
			numBlobs = detectedBlobs.Length;
		}


		public BlobResult(Image<Gray, byte> image, int threshold, IntPtr maskImage,
			bool borderColor, bool findMoments)
		{
			detectedBlobs = GetBlobs(image, threshold, maskImage, borderColor, findMoments);
			numBlobs = detectedBlobs.Length;


		}

		public void ClearBlobResult()
		{
			for (int i = 0; i < numBlobs; i++)
			{
				detectedBlobs[i].Clear();
			}

			numBlobs = 0;
			System.GC.Collect();
		}

		public BlobObject GetBlob(int index)
		{
			return detectedBlobs[index];
		}


		#region Filter by area

		public void FilterByArea(int minArea, int maxArea)
		{
			BlobObject[] finalBlobs;

			int numFilteredBlobs = 0;

			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Area > minArea && detectedBlobs[i].Area < maxArea)
					numFilteredBlobs++;
			}

			finalBlobs = new BlobObject[numFilteredBlobs];

			int counter = 0;
			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Area > minArea && detectedBlobs[i].Area < maxArea)
				{
					finalBlobs[counter] = detectedBlobs[i];
					counter++;
				}
				else
				{
					detectedBlobs[i].Clear();
				}
				
				
			}

			detectedBlobs = finalBlobs;
			numBlobs = numFilteredBlobs;
		}

		#endregion


		#region Eliminate blobs


		/// <summary>
		/// This methods eliminates the blobs that are exterior, i.e. those blobs
		/// that have pixels on the edge of the image
		/// </summary>
		public void EliminateExteriorBlobs()
		{
			BlobObject[] finalBlobs;

			int numFilteredBlobs = 0;

			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Exterior == 0)
					numFilteredBlobs++;
			}

			finalBlobs = new BlobObject[numFilteredBlobs];

			int counter = 0;
			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Exterior == 0) 
				{
					finalBlobs[counter] = detectedBlobs[i];
					counter++;
				}
				else
				{
					detectedBlobs[i].Clear();
				}


			}

			detectedBlobs = finalBlobs;
			numBlobs = numFilteredBlobs;
		}

		#endregion


		#region Filter by distance
		/// <summary>
		/// Select the blob closest to the initial location
		/// </summary>
		/// <param name="initialLocation">GTPoint</param>
		/// <returns>Blob</returns>
		public BlobObject FilterByDistance(GTPoint initialLocation)
		{
			double[] distances = new double[numBlobs];
			int[] keys = new int[numBlobs];
			GTPoint center;

			for (int i = 0; i < numBlobs; i++)
			{
				center = new GTPoint(detectedBlobs[i].CentroidX, detectedBlobs[i].CentroidY);
				distances[i] = Operations.Distance(center, new GTPoint(initialLocation));
				keys[i] = i;
			}

			Array.Sort(distances, keys);		

			return GetBlob(keys[0]);

		}


		public void FilterByDistance(GTPoint initialLocation, int N)
		{
			if(N > numBlobs)
				return;

			BlobObject[] finalBlobs = new BlobObject[N];

			double[] distances = new double[numBlobs];
			int[] keys = new int[numBlobs];
			//Matrix<double> distances = new Matrix<double>(numBlobs, 1);
			GTPoint center;

			for (int i = 0; i < numBlobs; i++)
			{
				center = new GTPoint(detectedBlobs[i].CentroidX, detectedBlobs[i].CentroidY);
				distances[i] = Operations.Distance(center, new GTPoint(initialLocation));
				keys[i] = i;
			}

			Array.Sort(distances, keys);

			for (int i = 0; i < numBlobs; i++)
			{
				if (i < N)
					finalBlobs[i] = detectedBlobs[keys[i]];
				else
					detectedBlobs[keys[i]].Clear();
			}

			detectedBlobs = finalBlobs;
			numBlobs = N;

	
		}

		#endregion


		#region Filter by eccentricity

		public void FilterByEccentricity(double maxEccentricity)
		{
			BlobObject[] finalBlobs;

			int numFilteredBlobs = 0;

			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Eccentricity < maxEccentricity)
					numFilteredBlobs++;
			}

			finalBlobs = new BlobObject[numFilteredBlobs];

			int counter = 0;
			for (int i = 0; i < numBlobs; i++)
			{
				if (detectedBlobs[i].Eccentricity < maxEccentricity)
				{
					finalBlobs[counter] = detectedBlobs[i];
					counter++;
				}
				else
					detectedBlobs[i].Clear();
			}

			detectedBlobs = finalBlobs;
			numBlobs = numFilteredBlobs;
		}

		#endregion



		#region Get Blobs
		private BlobObject[] GetBlobs(Image<Gray, byte> image,
						int threshold,
					   IntPtr maskImage,
					   bool borderColor,
					   bool findMoments)
		{
			int count;

			IntPtr vector = BlobsInvoke.CvGetBlobs(image.Ptr, threshold, maskImage, borderColor, findMoments, out count);
			IntPtr[] blobsPtrs = new IntPtr[count];

			GCHandle handle = GCHandle.Alloc(blobsPtrs, GCHandleType.Pinned);
			Emgu.Util.Toolbox.memcpy(handle.AddrOfPinnedObject(), vector, count * Marshal.SizeOf(typeof(IntPtr)));
			handle.Free();

			BlobObject[] Blobs = new BlobObject[count];
			for (int i = 0; i < blobsPtrs.Length; i++)
				Blobs[i] = new BlobObject(blobsPtrs[i]);

            System.GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

			return Blobs;
		}

		#endregion



		#region Draw

		public void DrawBlobs(Image<Bgr, byte> image,
				BlobObject[] blobs,
				bool fill,
				bool drawBoundingBox, Color BoundingBoxColor,
				bool drawConvexHull, Color ConvexHullColor,
				bool drawEllipse, Color EllipseColor,
				bool drawCentroid, Color CentroidColor,
				bool drawAngle, Color AngleColor)
		{
			Random r = new Random(0);
			foreach (var b in blobs)
			{
				if (fill)
					b.FillBlob(image.Ptr, new MCvScalar(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));
				if (drawBoundingBox)
					image.Draw(b.BoundingBox, new Bgr(BoundingBoxColor), 1);
				if (drawConvexHull)
					image.DrawPolyline(b.ConvexHull, true, new Bgr(ConvexHullColor), 1);
				if (drawEllipse)
					image.Draw(b.BestFitEllipse, new Bgr(EllipseColor), 1);

				if (drawCentroid)
				{
					image.Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
										   new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
						 new Bgr(CentroidColor), 1);
					image.Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
										   new Point((int)b.CentroidX, (int)b.CentroidY + 4)),
						 new Bgr(CentroidColor), 1);
				}
				if (drawAngle)
				{
					double x1, x2, y1, y2;
					x1 = b.CentroidX - 0.005 * b.Area * Math.Cos(b.Angle);
					y1 = b.CentroidY - 0.005 * b.Area * Math.Sin(b.Angle);
					x2 = b.CentroidX + 0.005 * b.Area * Math.Cos(b.Angle);
					y2 = b.CentroidY + 0.005 * b.Area * Math.Sin(b.Angle);
					image.Draw(new LineSegment2D(new Point((int)x1, (int)y1),
									new Point((int)x2, (int)y2)),
						 new Bgr(AngleColor), 1);
				}
			}
		}

		public Image<Bgr, byte> DrawBlobs(Image<Bgr, byte> image, BlobObject[] blobs)
		{
			Image<Bgr, byte> imageFinal;
			imageFinal = image.Copy();

			foreach (var b in blobs)
			{
				imageFinal.Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
									   new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
					 new Bgr(Color.Red), 1);
				imageFinal.Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
									   new Point((int)b.CentroidX, (int)b.CentroidY + 4)),
					 new Bgr(Color.Red), 1);

			}

			return imageFinal;

		}
		#endregion


    }
}
