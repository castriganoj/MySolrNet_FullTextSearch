using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchLibrary.Models.Query
{
    public class EhsQuery
    {
        public EhsQuery()
        {
            Rows = 20;
            Start = 0;
            ProductTypeFilter = new List<string>();
            StatusFilter = new List<string>();
            OrgLocationFilter = new List<string>();
            DateFilter = new List<DateTime>();

        }

        public string QueryText { get; set; }

        public int Start { get; set; }

        public int Rows { get; set; }

        public List<string> ProductTypeFilter { get; set;}
        public List<string> StatusFilter { get; set; }
        public List<string> OrgLocationFilter { get; set; }
        public List<DateTime> DateFilter { get; set; }

        public Dictionary<string, List<string>> Filters {get; set;}

    }
}