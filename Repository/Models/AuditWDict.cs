using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer.Models
{
    public class AuditWDict
    {
        //Don't necessairly need this with managed schema. 
        [SolrUniqueKey("auditid")]
        public string Id { get; set; }

        [SolrField("name")]
        public string Name { get; set; }
        
        [SolrField("questions")]
        public List<string> Questions { get; set; }

        [SolrField("answers")]
        public List<string> Answers { get; set; }

        [SolrField("domains")]
        public List<string> Domains { get; set; }

        [SolrField("subdomains")]
        public List<string> SubDomains { get; set; }
        
        [SolrField("applicable")]
        public bool Applicable { get; set; }

        [SolrField("createdate")]
        public DateTime CreateDate { get; set; }

        [SolrField("status")]
        public string Status { get; set; }

        //maps to the key in the dictionary
        [SolrField("*")]
        public IDictionary<string, object> OtherFields {get;set;}

        //[SolrField("LibraryReference_")] LibraryReference
        //This annotaion would map to a solr schema field prefixed with text before the underscore
        //for example the dictionary key LibraryReference["WasteProcess"] would map to the solr field LibraryReferenceWasteProcess
    }
}
