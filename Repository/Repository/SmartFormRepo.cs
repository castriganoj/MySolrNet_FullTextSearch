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
    public class SmartFormRepo
    {

        public IEnumerable<dynamic> GetDynamicSmartForms()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string sql =
                    @"select o2.sName as ParentName, o2.sOrgPath as ParentPath, i.*
                      from tracer.orgtree as o1
                      inner join tracer.orgtree as o2 on o1.IParentID = o2.INodeID                    
                      inner join tracer.IA_DataView as i on o1.INodeID = i.iNodeID";

                var smartForms = cn.Query(sql);

                return smartForms;

            }
        }

    }
}
