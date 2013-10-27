using System;

using Emgu.CV;
using Emgu.CV.Structure;

namespace GTHardware.Cameras
{
    public class ImageEventArgs : EventArgs
    {
        private Image<Gray, byte> image;

        public ImageEventArgs(Image<Gray, byte> img)
        {
            image = img;
        }

        public Image<Gray, byte> Image
        {
            get { return image; }
			set { image = value; }
        }
    }
}
