using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    [System.Xml.Serialization.XmlRoot("RtaCategory")]
    public class RtaCategory
    {
        [System.Xml.Serialization.XmlElement("name")]
        public string name { get; set; }
        [System.Xml.Serialization.XmlElement("description")]
        public string description { get; set; }
        [System.Xml.Serialization.XmlElement("parent")]
        public RtaCategory parent { get; set; }
        [System.Xml.Serialization.XmlElement("show")]
        public Boolean show { get; set; }

        public List<RtaCategory> children = new List<RtaCategory>();
        
        public RtaCategory()
        {

        }

        public void SetActive(bool active)
        {
            this.show = active;
            for (int i = 0; i < children.Count; i++)
            {
                RtaCategory child = children.ElementAt(i);
                child.SetActive(active);
            }
        }

        public RtaCategory(string name)
        {
            this.name = name;
        }
        public List<RtaCategory> getChildren()
        {
            return this.children;
        }

        public void Add(RtaCategory category)
        {
            this.children.Add(category);
        }

        public void Remove(RtaCategory category)
        {
            this.children.Remove(category);
        }
    }

}
