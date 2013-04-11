using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace GazeTracker.Calibration.CommunitySharing
{
    // http://aspnetupload.com
    // Copyright © 2009 Krystalware, Inc.
    //
    // This work is licensed under a Creative Commons Attribution-Share Alike 3.0 United States License
    // http://creativecommons.org/licenses/by-sa/3.0/us/

    public abstract class MimePart
    {
        private readonly NameValueCollection _headers = new NameValueCollection();
        private byte[] _header;

        public NameValueCollection Headers
        {
            get { return _headers; }
        }

        public byte[] Header
        {
            get { return _header; }
        }

        public abstract Stream Data { get; }

        public long GenerateHeaderFooterData(string boundary)
        {
            var sb = new StringBuilder();

            sb.Append("--");
            sb.Append(boundary);
            sb.AppendLine();
            foreach (string key in _headers.AllKeys)
            {
                sb.Append(key);
                sb.Append(": ");
                sb.AppendLine(_headers[key]);
            }
            sb.AppendLine();

            _header = Encoding.UTF8.GetBytes(sb.ToString());

            return _header.Length + Data.Length + 2;
        }
    }
}