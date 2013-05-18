using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Ogama.Modules.Rta.Model;
namespace Ogama.Modules.Rta.RtaReplay
{
    [System.Xml.Serialization.XmlRoot("RtaCategoryModel")]
    public class RtaCategoryModel
    {
        [System.Xml.Serialization.XmlElement("RtaCategory")]
        public List<RtaCategory> categories { get; set; }
        
        [System.Xml.Serialization.XmlElement("RtaEvent")]
        public List<RtaEvent> events { get; set; }

        RtaCategoryModelListener listener { get; set;}



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
            if (this.events == null)
            {
                this.events = new List<RtaEvent>();
            }
        }

        public void Add(RtaEvent rtaEvent)
        {
            events.Add(rtaEvent);
            this.fireOnEventAdded(rtaEvent);
        }
        public void Change(RtaEvent rtaEvent)
        {
            this.fireOnEventChanged(rtaEvent);
        }

        public void RemoveEvent(RtaEvent rtaEvent)
        {
            events.Remove(rtaEvent);
            this.fireOnEventRemoved(rtaEvent);
        }

        public void Add(RtaCategory newCategory, RtaCategory parent)
        {
            parent.getChildren().Add(newCategory);
            this.fireOnCategoryAdded(newCategory);
        }

        public void Add(RtaCategory category)
        {
            this.categories.Add(category);
            category.parent = null;
            this.fireOnCategoryAdded(category);
        }

        public void Change(RtaCategory rtaCategory)
        {
            this.fireOnCategoryChanged(rtaCategory);
        }

        public void RemoveCategory(RtaCategory category)
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


        public int CountChildren()
        {
            return this.categories.Count;
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

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
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
                Console.WriteLine(e);
                return;
            }
            

            try
            {
                RtaCategoryModel result = (RtaCategoryModel)serializer.Deserialize(fileStream);
                this.categories = result.categories;
                this.events = result.events;
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex);
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

            for (int i = 0; i < this.categories.Count; i++)
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



            for (int i = 0; i < this.events.Count; i++)
            {
                RtaEvent rtaEvent = this.events.ElementAt(i);
                visitor.onVisit(rtaEvent);
            }
            
        }

        protected void fireOnCategoryAdded(RtaCategory rtaCategory)
        {
            if (this.listener != null)
            {
                this.listener.onCategoryAdded(rtaCategory);
            }
        }

        protected void fireOnCategoryChanged(RtaCategory rtaCategory)
        {
            if (this.listener != null)
            {
                this.listener.onCategoryChanged(rtaCategory);
            }
        }

        protected void fireOnCategoryRemoved(RtaCategory rtaCategory)
        {
            if (this.listener != null)
            {
                this.listener.onCategoryRemoved(rtaCategory);
            }
        }

        protected void fireOnEventAdded(RtaEvent rtaEvent)
        {
            if (this.listener != null)
            {
                this.listener.onEventAdded(rtaEvent);
            }
        }

        protected void fireOnEventChanged(RtaEvent rtaEvent)
        {
            if (this.listener != null)
            {
                this.listener.onEventChanged(rtaEvent);
            }
        }

        protected void fireOnEventRemoved(RtaEvent rtaEvent)
        {
            if (this.listener != null)
            {
                this.listener.onEventRemoved(rtaEvent);
            }
        }

        
    }
}
