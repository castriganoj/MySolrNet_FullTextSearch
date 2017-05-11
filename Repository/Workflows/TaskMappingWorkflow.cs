using Indexer.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer.Workflows
{
    public class TaskMappingWorkflow : IWorkFlow
    {

        public void Execute()
        {

            TaskRepo repo = new TaskRepo();
            var dynamicTasks = repo.GetTasks();


            List<EHSDoc> EHSDocs = MapEHSDocs(dynamicTasks);

            ISolrOperations<EHSDoc> solrAuditOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EHSDoc>>();

            foreach (var EHSDoc in EHSDocs)
            {
                solrAuditOperations.Add(EHSDoc);
            }

            solrAuditOperations.Commit();
        }

        private List<EHSDoc> MapEHSDocs(IEnumerable<dynamic> dumbTasks)
        {
            List<EHSDoc> taskList = new List<EHSDoc>();

            foreach (var dumbTask in dumbTasks)
            {
                //Move casting to another componenet or into repo These method
                //should not concern itself with dynamic objects. 

                var task = new Dictionary<string, string>();

                foreach (var pair in dumbTask)
                {
                    //transform the object in the key value pair to a string
                    //if its null theres nothing to search anyway.
                    if (pair.Value != null)
                    {
                        task.Add(pair.Key, pair.Value.ToString());
                    }

                }
                DateTime createDate;
                DateTime.TryParse(task.SingleOrDefault(colHead => colHead.Key == "dDate_DateOrdered").Value, out createDate);

                var EHSDoc = new EHSDoc()
                {
                    DocumentType = "Tracer",
                    Name = task.SingleOrDefault(colHead => colHead.Key == "ItemName").Value,
                    Description = task.SingleOrDefault(colHead => colHead.Key == "sDescription").Value,
                    Domain = task.SingleOrDefault(colHead => colHead.Key == "sDomainPath").Value,
                    OrgLocation = task.SingleOrDefault(colHead => colHead.Key == "sOrgPath").Value,
                    CreateDate = createDate,
                    Creator = task.SingleOrDefault(colHead => colHead.Key == "sResponsiblePerson").Value,
                    Status = task.SingleOrDefault(colHead => colHead.Key == "sName").Value,
                    ValueAnswers = string.Join(", ", task.Values)
                };

                taskList.Add(EHSDoc);
            }

            return taskList;
        }
    }
}