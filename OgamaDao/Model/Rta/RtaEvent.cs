using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaDao.Model.Rta
{
    /// <summary>
    /// 
    /// </summary>
    //[System.Xml.Serialization.XmlRoot("RtaEvent")]
    public class RtaEvent : OgamaDao.Model.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        //[System.Xml.Serialization.XmlElement("fkRtaCategory")]

        public virtual RtaCategory fkRtaCategory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        //[System.Xml.Serialization.XmlElement("Xstart")]
        public virtual double Xstart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[System.Xml.Serialization.XmlElement("Xend")]
        public virtual double Xend { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[System.Xml.Serialization.XmlElement("start")]
        public virtual double startTimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[System.Xml.Serialization.XmlElement("end")]
        public virtual double endTimestamp { get; set; }
    }
}
