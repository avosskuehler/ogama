using System.IO;

namespace GazeTracker.Calibration.CommunitySharing
{
    // http://aspnetupload.com
    // Copyright © 2009 Krystalware, Inc.
    //
    // This work is licensed under a Creative Commons Attribution-Share Alike 3.0 United States License
    // http://creativecommons.org/licenses/by-sa/3.0/us/

    public class StreamMimePart : MimePart
    {
        private Stream _data;

        public override Stream Data
        {
            get { return _data; }
        }

        public void SetStream(Stream stream)
        {
            _data = stream;
        }
    }
}