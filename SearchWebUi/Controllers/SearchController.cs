using SearchLibrary;
using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SearchWebUi.Controllers
{
    public class SearchController : ApiController
    {

        public Dictionary<string, List<EHSDoc>> Get(string query)
        
        {
            if (String.IsNullOrEmpty(query))
            {
                query = "*:*";
            }

            var searchService = new EHSSearch();

            Query testQuery = new Query();
            testQuery.QueryText = query;

            QueryResponse result = searchService.DoSearch(testQuery);

            List<EHSDoc> EhsDocs = result.Results;

            Dictionary<string, List<EHSDoc>> GroupedSearchResults = GroupByProducts(EhsDocs);

            return GroupedSearchResults;
        }

        private Dictionary<string, List<EHSDoc>> GroupByProducts(List<EHSDoc> EhsDocs)
        {
            Dictionary<string, List<EHSDoc>> docs =  EhsDocs
                .GroupBy(ehsDoc => ehsDoc.ProductType)
                .ToDictionary(g => g.Key, g => g.ToList());

            return docs;

        }
    }

}