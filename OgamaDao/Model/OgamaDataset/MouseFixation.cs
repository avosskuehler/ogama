using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaDao.Model.OgamaDataset
{
    public class MouseFixation : BaseModel
    {

        public virtual string SubjectName {get; set;}
        public virtual int TrialID {get; set;}
        public virtual int TrialSequence { get; set; }
        public virtual int CountInTrial { get; set; }
        public virtual long StartTime{get; set;}
        public virtual int Length{get; set;}
        public virtual double PosX{get; set;}
        public virtual double PosY{get; set;}
    }
}
