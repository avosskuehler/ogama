using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta
{
    public class DaoFactory
    {
        public static RtaSettingsDao rtaSettingsDao;

        private static System.Object LOCK = new System.Object();

        public static RtaSettingsDao getRtaSettingsDao()
        {
            lock (LOCK)
            {
                if (rtaSettingsDao == null)
                {
                    System.Data.SqlClient.SqlConnection sqlConnection = Document.ActiveDocument.DocDataSet.DatabaseConnection;
                    rtaSettingsDao = new RtaSettingsDao();
                    rtaSettingsDao.setDatabaseConnection(sqlConnection);
                }
                return rtaSettingsDao;
            }
        }
    }
}
