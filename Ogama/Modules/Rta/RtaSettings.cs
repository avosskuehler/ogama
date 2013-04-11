using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class RtaSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public RtaSettings()
        {
            init();
        }

        private void init()
        {
            this.MonitorIndex = 0;
            this.Framerate = 10;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public long version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MonitorIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Framerate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TempFilename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VideoCompressorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AudioInputDeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AudioCompressorName { get; set; }

        public Boolean isValid()
        {
            if (!exists(this.AudioCompressorName))
            {
                return false;
            }

            if (!exists(this.AudioCompressorName))
            {
                return false;
            }

            if (!exists(this.AudioInputDeviceName))
            {
                return false;
            }

            if (!exists(this.Filename))
            {
                return false;
            }

            if (!exists(this.TempFilename))
            {
                return false;
            }

            if (!exists(this.VideoCompressorName))
            {
                return false;
            }

            if (this.Framerate <= 0)
            {
                return false;
            }

            if (this.MonitorIndex < 0)
            {
                return false;
            }

            return true;
        }

        private Boolean exists(string s)
        {
            if (s == null || s.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
