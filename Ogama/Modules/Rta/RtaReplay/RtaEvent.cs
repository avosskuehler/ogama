using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    /// <summary>
    /// 
    /// </summary>
    [System.Xml.Serialization.XmlRoot("RtaEvent")]
    public class RtaEvent
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement("fkRtaCategory")]
        public RtaCategory fkRtaCategory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement("Xstart")]
        public double Xstart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement("Xend")]
        public double Xend { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement("start")]
        public String startTimestamp;

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement("end")]
        public String endTimestamp;
    }
}
