using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchLibrary.Models.Query
{
    public class Query
    {
        public Query()
        {
            Rows = 20;
            Start = 0;
            ProductTypeFilter = new List<string>();
        }
        public string QueryText { get; set; }

        public int Start { get; set; }

        public int Rows { get; set; }

        public List<string> ProductTypeFilter { get; set;}
    }
}