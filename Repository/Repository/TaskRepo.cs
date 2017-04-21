using ProactivityDataReader.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Indexer.Workflows
{
    public class TaskRepo
    {

        public IEnumerable<dynamic> GetTasks()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string sql =
                    @"SELECT sOrgPath, a.*
                    FROM tracer.aud_correctiveaction a
                    INNER JOIN tracer.aud_finding f
                    ON a.IAuditFileID = f.IAuditFileID 
                    AND a.ICorrActionSN = f.IFindingID 
                    INNER JOIN tracer.AuditFile ad
                    ON f.IAuditFileID = ad.IAuditFileID";

                var tasks = cn.Query(sql);

                return tasks;
            }
        }

    }
}
