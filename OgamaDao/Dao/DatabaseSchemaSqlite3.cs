using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NLog;

namespace OgamaDao.Dao
{
    public class DatabaseSchemaSqlite3
    {

        private Logger log = new LogFactory().GetCurrentClassLogger();

        public Boolean databaseExists(IStatelessSession session)
        {
            return false;
        }

        public void createDatabase(IStatelessSession session)
        {
            this.createInitialDatabase(session);
            this.updateDatabaseVersion_0_1(session);
        }

        public void createInitialDatabase(IStatelessSession session)
        {
            string[] queries = new string[] { 
                "CREATE TABLE RtaCategory (ID UNIQUEIDENTIFIER not null, name TEXT, description TEXT, parent UNIQUEIDENTIFIER, fkRtaSettings UNIQUEIDENTIFIER, show BOOL, primary key (ID), constraint FKB129CDD3655B7E foreign key (parent) references RtaCategory, constraint FKB129CDD8D6D98B foreign key (fkRtaSettings) references RtaSettings);",
                "CREATE TABLE RtaEvent (ID UNIQUEIDENTIFIER not null, fkRtaCategory UNIQUEIDENTIFIER, Xstart DOUBLE, Xend DOUBLE, startTimestamp DOUBLE, endTimestamp DOUBLE, primary key (ID), constraint FKF3AFA8173F19C63B foreign key (fkRtaCategory) references RtaCategory);",
                "CREATE TABLE RtaSettings (ID UNIQUEIDENTIFIER not null, MonitorIndex INT, Framerate INT, TempFilename TEXT, Filename TEXT, VideoCompressorName TEXT, AudioInputDeviceName TEXT, AudioCompressorName TEXT, primary key (ID));"
            };
            foreach (String s in queries)
            {
                ISQLQuery query = session.CreateSQLQuery(s);
                try
                {
                    int n = query.ExecuteUpdate();
                }
                catch (Exception e)
                {
                    log.Error("could not create table:" + e);
                }
            }
        }

        public void updateDatabaseVersion_0_1(IStatelessSession session)
        {
            //session.CreateSQLQuery("ALTER ");
        }
    }
}
