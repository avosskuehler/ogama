using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgamaDao.Model.Rta
{
    /// <summary>
    /// 
    /// Settings of the RTA module
    /// </summary>
    public class RtaSettings : OgamaDao.Model.BaseModel
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
        public virtual int MonitorIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int Framerate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string TempFilename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string VideoCompressorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string AudioInputDeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string AudioCompressorName { get; set; }

        
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
