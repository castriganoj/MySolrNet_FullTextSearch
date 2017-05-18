using Indexer.Models;
using Indexer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using Microsoft.Practices.ServiceLocation;

namespace Indexer.Workflows
{
    public class SmartFormMappingWorkflow : IWorkFlow
    {

        public void Execute()
        {
         
            SmartFormRepo repo = new SmartFormRepo();
            var dynamicSmartForms = repo.GetDynamicSmartForms();

            List<EHSDoc> smartForms = MapEHSDoc(dynamicSmartForms);

            ISolrOperations<EHSDoc> solrAuditOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EHSDoc>>();

            foreach (var sf in smartForms)
            {

                if ((!String.IsNullOrWhiteSpace(sf.FormType) && !sf.FormType.Contains("GHG")))
                {
                    solrAuditOperations.Add(sf);
                }
            }
            solrAuditOperations.Commit();
        }


        private List<EHSDoc> MapEHSDoc(IEnumerable<dynamic> dumbForms)
        {
            var formsList = new List<EHSDoc>();

            foreach (var dumbForm in dumbForms)
            {
                //Move casting to another componenet or into repo These method
                //should not concern itself with dynamic objects. 

                var sf = new Dictionary<string, string>();

                foreach (var pair in dumbForm)
                {
                    //cast the object in the key value pair to a string
                    if (pair.Value != null)
                    {
                        sf.Add(pair.Key, pair.Value.ToString());
                    }

                }

                DateTime createDate;
                DateTime.TryParse(sf.SingleOrDefault(colHead => colHead.Key == "dData_DateReported").Value, out createDate);

                var smartForm = new EHSDoc()
                {
                    DocumentType = sf.SingleOrDefault(colHead => colHead.Key == "vData_ReportType").Value,
                    CreateDate = createDate,
                    Creator = sf.SingleOrDefault(colHead => colHead.Key == "vData_RptrLName").Value,
                    FormType = sf.SingleOrDefault(colHead => colHead.Key == "vData_ReportType").Value,
                    OrgLocation = sf.SingleOrDefault(colHead => colHead.Key == "ParentPath").Value,
                    Status = sf.SingleOrDefault(colHead => colHead.Key == "vData_Status").Value,
                    ValueAnswers = string.Join(", ", sf.Values),
                    EditDate = null
                };

                formsList.Add(smartForm);
            }

            return formsList;
        }
    }

}

