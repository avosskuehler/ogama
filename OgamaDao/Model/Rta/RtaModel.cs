using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao.Rta;

namespace OgamaDao.Model.Rta
{
    /// <summary>
    /// Facade class for the whole RTA model
    /// </summary>
    public class RtaModel
    {
        private List<RtaCategory> rtaCategories = new List<RtaCategory>();
        private List<RtaEvent> rtaEvents = new List<RtaEvent>();

        private RtaCategoryDao rtaCateogryDao;
        private RtaEventDao rtaEventDao;

        public void SetRtaCategoryDao(RtaCategoryDao dao)
        {
            this.rtaCateogryDao = dao;
        }

        public void SetRtaEventDao(RtaEventDao dao)
        {
            this.rtaEventDao = dao;
        }

        public void Add(RtaEvent rtaEvent)
        {
            if (rtaEvent == null)
            {
                return;
            }
            this.rtaEvents.Add(rtaEvent);
        }

        public void Add(RtaCategory newRtaCategory)
        {
            Add(newRtaCategory, null);
        }

        public void Add(RtaCategory newRtaCategory, RtaCategory parentRtaCategory)
        {
            if (newRtaCategory == null)
            {
                return;
            }
            if (parentRtaCategory != null)
            {
                parentRtaCategory.Add(newRtaCategory);
            }
            this.rtaCategories.Add(newRtaCategory);
        }

        public List<RtaEvent> getRtaEvents()
        {
            return this.rtaEvents;
        }

        public List<RtaCategory> getRtaCategories()
        {
            return this.rtaCategories;
        }

        public void Remove(RtaCategory c)
        {
            this.rtaCategories.Remove(c);
        }

        public void Remove(RtaEvent e)
        {
            this.rtaEvents.Remove(e);
        }

        public void SaveRtaCategories()
        {
            this.rtaCateogryDao.save(this.rtaCategories);
        }

        public void SaveRtaEvents()
        {
            this.rtaEventDao.save(this.rtaEvents);            
        }

        public void Load()
        {
            this.rtaCategories = this.rtaCateogryDao.findAll();
            this.rtaEvents = this.rtaEventDao.findAll();
        }

        public void visit(IRtaModelVisitor visitor)
        {

            List<RtaCategory> categories = new List<RtaCategory>();

            for (int i = 0; i < this.getRtaCategories().Count; i++)
            {
                categories.Add(this.getRtaCategories().ElementAt(i));
            }

            Stack<RtaCategory> parentNodes = new Stack<RtaCategory>();

            for (int i = 0; i < categories.Count; i++)
            {
                RtaCategory currentItem = categories.ElementAt(i);

                visitor.onVisit(currentItem);


                List<RtaCategory> subList = currentItem.getChildren();

                categories.InsertRange(i + 1, subList);
            }



            for (int i = 0; i < this.getRtaEvents().Count; i++)
            {
                RtaEvent rtaEvent = this.getRtaEvents().ElementAt(i);
                visitor.onVisit(rtaEvent);
            }

        }

        public void Save()
        {
            this.SaveRtaCategories();
            this.SaveRtaEvents();
        }
    }
}
