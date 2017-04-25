using Indexer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProactivityDataReader.Config;
using Dapper;

namespace Indexer.Repository
{
    public class AuditRepo
    {

        public IEnumerable<dynamic> GetDynamicAudits()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string sql =
                    @"SELECT sLastName, sName, af.*
                      FROM auditor.AuditFile af
                      INNER JOIN auditor.People p ON af.IAuditorID = p.IPersonID
                      INNER JOIN auditor.Status as s ON s.IStatusID = af.IStatusID";

                var smartForms = cn.Query(sql);

                return smartForms;
            }
        }

    }
}
