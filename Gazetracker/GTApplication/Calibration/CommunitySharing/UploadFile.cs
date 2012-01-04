using System.IO;

namespace GazeTracker.Calibration.CommunitySharing
{
    // http://aspnetupload.com
    // Copyright © 2009 Krystalware, Inc.
    //
    // This work is licensed under a Creative Commons Attribution-Share Alike 3.0 United States License
    // http://creativecommons.org/licenses/by-sa/3.0/us/

    public class UploadFile
    {
        public UploadFile(Stream data, string fieldName, string fileName, string contentType)
        {
            Data = data;
            FieldName = fieldName;
            FileName = fileName;
            ContentType = contentType;
        }

        public UploadFile(string fileName, string fieldName, string contentType)
            : this(File.OpenRead(fileName), fieldName, Path.GetFileName(fileName), contentType)
        {
        }

        public UploadFile(string fileName)
            : this(fileName, null, "application/octet-stream")
        {
        }

        public Stream Data { get; set; }

        public string FieldName { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }
    }
}