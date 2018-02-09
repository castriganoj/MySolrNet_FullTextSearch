using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer.Models
{
    public class EHSDoc
    {
        [SolrField("EhsId")]
        public int EhsId { get; set; }

        [SolrField("documentType")]
        public string DocumentType { get; set; }

        [SolrField("orgLocation")]
        public string OrgLocation { get; set; }

        [SolrField("createDate")]
        public DateTime CreateDate { get; set; }

        [SolrField("editDate")]
        public string EditDate { get; set; }

        [SolrField("creator")]
        public string Creator { get; set; }

        [SolrField("viewers")]
        public List<string> Viewers { get; set; }

        [SolrField("status")]
        public string Status { get; set; }

        [SolrField("answers")]
        public string ValueAnswers { get; set; }
     
        //sf
        [SolrField("formType")]
        public string FormType { get; set; } 

        //task //audit
        [SolrField("name")]
        public string Name { get; set; }

        //task //audit
        [SolrField("description")]
        public string Description { get; set; }

        //task //audit
        [SolrField("domains")]
        public string Domain { get; set; }

       

    }
}
