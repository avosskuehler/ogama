using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model;

namespace OgamaDao.Dao
{
    public class FindRequest<T> where T : BaseModel
    {
        public T entity { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<string> ignoreProperties = new List<string>();
        public void ignore(string propertyName)
        {
            this.ignoreProperties.Add(propertyName);
        }
        public List<Tuple<string, string>> restrictions = new List<Tuple<string, string>>();

    }
}
