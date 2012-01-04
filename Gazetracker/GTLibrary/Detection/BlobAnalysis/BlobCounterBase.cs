﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using GTLibrary.Utils;
using Image = System.Drawing.Image;

namespace GTLibrary.Detection.BlobAnalysis
{
  using GTLibrary.Utils;

  // All credits for this code goes to AForgeNet, we're adopting it and making small modifications to fit our framework.

    /// <summary>
    /// Possible object orders.
    /// </summary>
    /// 
    /// <remarks>The enumeration defines possible sorting orders of objects, found by blob
    /// counting classes.</remarks>
    /// 
    public enum ObjectsOrder
    {
        /// <summary>
        /// Unsorted order (as it is collected by algorithm).
        /// </summary>
        None,

        /// <summary>
        /// Objects are sorted by size in descending order (bigger objects go first).
        /// Size is calculated as <b>Width * Height</b>.
        /// </summary>
        Size,

        /// <summary>
        /// Objects are sorted by area in descending order (bigger objects go first).
        /// </summary>
        Area,

        /// <summary>
        /// Objects are sorted by Y coordinate, then by X coordinate in ascending order
        /// (smaller coordinates go first).
        /// </summary>
        YX,

        /// <summary>
        /// Objects are sorted by X coordinate, then by Y coordinate in ascending order
        /// (smaller coordinates go first).
        /// </summary>
        XY
    }

    /// <summary>
    /// Base class for different blob counting algorithms.
    /// </summary>
    /// 
    /// <remarks><para>The class is abstract and serves as a base for different blob counting algorithms.
    /// Classes, which inherit from this base class, require to implement <see cref="BuildObjectsMap"/>
    /// method, which does actual building of object's label's map.</para>
    /// 
    /// <para>For blobs' searcing usually all inherited classes accept binary images, which are actually
    /// grayscale thresholded images. But the exact supported format should be checked in particular class,
    /// inheriting from the base class. For blobs' extraction the class supports grayscale (8 bpp indexed)
    /// and color images (24 and 32 bpp).</para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// // create an instance of blob counter algorithm
    /// BlobCounterBase bc = new ...
    /// // set filtering options
    /// bc.FilterBlobs = true;
    /// bc.MinWidth  = 5;
    /// bc.MinHeight = 5;
    /// // process binary image
    /// bc.ProcessImage( image );
    /// Blob[] blobs = bc.GetObjects( image, false );
    /// // process blobs
    /// foreach ( Blob blob in blobs )
    /// {
    ///     // ...
    ///     // blob.Rectangle - blob's rectangle
    ///     // blob.Image - blob's image
    /// }
    /// </code>
    /// </remarks>
    /// 
    public abstract class BlobCounterBase
    {
        #region Variables

        // found blobs
        private readonly List<Blob> blobs = new List<Blob>();

        // objects' sort order

        // coupled size filtering or not
        private bool coupledSizeFiltering;
        private IBlobsFilter filter;
        private bool filterBlobs;

        /// <summary>
        /// Height of processed image.
        /// </summary>
        protected int imageHeight;

        /// <summary>
        /// Width of processed image.
        /// </summary>
        protected int imageWidth;

        // blobs' minimal and maximal size
        private int maxHeight = int.MaxValue;
        private int maxWidth = int.MaxValue;
        private int minHeight = 1;
        private int minWidth = 1;

        /// <summary>
        /// Objects' labels.
        /// </summary>
        protected int[] objectLabels;

        /// <summary>
        /// Objects count.
        /// </summary>
        protected int objectsCount;

        private ObjectsOrder objectsOrder = ObjectsOrder.None;

        #endregion

        #region Get/Set

        /// <summary>
        /// Objects count.
        /// </summary>
        /// 
        /// <remarks><para>Number of objects (blobs) found by <see cref="ProcessImage(Bitmap)"/> method.
        /// </para></remarks>
        /// 
        public int ObjectsCount
        {
            get { return objectsCount; }
        }

        /// <summary>
        /// Objects' labels.
        /// </summary>
        /// 
        /// <remarks>The array of <b>width</b> * <b>height</b> size, which holds
        /// labels for all objects. Background is represented with <b>0</b> value,
        /// but objects are represented with labels starting from <b>1</b>.</remarks>
        /// 
        public int[] ObjectLabels
        {
            get { return objectLabels; }
        }

        /// <summary>
        /// Objects sort order.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies objects' sort order, which are provided
        /// by <see cref="GetObjectsRectangles"/>, <see cref="GetObjectsInformation"/>, etc.
        /// </para></remarks>
        /// 
        public ObjectsOrder ObjectsOrder
        {
            get { return objectsOrder; }
            set { objectsOrder = value; }
        }

        /// <summary>
        /// Specifies if blobs should be filtered.
        /// </summary>
        /// 
        /// <remarks><para>If the property is equal to <b>false</b>, then there is no any additional
        /// post processing after image was processed. If the property is set to <b>true</b>, then
        /// blobs filtering is done right after image processing routine. If <see cref="BlobsFilter"/>
        /// is set, then custom blobs' filtering is done, which is implemented by user. Otherwise
        /// blobs are filtered according to dimensions specified in <see cref="MinWidth"/>,
        /// <see cref="MinHeight"/>, <see cref="MaxWidth"/> and <see cref="MaxHeight"/> properties.</para>
        /// 
        /// <para>Default value is set to <see langword="false"/>.</para></remarks>
        /// 
        public bool FilterBlobs
        {
            get { return filterBlobs; }
            set { filterBlobs = value; }
        }

