using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.Models.EHS
{
    public class EHSDoc
    {
        [SolrField("productType")]
        public string ProductType { get; set; }

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

        [SolrField("answers")]
        public List<string> ValueAnswers { get; set; }
     
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
        public List<string> Domain { get; set; }

        //audit
        [SolrField("status")]
        public string Status { get; set; }

    }
}
