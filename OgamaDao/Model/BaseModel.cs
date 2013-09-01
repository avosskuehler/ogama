using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaDao.Model
{
    public class BaseModel
    {
       public virtual Guid ID { get; set; }
       public virtual long Version { get; set; }
    }
}