        /// <summary>
        /// Specifies if size filetering should be coupled or not.
        /// </summary>
        /// 
        /// <remarks><para>In uncoupled filtering mode, objects are filtered out in the case if
        /// their width is smaller than <see cref="MinWidth"/> or height is smaller than 
        /// <see cref="MinHeight"/>. But in coupled filtering mode, objects are filtered out in
        /// the case if their width is smaller than <see cref="MinWidth"/> <b>and</b> height is
        /// smaller than <see cref="MinHeight"/>. In both modes the idea with filtering by objects'
        /// maximum size is the same as filtering by objects' minimum size.</para>
        /// 
        /// <para>Default value is set to <see langword="false"/>, what means uncoupled filtering by size.</para>
        /// </remarks>
        /// 
        public bool CoupledSizeFiltering
        {
            get { return coupledSizeFiltering; }
            set { coupledSizeFiltering = value; }
        }

        /// <summary>
        /// Minimum allowed width of blob.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies minimum object's width acceptable by blob counting
        /// routine and has power only when <see cref="FilterBlobs"/> property is set to
        /// <see langword="true"/> and <see cref="BlobsFilter">custom blobs' filter</see> is
        /// set to <see langword="null"/>.</para>
        /// 
        /// <para>See documentation to <see cref="CoupledSizeFiltering"/> for additional information.</para>
        /// </remarks>
        /// 
        public int MinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }

