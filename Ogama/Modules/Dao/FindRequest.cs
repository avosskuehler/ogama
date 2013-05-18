using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Dao
{
    public class FindRequest<T> where T : BaseModel
    {
        public T entity { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

    }
}
