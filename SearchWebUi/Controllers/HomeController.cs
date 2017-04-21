using SearchLibrary;
using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchWebUi.Controllers
{
    public class HomeController : Controller
    {
      public ActionResult Index(string query)
        {
            //if (String.IsNullOrEmpty(query))
            //{
            //    query = "commercial";
            //}

            var searchService = new EHSSearch();

            Query testQuery = new Query();
            testQuery.QueryText = query;

            QueryResponse result = searchService.DoSearch(testQuery);

            return View(result);
        }

      private Dictionary<string, List<EHSDoc>> GroupByProducts(List<EHSDoc> EhsDocs)
      {
          Dictionary<string, List<EHSDoc>> docs = EhsDocs
              .GroupBy(ehsDoc => ehsDoc.ProductType)
              .ToDictionary(g => g.Key, g => g
              .ToList());

          return docs;

      }
    }
}