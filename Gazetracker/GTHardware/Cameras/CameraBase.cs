using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using GTHardware.Cameras;

namespace GTHardware
{
    public abstract partial class CameraBase
    {
        public long FrameCounter = 0;
        public string Name;

        // Abstract methods that should to be implemented in each device

        public abstract int Width { get;}
        public abstract int Height { get; }
        public abstract int DefaultWidth { get; }
        public abstract int DefaultHeight { get; }
        public abstract int FPS { get; }

        public abstract bool IsSupportingROI { get; set; }
        public abstract bool IsROISet { get; }
        public abstract bool IsSettingROI { get; }

        public event EventHandler<ImageEventArgs> OnImage;

        public abstract bool Initialize();
        public abstract bool Start();
        public abstract bool Stop();
        public abstract Rectangle SetROI(Rectangle newRoi);
        public abstract Rectangle GetROI();
        public abstract void ClearROI();
        public abstract void Cleanup();


        protected virtual void OnRaiseCustomEvent(ImageEventArgs imageEventArgs)
        {
            // Each device calls this method when a new frame has been captured

            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<ImageEventArgs> handler = OnImage;

            FrameCounter++;

            // Event will be null if there are no subscribers
            if (OnImage != null)
                OnImage(this, imageEventArgs);
        }

    }
}
