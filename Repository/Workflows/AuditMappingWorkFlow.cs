using Indexer.Models;
using Indexer.Repository;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indexer
{
    public class AuditMappingWorkFlow : IWorkFlow
    {

        public void Execute()
        {
            AuditRepo repo = new AuditRepo();
            var dynamicAudits = repo.GetDynamicAudits();

            List<EHSDoc> solrAudits = MapSolrAudits(dynamicAudits);

            ISolrOperations<EHSDoc> solrAuditOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EHSDoc>>();

            foreach (var solrAudit in solrAudits)
            {
                solrAuditOperations.Add(solrAudit);
            }

            solrAuditOperations.Commit();

        }

        private List<EHSDoc> MapSolrAudits(IEnumerable<dynamic> dynamicAudits)
        {
            var auditsList = new List<EHSDoc>();

            foreach (var dumbAudit in dynamicAudits)
            {

                var audit = new Dictionary<string, string>();

                foreach (var pair in dumbAudit)
                {
                    //transform the object in the key value pair to a string
                    if (pair.Value != null)
                    {
                        audit.Add(pair.Key, pair.Value.ToString());
                    }

                }

                DateTime createDate;
                DateTime.TryParse(audit.SingleOrDefault(colHead => colHead.Key == "dAuditDate").Value, out createDate);

                var solrAudit = new EHSDoc()
                {
                    DocumentType = "Auditor",
                    Name = audit.SingleOrDefault(colHead => colHead.Key == "sFileName").Value,
                    CreateDate = createDate,
                    Description = audit.SingleOrDefault(colHead => colHead.Key == "sDescription").Value,
                    Status = audit.SingleOrDefault(coldHead => coldHead.Key == "sName").Value,
                    OrgLocation = audit.SingleOrDefault(colHead => colHead.Key == "sOrgPath").Value,
                    ValueAnswers = string.Join(", ", audit.Values)
                };

                auditsList.Add(solrAudit);
            }

            return auditsList;
        }
    }
}

