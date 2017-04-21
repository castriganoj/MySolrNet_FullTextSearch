using Indexer.Models;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Indexer
{
    public class JsonFileMapper  : IWorkFlow
    {
        public void Execute()
        {
            Console.WriteLine("Mapper mode: (A)annotations, (AD)annotations and dictionary, (DD) dynamic dictionary");
            string input = Console.ReadLine();

            //default
            var audit = new EHSDoc();
            audit = CreateAnnotaionMapping();

            if (input == "A")
            {
                audit = CreateAnnotaionMapping();
                Startup.Init<EHSDoc>("http://localhost:8983/solr/Audits");
                ISolrOperations<EHSDoc> solrAuditOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EHSDoc>>();
                solrAuditOperations.Add(audit);
                solrAuditOperations.Commit();
            }
            else if (input == "AD")
            {
                var auditWDict = new AuditWDict();
                auditWDict = CreateAnnotationWDictionaryMapping();
                Startup.Init<AuditWDict>("http://localhost:8983/solr/Audits");
                ISolrOperations<AuditWDict> solrAuditWDictOperations= ServiceLocator.Current.GetInstance<ISolrOperations<AuditWDict>>();
                solrAuditWDictOperations.Add(auditWDict);
                solrAuditWDictOperations.Commit();

            }
            else if (input == "DD")
            {
                var auditDict = CreateFullyLooseMapping().audit;
                Startup.Init<Dictionary<string, object>>("http://localhost:8983/solr/Audits");
                ISolrOperations<Dictionary<string,object>> solrFullyLooseDictMap = ServiceLocator.Current.GetInstance<ISolrOperations<Dictionary<string, object>>>();
                solrFullyLooseDictMap.Add(auditDict);
                solrFullyLooseDictMap.Commit();
            }


            

           

        }

        private AuditDictionary CreateFullyLooseMapping()
        {
            List<AuditWDict> audits = JsonConvert.DeserializeObject<List<AuditWDict>>(File.ReadAllText(@"Audits.json"));

            var auditFromFile = audits[2];

            var auditDictionary = new AuditDictionary();

            auditDictionary.audit = new Dictionary<string, object>();

            auditDictionary.audit.Add("audit from fully loose mapping", "this demonstrates that a loosely defined mapping via a c# dictionary can be passed to the same core as a stringly typed tightly mapped c# object");
            auditDictionary.audit.Add("auditid", auditFromFile.Id);
            auditDictionary.audit.Add("name", auditFromFile.Name);
            auditDictionary.audit.Add("questions", auditFromFile.Questions);
            auditDictionary.audit.Add("answers", auditFromFile.Answers);
            auditDictionary.audit.Add("domains", auditFromFile.Domains);
            auditDictionary.audit.Add("SubDomains", auditFromFile.SubDomains);
            auditDictionary.audit.Add("applicable", auditFromFile.Applicable);
            auditDictionary.audit.Add("createdate", auditFromFile.CreateDate);
            auditDictionary.audit.Add("status", auditFromFile.Status);
            //other fields
            auditDictionary.audit.Add("ReferenceLibrary", "5.32.2.1");
            auditDictionary.audit.Add("Creator", "JohnDoe");
            

            return auditDictionary;

        }

        private AuditWDict CreateAnnotationWDictionaryMapping()
        {
            List<AuditWDict> audits = JsonConvert.DeserializeObject<List<AuditWDict>>(File.ReadAllText(@"Audits.json"));
            audits[1].OtherFields = new Dictionary<string, object>();

            //other fields
            audits[1].OtherFields.Add("Field from annotation w/ dict", "cool");
            audits[1].OtherFields.Add("Creator", "John Doe");

            return audits[1];
        }

        private EHSDoc CreateAnnotaionMapping()
        {
            List<EHSDoc> audits = JsonConvert.DeserializeObject<List<EHSDoc>>(File.ReadAllText(@"Audits.json"));

            return audits[0];
        }
    }
}
