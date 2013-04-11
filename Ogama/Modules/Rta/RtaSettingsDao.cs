using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta
{
    public class RtaSettingsDao
    {
        private System.Data.SqlClient.SqlConnection databaseConnection;
        private const string TABLE_NAME = "RtaSetting";

        public void setDatabaseConnection(System.Data.SqlClient.SqlConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public RtaSettings save(RtaSettings entity)
        {

            return entity;
        }

        protected void checkDatabase()
        {    
            if (!Ogama.Modules.Common.Tools.Queries.TableExists(TABLE_NAME))
            {
                string tableName = TABLE_NAME;
                string columnDefinitions = "";
                string columns = "";
                Ogama.Modules.Common.Tools.Queries.RecreateTable(tableName, columnDefinitions, columns);
            }
        }
    }
}