        /// <summary>
        /// Minimum allowed height of blob.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies minimum object's height acceptable by blob counting
        /// routine and has power only when <see cref="FilterBlobs"/> property is set to
        /// <see langword="true"/> and <see cref="BlobsFilter">custom blobs' filter</see> is
        /// set to <see langword="null"/>.</para>
        /// 
        /// <para>See documentation to <see cref="CoupledSizeFiltering"/> for additional information.</para>
        /// </remarks>
        /// 
        public int MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }

        /// <summary>
        /// Maximum allowed width of blob.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies maximum object's width acceptable by blob counting
        /// routine and has power only when <see cref="FilterBlobs"/> property is set to
        /// <see langword="true"/> and <see cref="BlobsFilter">custom blobs' filter</see> is
        /// set to <see langword="null"/>.</para>
        /// 
        /// <para>See documentation to <see cref="CoupledSizeFiltering"/> for additional information.</para>
        /// </remarks>
        /// 
        public int MaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }

        /// <summary>
        /// Maximum allowed height of blob.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies maximum object's height acceptable by blob counting
        /// routine and has power only when <see cref="FilterBlobs"/> property is set to
        /// <see langword="true"/> and <see cref="BlobsFilter">custom blobs' filter</see> is
        /// set to <see langword="null"/>.</para>
        /// 
        /// <para>See documentation to <see cref="CoupledSizeFiltering"/> for additional information.</para>
        /// </remarks>
        /// 
        public int MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        /// <summary>
        /// Custom blobs' filter to use.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies custom blobs' filtering routine to use. It has
        /// effect only in the case if see cref="FilterBlobs"/> property is set to <see langword="true"/>.</para>
        /// 
        /// <para><note>When custom blobs' filtering routine is set, it has priority over default filtering done
        /// with <see cref="MinWidth"/>, <see cref="MinHeight"/>, <see cref="MaxWidth"/> and <see cref="MaxHeight"/>.</note></para>
        /// </remarks>
        /// 
        public IBlobsFilter BlobsFilter
        {
            get { return filter; }
            set { filter = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounterBase"/> class.
        /// </summary>
        /// 
        /// <remarks>Creates new instance of the <see cref="BlobCounterBase"/> class with
        /// an empty objects map. Before using methods, which provide information about blobs
        /// or extract them, the <see cref="ProcessImage(Bitmap)"/>,
        /// <see cref="ProcessImage(BitmapData)"/> or <see cref="ProcessImage(UnmanagedImage)"/>
        /// method should be called to collect objects map.</remarks>
        /// 
        protected BlobCounterBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounterBase"/> class.
        /// </summary>
        /// 
        /// <param name="image">Binary image to look for objects in.</param>
        /// 
        /// <remarks>Creates new instance of the <see cref="BlobCounterBase"/> class with
        /// initialized objects map built by calling <see cref="ProcessImage(Bitmap)"/> method.</remarks>
        /// 
        protected BlobCounterBase(Bitmap image)
        {
            ProcessImage(image);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounterBase"/> class.
        /// </summary>
        /// 
        /// <param name="imageData">Binary image data to look for objects in.</param>
        /// 
        /// <remarks>Creates new instance of the <see cref="BlobCounterBase"/> class with
        /// initialized objects map built by calling <see cref="ProcessImage(BitmapData)"/> method.</remarks>
        /// 
        protected BlobCounterBase(BitmapData imageData)
        {
            ProcessImage(imageData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounterBase"/> class.
        /// </summary>
        /// 
        /// <param name="image">Unmanaged binary image to look for objects in.</param>
        /// 
        /// <remarks>Creates new instance of the <see cref="BlobCounterBase"/> class with
        /// initialized objects map built by calling <see cref="ProcessImage(UnmanagedImage)"/> method.</remarks>
        /// 
        protected BlobCounterBase(UnmanagedImage image)
        {
            ProcessImage(image);
        }

        #endregion

        #region Public methods (process image)

        /// <summary>
        /// Build objects map.
        /// </summary>
        /// 
        /// <param name="image">Source binary image.</param>
        /// <remarks><para>Processes the image and builds objects map, which is used later to extracts blobs.</para></remarks>
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the source image.</exception>
        /// 
        public void ProcessImage(Bitmap image)
        {
            // lock source bitmap data
            BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, image.PixelFormat);

            try
            {
                // process image
                ProcessImage(imageData);
            }
            finally
            {
                // unlock source image
                image.UnlockBits(imageData);
            }
        }

        public void ProcessImage(Image<Gray, byte> image)
        {
            ProcessImage(image.ToBitmap());
        }

        /// <summary>
        /// Build objects map.
        /// </summary>
        /// 
        /// <param name="imageData">Source binary image data.</param>
        /// 
        /// <remarks><para>Processes the image and builds objects map, which is used later to extracts blobs.</para></remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the source image.</exception>
        /// 
        public void ProcessImage(BitmapData imageData)
        {
            // do actual objects map building
            ProcessImage(new UnmanagedImage(imageData));
        }

        /// <summary>
        /// Build object map from raw image data.
        /// </summary>
        /// 
        /// <param name="image">Source unmanaged binary image data.</param>
        /// 
        /// <remarks><para>Processes the image and builds objects map, which is used later to extracts blobs.</para></remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the source image.</exception>
        /// 
        public void ProcessImage(UnmanagedImage image)
        {
            imageWidth = image.Width;
            imageHeight = image.Height;

            // do actual objects map building
            BuildObjectsMap(image);

            // collect information about blobs
            CollectObjectsInfo(image);

            // filter blobs by size if required
            if (filterBlobs)
            {
                // labels remapping array
                var labelsMap = new int[objectsCount + 1];
                for (int i = 1; i <= objectsCount; i++)
                {
                    labelsMap[i] = i;
                }

                // check dimension of all objects and filter them
                int objectsToRemove = 0;

                if (filter == null)
                {
                    for (int i = objectsCount - 1; i >= 0; i--)
                    {
                        int blobWidth = blobs[i].Rectangle.Width;
                        int blobHeight = blobs[i].Rectangle.Height;

                        if (coupledSizeFiltering == false)
                        {
                            // uncoupled filtering
                            if (
                                (blobWidth < minWidth) || (blobHeight < minHeight) ||
                                (blobWidth > maxWidth) || (blobHeight > maxHeight))
                            {
                                labelsMap[i + 1] = 0;
                                objectsToRemove++;
                                blobs.RemoveAt(i);
                            }
                        }
                        else
                        {
                            // coupled filtering
                            if (
                                ((blobWidth < minWidth) && (blobHeight < minHeight)) ||
                                ((blobWidth > maxWidth) && (blobHeight > maxHeight)))
                            {
                                labelsMap[i + 1] = 0;
                                objectsToRemove++;
                                blobs.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = objectsCount - 1; i >= 0; i--)
                    {
                        if (!filter.Check(blobs[i]))
                        {
                            labelsMap[i + 1] = 0;
                            objectsToRemove++;
                            blobs.RemoveAt(i);
                        }
                    }
                }

                // update labels remapping array
                int label = 0;
                for (int i = 1; i <= objectsCount; i++)
                {
                    if (labelsMap[i] != 0)
                    {
                        label++;
                        // update remapping array
                        labelsMap[i] = label;
                    }
                }

                // repair object labels
                for (int i = 0, n = objectLabels.Length; i < n; i++)
                {
                    objectLabels[i] = labelsMap[objectLabels[i]];
                }

                objectsCount -= objectsToRemove;

                // repair IDs
                for (int i = 0, n = blobs.Count; i < n; i++)
                {
                    blobs[i].ID = i + 1;
                }
            }

            // do we need to sort the list?
            if (objectsOrder != ObjectsOrder.None)
            {
                blobs.Sort(new BlobsSorter(objectsOrder));
            }
        }

        #endregion

        #region Public get blobs information

        /// <summary>
        /// Get objects' rectangles.
        /// </summary>
        /// 
        /// <returns>Returns array of objects' rectangles.</returns>
        /// 
        /// <remarks>The method returns array of objects rectangles. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will
        /// build objects map.</remarks>
        /// 
        /// <exception cref="ApplicationException">No image was processed before, so objects' rectangles
        /// can not be collected.</exception>
        /// 
        public Rectangle[] GetObjectsRectangles()
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            var rects = new Rectangle[objectsCount];

            for (int i = 0; i < objectsCount; i++)
            {
                rects[i] = blobs[i].Rectangle;
            }

            return rects;
        }

        /// <summary>
        /// Get objects' information.
        /// </summary>
        /// 
        /// <returns>Returns array of partially initialized blobs (without <see cref="Blob.Image"/> property initialized).</returns>
        /// 
        /// <remarks><para>By the amount of provided information, the method is between <see cref="GetObjectsRectangles"/> and
        /// <see cref="GetObjects( BitmapData, bool )"/> methods. The method provides array of blobs without initialized their image.
        /// Blob's image may be extracted later using <see cref="ExtractBlobsImage( Bitmap, Blob, bool )"/>,
        /// <see cref="ExtractBlobsImage( BitmapData, Blob, bool )"/> or <see cref="ExtractBlobsImage( UnmanagedImage, Blob, bool )"/> method.
        /// </para></remarks>
        /// 
        /// <example>
        /// <code>
        /// // create blob counter and process image
        /// BlobCounter bc = new BlobCounter( sourceImage );
        /// // specify sort order
        /// bc.ObjectsOrder = ObjectsOrder.Size;
        /// // get objects' information (blobs without image)
        /// Blob[] blobs = bc.GetObjectInformation( );
        /// // process blobs
        /// foreach ( Blob blob in blobs )
        /// {
        ///     // check blob's properties
        ///     if ( blob.Rectangle.Width > 50 )
        ///     {
        ///         // the blob looks interesting, let's extract it
        ///         bc.ExtractBlobsImage( sourceImage, blob );
        ///     }
        /// }
        /// </code>
        /// </example>
        /// 
        /// <exception cref="ApplicationException">No image was processed before, so objects' information
        /// can not be collected.</exception>
        /// 
        public Blob[] GetObjectsInformation()
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            var blobsToReturn = new Blob[objectsCount];

            // create each blob
            for (int k = 0; k < objectsCount; k++)
            {
                if(blobsToReturn.Length-1 >= k)
                   blobsToReturn[k] = new Blob(blobs[k]);
            }

            return blobsToReturn;
        }

        /// <summary>
        /// Get blobs.
        /// </summary>
        /// 
        /// <param name="image">Source image to extract objects from.</param>
        /// 
        /// <returns>Returns array of blobs.</returns>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        ///
        /// <remarks><para>The method returns array of blobs. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so objects
        /// can not be collected.</exception>
        /// 
        public Blob[] GetObjects(Bitmap image, bool extractInOriginalSize)
        {
            Blob[] blobs = null;
            // lock source bitmap data
            BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, image.PixelFormat);

            try
            {
                // process image
                blobs = GetObjects(imageData, extractInOriginalSize);
            }
            finally
            {
                // unlock source images
                image.UnlockBits(imageData);
            }
            return blobs;
        }

        /// <summary>
        /// Get blobs.
        /// </summary>
        /// 
        /// <param name="imageData">Source image data to extract objects from.</param>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        ///
        /// <returns>Returns array of blobs.</returns>
        /// 
        /// <remarks><para>The method returns array of blobs. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so objects
        /// can not be collected.</exception>
        /// 
        public Blob[] GetObjects(BitmapData imageData, bool extractInOriginalSize)
        {
            return GetObjects(new UnmanagedImage(imageData), extractInOriginalSize);
        }

        /// <summary>
        /// Get blobs.
        /// </summary>
        /// 
        /// <param name="image">Source unmanaged image to extract objects from.</param>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        /// 
        /// <returns>Returns array of blobs.</returns>
        /// 
        /// <remarks><para>The method returns array of blobs. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        ///
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so objects
        /// can not be collected.</exception>
        /// 
        public Blob[] GetObjects(UnmanagedImage image, bool extractInOriginalSize)
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            //if (
            //    ( image.PixelFormat != PixelFormat.Format24bppRgb ) &&
            //    ( image.PixelFormat != PixelFormat.Format8bppIndexed ) &&
            //    ( image.PixelFormat != PixelFormat.Format32bppArgb ) &&
            //    ( image.PixelFormat != PixelFormat.Format32bppPArgb )
            //    )
            //    throw new UnsupportedImageFormatException( "Unsupported pixel format of the provided image." );

            // image size
            int width = image.Width;
            int height = image.Height;
            int srcStride = image.Stride;
            int pixelSize = Image.GetPixelFormatSize(image.PixelFormat)/8;

            var objects = new Blob[objectsCount];

            // create each image
            for (int k = 0; k < objectsCount; k++)
            {
                int objectWidth = blobs[k].Rectangle.Width;
                int objectHeight = blobs[k].Rectangle.Height;

                int blobImageWidth = (extractInOriginalSize) ? width : objectWidth;
                int blobImageHeight = (extractInOriginalSize) ? height : objectHeight;

                int xmin = blobs[k].Rectangle.X;
                int xmax = xmin + objectWidth - 1;
                int ymin = blobs[k].Rectangle.Y;
                int ymax = ymin + objectHeight - 1;

                int label = blobs[k].ID;

                // create new image
                Bitmap dstImg = (image.PixelFormat == PixelFormat.Format8bppIndexed)
                                    ? AForge.Imaging.Image.CreateGrayscaleImage(blobImageWidth, blobImageHeight)
                                    : new Bitmap(blobImageWidth, blobImageHeight, image.PixelFormat);

                // lock destination bitmap data
                BitmapData dstData = dstImg.LockBits(
                    new Rectangle(0, 0, blobImageWidth, blobImageHeight),
                    ImageLockMode.ReadWrite, image.PixelFormat);

                // copy image
                unsafe
                {
                    byte* src = (byte*) image.ImageData.ToPointer() + ymin*srcStride + xmin*pixelSize;
                    var dst = (byte*) dstData.Scan0.ToPointer();
                    int p = ymin*width + xmin;

                    if (extractInOriginalSize)
                    {
                        // allign destination pointer also
                        dst += ymin*dstData.Stride + xmin*pixelSize;
                    }

                    int srcOffset = srcStride - objectWidth*pixelSize;
                    int dstOffset = dstData.Stride - objectWidth*pixelSize;
                    int labelsOffset = width - objectWidth;

                    // for each line
                    for (int y = ymin; y <= ymax; y++)
                    {
                        // copy each pixel
                        for (int x = xmin; x <= xmax; x++, p++, dst += pixelSize, src += pixelSize)
                        {
                            if (objectLabels[p] == label)
                            {
                                // copy pixel
                                *dst = *src;

                                if (pixelSize > 1)
                                {
                                    dst[1] = src[1];
                                    dst[2] = src[2];

                                    if (pixelSize > 3)
                                    {
                                        dst[3] = src[3];
                                    }
                                }
                            }
                        }
                        src += srcOffset;
                        dst += dstOffset;
                        p += labelsOffset;
                    }
                }
                // unlock destination image
                dstImg.UnlockBits(dstData);

                objects[k] = new Blob(blobs[k]);
                objects[k].Image = dstImg;
                objects[k].OriginalSize = extractInOriginalSize;
            }

            return objects;
        }

        #endregion

        #region Public blob image

        /// <summary>
        /// Extract blob's image.
        /// </summary>
        /// 
        /// <param name="image">Source image to extract blob's image from.</param>
        /// <param name="blob">Blob which is required to be extracted.</param>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        ///
        /// <remarks><para>The method is used to extract image of partially initialized blob, which
        /// was provided by <see cref="GetObjectsInformation"/> method. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public void ExtractBlobsImage(Bitmap image, Blob blob, bool extractInOriginalSize)
        {
            // lock source bitmap data
            BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, image.PixelFormat);

            try
            {
                // process image
                ExtractBlobsImage(imageData, blob, extractInOriginalSize);
            }
            finally
            {
                // unlock source images
                image.UnlockBits(imageData);
            }
        }

        /// <summary>
        /// Extract blob's image.
        /// </summary>
        /// 
        /// <param name="imageData">Source image data to extract blob's image from.</param>
        /// <param name="blob">Blob which is required to be extracted.</param>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        ///
        /// <remarks><para>The method is used to extract image of partially initialized blob, which
        /// was provided by <see cref="GetObjectsInformation"/> method. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public void ExtractBlobsImage(BitmapData imageData, Blob blob, bool extractInOriginalSize)
        {
            ExtractBlobsImage(new UnmanagedImage(imageData), blob, extractInOriginalSize);
        }

        /// <summary>
        /// Extract blob's image.
        /// </summary>
        /// 
        /// <param name="image">Source unmanaged image to extract blob's image from.</param>
        /// <param name="blob">Blob which is required to be extracted.</param>
        /// <param name="extractInOriginalSize">Specifies size of blobs' image to extract.
        /// If set to <see langword="true"/> each blobs' image will have the same size as
        /// the specified image. If set to <see langword="false"/> each blobs' image will
        /// have the size of its blob.</param>
        ///
        /// <remarks><para>The method is used to extract image of partially initialized blob, which
        /// was provided by <see cref="GetObjectsInformation"/> method. Before calling the
        /// method, the <see cref="ProcessImage(Bitmap)"/>, <see cref="ProcessImage(BitmapData)"/>
        /// or <see cref="ProcessImage(UnmanagedImage)"/> method should be called, which will build
        /// objects map.</para>
        /// 
        /// <para>The method supports 24 bpp color and 8 bpp indexed grayscale images.</para>
        /// </remarks>
        /// 
        /// <exception cref="UnsupportedImageFormatException">Unsupported pixel format of the provided image.</exception>
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public void ExtractBlobsImage(UnmanagedImage image, Blob blob, bool extractInOriginalSize)
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            //if (
            //    ( image.PixelFormat != PixelFormat.Format24bppRgb ) &&
            //    ( image.PixelFormat != PixelFormat.Format8bppIndexed ) &&
            //    ( image.PixelFormat != PixelFormat.Format32bppArgb ) &&
            //    ( image.PixelFormat != PixelFormat.Format32bppPArgb )
            //    )
            //    throw new UnsupportedImageFormatException( "Unsupported pixel format of the provided image." );

            // image size
            int width = image.Width;
            int height = image.Height;
            int srcStride = image.Stride;
            int pixelSize = Image.GetPixelFormatSize(image.PixelFormat)/8;

            int objectWidth = blob.Rectangle.Width;
            int objectHeight = blob.Rectangle.Height;

            int blobImageWidth = (extractInOriginalSize) ? width : objectWidth;
            int blobImageHeight = (extractInOriginalSize) ? height : objectHeight;

            int xmin = blob.Rectangle.Left;
            int xmax = xmin + objectWidth - 1;
            int ymin = blob.Rectangle.Top;
            int ymax = ymin + objectHeight - 1;

            int label = blob.ID;

            // create new image
            blob.Image = (image.PixelFormat == PixelFormat.Format8bppIndexed)
                             ? AForge.Imaging.Image.CreateGrayscaleImage(blobImageWidth, blobImageHeight)
                             : new Bitmap(blobImageWidth, blobImageHeight, image.PixelFormat);
            blob.OriginalSize = extractInOriginalSize;

            // lock destination bitmap data
            BitmapData dstData = blob.Image.LockBits(
                new Rectangle(0, 0, blobImageWidth, blobImageHeight),
                ImageLockMode.ReadWrite, image.PixelFormat);

            // copy image
            unsafe
            {
                byte* src = (byte*) image.ImageData.ToPointer() + ymin*srcStride + xmin*pixelSize;
                var dst = (byte*) dstData.Scan0.ToPointer();
                int p = ymin*width + xmin;

                if (extractInOriginalSize)
                {
                    // allign destination pointer also
                    dst += ymin*dstData.Stride + xmin*pixelSize;
                }

                int srcOffset = srcStride - objectWidth*pixelSize;
                int dstOffset = dstData.Stride - objectWidth*pixelSize;
                int labelsOffset = width - objectWidth;

                // for each line
                for (int y = ymin; y <= ymax; y++)
                {
                    // copy each pixel
                    for (int x = xmin; x <= xmax; x++, p++, dst += pixelSize, src += pixelSize)
                    {
                        if (objectLabels[p] == label)
                        {
                            // copy pixel
                            *dst = *src;

                            if (pixelSize > 1)
                            {
                                dst[1] = src[1];
                                dst[2] = src[2];

                                if (pixelSize > 3)
                                {
                                    dst[3] = src[3];
                                }
                            }
                        }
                    }
                    src += srcOffset;
                    dst += dstOffset;
                    p += labelsOffset;
                }
            }
            // unlock destination image
            blob.Image.UnlockBits(dstData);
        }

        #endregion

        #region Get edges methods

        /// <summary>
        /// Get list of points on the left and right edges of the blob.
        /// </summary>
        /// 
        /// <param name="blob">Blob to collect edge points for.</param>
        /// <param name="leftEdge">List of points on the left edge of the blob.</param>
        /// <param name="rightEdge">List of points on the right edge of the blob.</param>
        /// 
        /// <remarks><para>The method scans each line of the blob and finds the most left and the
        /// most right points for it adding them to appropriate lists. The method may be very
        /// useful in conjunction with different routines from <see cref="AForge.Math.Geometry"/>,
        /// which allow finding convex hull or quadrilateral's corners.</para>
        /// 
        /// <para><note>Both lists of points are sorted by Y coordinate - points with smaller Y
        /// value go first.</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public void GetBlobsLeftAndRightEdges(Blob blob, out List<IntPoint> leftEdge, out List<IntPoint> rightEdge)
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            leftEdge = new List<IntPoint>();
            rightEdge = new List<IntPoint>();

            int xmin = blob.Rectangle.Left;
            int xmax = xmin + blob.Rectangle.Width - 1;
            int ymin = blob.Rectangle.Top;
            int ymax = ymin + blob.Rectangle.Height - 1;

            int label = blob.ID;

            // for each line
            for (int y = ymin; y <= ymax; y++)
            {
                // scan from left to right
                int p = y*imageWidth + xmin;
                for (int x = xmin; x <= xmax; x++, p++)
                {
                    if (objectLabels[p] == label)
                    {
                        leftEdge.Add(new IntPoint(x, y));
                        break;
                    }
                }

                // scan from right to left
                p = y*imageWidth + xmax;
                for (int x = xmax; x >= xmin; x--, p--)
                {
                    if (objectLabels[p] == label)
                    {
                        rightEdge.Add(new IntPoint(x, y));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get list of points on the top and bottom edges of the blob.
        /// </summary>
        /// 
        /// <param name="blob">Blob to collect edge points for.</param>
        /// <param name="topEdge">List of points on the top edge of the blob.</param>
        /// <param name="bottomEdge">List of points on the bottom edge of the blob.</param>
        /// 
        /// <remarks><para>The method scans each column of the blob and finds the most top and the
        /// most bottom points for it adding them to appropriate lists. The method may be very
        /// useful in conjunction with different routines from <see cref="AForge.Math.Geometry"/>,
        /// which allow finding convex hull or quadrilateral's corners.</para>
        /// 
        /// <para><note>Both lists of points are sorted by X coordinate - points with smaller X
        /// value go first.</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public void GetBlobsTopAndBottomEdges(Blob blob, out List<IntPoint> topEdge, out List<IntPoint> bottomEdge)
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            topEdge = new List<IntPoint>();
            bottomEdge = new List<IntPoint>();

            int xmin = blob.Rectangle.Left;
            int xmax = xmin + blob.Rectangle.Width - 1;
            int ymin = blob.Rectangle.Top;
            int ymax = ymin + blob.Rectangle.Height - 1;

            int label = blob.ID;

            // for each column
            for (int x = xmin; x <= xmax; x++)
            {
                // scan from top to bottom
                int p = ymin*imageWidth + x;
                for (int y = ymin; y <= ymax; y++, p += imageWidth)
                {
                    if (objectLabels[p] == label)
                    {
                        topEdge.Add(new IntPoint(x, y));
                        break;
                    }
                }

                // scan from bottom to top
                p = ymax*imageWidth + x;
                for (int y = ymax; y >= ymin; y--, p -= imageWidth)
                {
                    if (objectLabels[p] == label)
                    {
                        bottomEdge.Add(new IntPoint(x, y));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get list of object's edge points.
        /// </summary>
        /// 
        /// <param name="blob">Blob to collect edge points for.</param>
        /// 
        /// <returns>Returns unsorted list of blob's edge points.</returns>
        /// 
        /// <remarks><para>The method scans each row and column of the blob and finds the
        /// most top/bottom/left/right points. The method returns similar result as if results of
        /// both <see cref="GetBlobsLeftAndRightEdges"/> and <see cref="GetBlobsTopAndBottomEdges"/>
        /// methods were combined, but each edge point occurs only once in the list.</para>
        /// 
        /// <para><note>Edge points in the returned list are not ordered. This makes the list unusable
        /// for visualization with methods, which draw polygon or poly-line. But the returned list
        /// can be used with such algorithms, like convex hull search, shape analyzer, etc.</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ApplicationException">No image was processed before, so blob
        /// can not be extracted.</exception>
        /// 
        public List<IntPoint> GetBlobsEdgePoints(Blob blob)
        {
            // check if objects map was collected
            if (objectLabels == null)
                throw new ApplicationException("Image should be processed before to collect objects map.");

            var edgePoints = new List<IntPoint>();

            int xmin = blob.Rectangle.Left;
            int xmax = xmin + blob.Rectangle.Width - 1;
            int ymin = blob.Rectangle.Top;
            int ymax = ymin + blob.Rectangle.Height - 1;

            int label = blob.ID;

            // array of already processed points on left/right edges
            // (index in these arrays represent Y coordinate, but value - X coordinate)
            var leftProcessedPoints = new int[blob.Rectangle.Height];
            var rightProcessedPoints = new int[blob.Rectangle.Height];

            // for each line
            for (int y = ymin; y <= ymax; y++)
            {
                // scan from left to right
                int p = y*imageWidth + xmin;
                for (int x = xmin; x <= xmax; x++, p++)
                {
                    if (objectLabels[p] == label)
                    {
                        edgePoints.Add(new IntPoint(x, y));
                        leftProcessedPoints[y - ymin] = x;
                        break;
                    }
                }

                // scan from right to left
                p = y*imageWidth + xmax;
                for (int x = xmax; x >= xmin; x--, p--)
                {
                    if (objectLabels[p] == label)
                    {
                        // avoid adding the point we already have
                        if (leftProcessedPoints[y - ymin] != x)
                        {
                            edgePoints.Add(new IntPoint(x, y));
                        }
                        rightProcessedPoints[y - ymin] = x;
                        break;
                    }
                }
            }

            // for each column
            for (int x = xmin; x <= xmax; x++)
            {
                // scan from top to bottom
                int p = ymin*imageWidth + x;
                for (int y = ymin, y0 = 0; y <= ymax; y++, y0++, p += imageWidth)
                {
                    if (objectLabels[p] == label)
                    {
                        // avoid adding the point we already have
                        if ((leftProcessedPoints[y0] != x) &&
                            (rightProcessedPoints[y0] != x))
                        {
                            edgePoints.Add(new IntPoint(x, y));
                        }
                        break;
                    }
                }

                // scan from bottom to top
                p = ymax*imageWidth + x;
                for (int y = ymax, y0 = ymax - ymin; y >= ymin; y--, y0--, p -= imageWidth)
                {
                    if (objectLabels[p] == label)
                    {
                        // avoid adding the point we already have
                        if ((leftProcessedPoints[y0] != x) &&
                            (rightProcessedPoints[y0] != x))
                        {
                            edgePoints.Add(new IntPoint(x, y));
                        }
                        break;
                    }
                }
            }

            return edgePoints;
        }

        #endregion

        /// <summary>
        /// Actual objects map building.
        /// </summary>
        /// 
        /// <param name="image">Unmanaged image to process.</param>
        /// 
        /// <remarks><note>By the time this method is called bitmap's pixel format is not
        /// yet checked, so this should be done by the class inheriting from the base class.
        /// <see cref="imageWidth"/> and <see cref="imageHeight"/> members are initialized
        /// before the method is called, so these members may be used safely.</note></remarks>
        /// 
        protected abstract void BuildObjectsMap(UnmanagedImage image);

        #region Private Methods - Collecting objects' rectangles

        // Collect objects' rectangles
        private unsafe void CollectObjectsInfo(UnmanagedImage image)
        {
            #region Local variables 

            int i = 0, label;
            // create object coordinates arrays
            var x1 = new int[objectsCount + 1];
            var y1 = new int[objectsCount + 1];
            var x2 = new int[objectsCount + 1];
            var y2 = new int[objectsCount + 1];

            var area = new double[objectsCount + 1];
            var xc = new long[objectsCount + 1];
            var yc = new long[objectsCount + 1];

            var meanR = new long[objectsCount + 1];
            var meanG = new long[objectsCount + 1];
            var meanB = new long[objectsCount + 1];

            var stdDevR = new long[objectsCount + 1];
            var stdDevG = new long[objectsCount + 1];
            var stdDevB = new long[objectsCount + 1];

            #endregion

            for (int j = 1; j <= objectsCount; j++)
            {
                x1[j] = imageWidth;
                y1[j] = imageHeight;
            }

            var src = (byte*) image.ImageData.ToPointer();

            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                #region Gray images

                int offset = image.Stride - imageWidth;
                byte g; // pixel's grey value

                // walk through labels array
                for (int y = 0; y < imageHeight; y++)
                {
                    for (int x = 0; x < imageWidth; x++, i++, src++)
                    {
                        // get Instance label
                        label = objectLabels[i];

                        // skip unlabeled pixels
                        if (label == 0)
                            continue;

                            // check and update all coordinates
                            // M.T Now checking array size to avoid out of bounds exception
                            if (x1.Length-1 >= label && x < x1[label])
                            {
                                x1[label] = x;
                            }
                            if (x2.Length-1 >= label && x > x2[label])
                            {
                                x2[label] = x;
                            }
                            if (y1.Length - 1 >= label && y < y1[label])
                            {
                                y1[label] = y;
                            }
                            if (y2.Length - 1 >= label && y > y2[label])
                            {
                                y2[label] = y;
                            }


                            if(area.Length -1 >= label)
                               area[label]++;

                            if(xc.Length-1 >= label)
                               xc[label] += x;

                            if(yc.Length-1 >= label)
                               yc[label] += y;

                            g = *src;

                            // Mean
                            if (meanG.Length-1 >= label)
                                meanG[label] += g;

                            // Standard Deviation
                            if (stdDevG.Length - 1 >= label)
                                stdDevG[label] += g*g;
                    }

                    src += offset;
                }

                for (int j = 1; j <= objectsCount; j++)
                {
                    if (meanR.Length-1 >= j)
                        meanR[j] = meanB[j] = meanG[j];
                    if (stdDevR.Length-1 >= j)
                        stdDevR[j] = stdDevB[j] = meanG[j];
                }
                #endregion
            }
            else
            {
                #region color images

                int pixelSize = Image.GetPixelFormatSize(image.PixelFormat)/8;
                int offset = image.Stride - imageWidth*pixelSize;
                byte r, g, b; // RGB value

                // walk through labels array
                for (int y = 0; y < imageHeight; y++)
                {
                    for (int x = 0; x < imageWidth; x++, i++, src += pixelSize)
                    {
                        // get Instance label
                        label = objectLabels[i];

                        // skip unlabeled pixels
                        if (label == 0)
                            continue;

                        // check and update all coordinates

                        if (x < x1[label])
                        {
                            x1[label] = x;
                        }
                        if (x > x2[label])
                        {
                            x2[label] = x;
                        }
                        if (y < y1[label])
                        {
                            y1[label] = y;
                        }
                        if (y > y2[label])
                        {
                            y2[label] = y;
                        }

                        area[label]++;
                        xc[label] += x;
                        yc[label] += y;

                        r = src[RGB.R];
                        g = src[RGB.G];
                        b = src[RGB.B];

                        meanR[label] += r;
                        meanG[label] += g;
                        meanB[label] += b;

                        stdDevR[label] += r*r;
                        stdDevG[label] += g*g;
                        stdDevB[label] += b*b;
                    }

                    src += offset;
                }

                #endregion
            }

            // create blobs
            blobs.Clear();

            for (int j = 1; j <= objectsCount; j++)
            {
                double blobArea = area[j];

                var blob = new Blob(j, new Rectangle(x1[j], y1[j], x2[j] - x1[j] + 1, y2[j] - y1[j] + 1));
                blob.Area = blobArea;
                blob.Fullness = blobArea/((x2[j] - x1[j] + 1)*(y2[j] - y1[j] + 1));
                blob.CenterOfGravity = new GTPoint((xc[j]/blobArea), (yc[j]/blobArea));
                blob.ColorMean = Color.FromArgb((byte)(meanR[j]/blobArea), (byte)(meanG[j]/blobArea),(byte)(meanB[j]/blobArea));
                blob.ColorStdDev = Color.FromArgb(
                    (byte) (Math.Sqrt(stdDevR[j]/blobArea - blob.ColorMean.R*blob.ColorMean.R)),
                    (byte) (Math.Sqrt(stdDevG[j]/blobArea - blob.ColorMean.G*blob.ColorMean.G)),
                    (byte) (Math.Sqrt(stdDevB[j]/blobArea - blob.ColorMean.B*blob.ColorMean.B)));

                blobs.Add(blob);
            }
        }

        // Rectangles' and blobs' sorter
        private class BlobsSorter : IComparer<Blob>
        {
            private readonly ObjectsOrder order;

            public BlobsSorter(ObjectsOrder order)
            {
                this.order = order;
            }

            #region IComparer<Blob> Members

            public int Compare(Blob a, Blob b)
            {
                Rectangle aRect = a.Rectangle;
                Rectangle bRect = b.Rectangle;

                switch (order)
                {
                    case ObjectsOrder.Size: // sort by size

                        // the order is changed to descending
                        return (bRect.Width*bRect.Height - aRect.Width*aRect.Height);

                    case ObjectsOrder.Area: // sort by area
                        return (int) b.Area - (int) a.Area;

                    case ObjectsOrder.YX: // YX order

                        return ((aRect.Y*100000 + aRect.X) - (bRect.Y*100000 + bRect.X));

                    case ObjectsOrder.XY: // XY order

                        return ((aRect.X*100000 + aRect.Y) - (bRect.X*100000 + bRect.Y));
                }
                return 0;
            }

            #endregion
        }

        #endregion
    }
}