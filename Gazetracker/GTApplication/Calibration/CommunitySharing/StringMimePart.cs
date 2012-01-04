﻿using System.IO;
using System.Text;

namespace GazeTracker.Calibration.CommunitySharing
{
    // http://aspnetupload.com
    // Copyright © 2009 Krystalware, Inc.
    //
    // This work is licensed under a Creative Commons Attribution-Share Alike 3.0 United States License
    // http://creativecommons.org/licenses/by-sa/3.0/us/

    public class StringMimePart : MimePart
    {
        private Stream _data;

        public string StringData
        {
            set { _data = new MemoryStream(Encoding.UTF8.GetBytes(value)); }
        }

        public override Stream Data
        {
            get { return _data; }
        }
    }
}