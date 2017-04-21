using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Indexer.REPO
{
    class SmartForm
    {
        internal List<SmartForm> getSmartForms()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                List<SmartForm> audits = cn.Query<SmartForm>("SELECT * from auditor.AuditFile").ToList();

                Console.WriteLine("\n*** Results *** \n");
                foreach (var audit in audits)
                {
                    Console.WriteLine(audit.sFileName);
                    Console.WriteLine("   Audit ID: " + audit.IAuditID);
                    Console.WriteLine("   Status ID: " + audit.IStatusID);
                    Console.WriteLine("   Type: " + audit.IType);
                    Console.WriteLine("   Description: " + audit.sDescription);
                }

                Console.WriteLine("\n*** End of Results ***");
                Console.ReadKey();
            }
        }
    }
}
