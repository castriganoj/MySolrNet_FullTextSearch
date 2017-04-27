using SearchLibrary.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchWebUi.Models
{
    public class SearchVM
    {
        public EhsQuery EhsQuery { get; set; }

        public QueryResponse QueryResponse { get; set; }

        public List<FilterCheckBox> filters { get; set; }

    }
}