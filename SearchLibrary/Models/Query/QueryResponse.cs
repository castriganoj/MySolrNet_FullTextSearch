
using SearchLibrary.Models.EHS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchLibrary.Models.Query
{
    public class QueryResponse
    {
         public QueryResponse()
        {
            ProductTypeFacet = new List<KeyValuePair<string, int>>();
            StatusTypeFacet = new List<KeyValuePair<string, int>>();
            OrgLocationFacet = new List<KeyValuePair<string, int>>();
        }

        //Expose properties that will be returned to from the search library
        public List<EHSDoc> Results { get; set; }

        public int TotalHits { get; set; }

        public int QueryTime { get; set; }

        public int Status { get; set; }

        public EhsQuery QueryText { get; set; }

        public List<KeyValuePair<string, int>> ProductTypeFacet { get; set; }

        public List<KeyValuePair<string, int>> StatusTypeFacet { get; set; }

        public List<KeyValuePair<string, int>> OrgLocationFacet{ get; set; }

        public List<KeyValuePair<string, int>> DateCreatedFacet { get; set; }
    }
}