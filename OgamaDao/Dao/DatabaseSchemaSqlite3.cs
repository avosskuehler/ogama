using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace OgamaDao.Dao
{
    public class DatabaseSchemaSqlite3
    {
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
            ISQLQuery query = 
                session.CreateSQLQuery("CREATE TABLE RtaCategory2 (ID UNIQUEIDENTIFIER not null, name TEXT, description TEXT, parent UNIQUEIDENTIFIER unique, show BOOL, primary key (ID), constraint FKB129CDD3655B7E foreign key (parent) references RtaCategory)");
            try
            {
                int n = query.ExecuteUpdate();
            }
            catch (Exception e)
            {
                
            }
            
            //CREATE TABLE RtaEvent (ID UNIQUEIDENTIFIER not null, fkRtaCategory UNIQUEIDENTIFIER unique, Xstart DOUBLE, Xend DOUBLE, startTimestamp DOUBLE, endTimestamp DOUBLE, primary key (ID), constraint FKF3AFA8173F19C63B foreign key (fkRtaCategory) references RtaCategory);
            //CREATE TABLE RtaSettings (ID UNIQUEIDENTIFIER not null, MonitorIndex INT, Framerate INT, TempFilename TEXT, Filename TEXT, VideoCompressorName TEXT, AudioInputDeviceName TEXT, AudioCompressorName TEXT, primary key (ID));

        }

        public void updateDatabaseVersion_0_1(IStatelessSession session)
        {

        }
    }
}
