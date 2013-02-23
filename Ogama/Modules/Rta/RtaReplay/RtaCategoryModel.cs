using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ogama.Modules.Rta.RtaReplay
{
    [System.Xml.Serialization.XmlRoot("RtaCategoryModel")]
    public class RtaCategoryModel
    {
        [System.Xml.Serialization.XmlElement("RtaCategory")]
        public List<RtaCategory> categories { get; set; }

        public RtaCategoryModel()
        {
            init();
        }

        protected void init()
        {
            if (this.categories == null)
            {
                this.categories = new List<RtaCategory>();
            }
        }

        public void Add(RtaCategory newCategory, RtaCategory parent)
        {
            parent.getChildren().Add(newCategory);
            
        }

        public void Add(RtaCategory category)
        {
            this.categories.Add(category);
            category.parent = null;
        }

        public int CountChildren()
        {
            return this.categories.Count;
        }

        public void Remove(RtaCategory category)
        {
            if (category == null)
            {
                return;
            }

            if (category.parent != null)
            {
                category.parent.Remove(category);
            }
            else
            {
                this.categories.Remove(category);
            }
            
        }

        public string WriteToXml()
        {
            System.Xml.Serialization.XmlSerializer serializer = getSerializer();
            System.IO.StringWriter sWriter = new System.IO.StringWriter();

            serializer.Serialize(sWriter, this);
            
            return sWriter.ToString();
        }


        public void WriteToXmlFile(string filename)
        {

            System.Xml.Serialization.XmlSerializer serializer = getSerializer();
            
            System.IO.FileStream fileStream = new System.IO.FileStream(filename,
                System.IO.FileMode.Create);

            serializer.Serialize(fileStream, this);

            fileStream.Close();
        }

        public void ReadFromXmlFile(string filename)
        {
            System.Xml.Serialization.XmlSerializer serializer = getSerializer();

            System.IO.FileStream fileStream = null;
            try
            {
                fileStream = new System.IO.FileStream(filename,
                 System.IO.FileMode.Open);
            }
            catch (System.IO.FileNotFoundException e)
            {
                return;
            }
            
            RtaCategoryModel result = null;

            try
            {
                result = (RtaCategoryModel)serializer.Deserialize(fileStream);
                this.categories = result.categories;
            }
            catch (System.InvalidOperationException ex)
            {
                
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            
        }

        private static System.Xml.Serialization.XmlSerializer getSerializer()
        {
            System.Xml.Serialization.XmlSerializer
                serializer =
                    new
                    System.Xml.Serialization.XmlSerializer(
                    typeof(RtaCategoryModel));

            return serializer;

        }


        public void visit(IRtaCategoryVisitor visitor)
        {

            List<RtaCategory> categories = new List<RtaCategory>();

            for (int i = 0; i < this.categories.Count(); i++)
            {
                categories.Add(this.categories.ElementAt(i));
            }

            Stack<RtaCategory> parentNodes = new Stack<RtaCategory>();

            for (int i = 0; i < categories.Count; i++)
            {
                RtaCategory currentItem = categories.ElementAt(i);

                visitor.onVisit(currentItem);

                List<RtaCategory> subList = currentItem.getChildren();

                categories.InsertRange(i + 1, subList);
            }
        }
    }
}
