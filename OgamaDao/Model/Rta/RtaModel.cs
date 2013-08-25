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

        private RtaCategoryDao rtaCateogryDao;
        private RtaEventDao rtaEventDao;
        private RtaSettings rtaSettings;

        public void Init(OgamaDao.Dao.DaoFactory daoFactory)
        {
            this.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());
            this.SetRtaEventDao(daoFactory.getRtaEventDao());
        }

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
            if (rtaEvent.fkRtaCategory == null)
            {
                throw new System.InvalidOperationException("given RtaEvent has no RtaCategory!");
            }
            rtaEvent.fkRtaCategory.Add(rtaEvent);
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

        public List<RtaEvent> getRtaEvents(RtaCategory rtaCategory)
        {
            return rtaEventDao.findByRtaCategory(rtaCategory);
        }

        public List<RtaCategory> getRtaCategories()
        {
            return this.rtaCategories;
        }

        public void Remove(RtaCategory c)
        {
            this.rtaCategories.Remove(c);
        }

        public void Remove(RtaEvent rtaEvent)
        {
            if (rtaEvent == null)
            {
                return;
            }
            if (rtaEvent.fkRtaCategory == null)
            {
                throw new System.InvalidOperationException("given RtaEvent has no RtaCategory!");
            }
            rtaEvent.fkRtaCategory.GetRtaEvents().Remove(rtaEvent);
            rtaEventDao.delete(rtaEvent);
        }

        public void SaveRtaCategories()
        {
            this.rtaCateogryDao.save(this.rtaCategories);
            SaveRtaEvents();
        }

        protected void SaveRtaEvents()
        {
            foreach (RtaCategory rtaCategory in rtaCategories)
            {
                rtaEventDao.save(rtaCategory.GetRtaEvents());
            }
        }

        public void Load(RtaSettings rtaSettings)
        {
            this.rtaSettings = rtaSettings;
            LoadCategories(rtaSettings);
            LoadEvents();
        }

        public void LoadCategories(RtaSettings rtaSettings)
        {
            this.rtaCategories = this.rtaCateogryDao.findByRtaSettings(rtaSettings);
        }

        protected void LoadEvents()
        {
            foreach (RtaCategory rtaCategory in getRtaCategories())
            {
                List<RtaEvent> rtaEvents = rtaEventDao.findByRtaCategory(rtaCategory);
                rtaCategory.SetRtaEvents(rtaEvents);
            }
            
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
                List<RtaEvent> rtaEvents = getRtaEvents(currentItem);
                currentItem.SetRtaEvents(rtaEvents);

                visitor.onVisit(currentItem);

                List<RtaCategory> subList = currentItem.getChildren();
                categories.InsertRange(i + 1, subList);
            }
        }

        public void Save()
        {
            this.SaveRtaCategories();
        }

        public RtaSettings getCurrentRtaSettings()
        {
            return this.rtaSettings;
        }
    }
}
