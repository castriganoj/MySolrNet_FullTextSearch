using SearchLibrary;
using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using SearchWebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchWebUi.Controllers
{
    public class HomeController : Controller
    {
      public ActionResult Index(SearchVM searchVM)
        {
            //first pass there will not be any query results or query paramas
            //second pass there will be query params
            //who cares about query results on first pass or subsequent passes becase it will be reset
            //query params should be set if they are not set from the view
            EhsQuery ehsQuery;
           
            if(searchVM.EhsQuery == null)
            { 
                ehsQuery = new EhsQuery();
                ehsQuery.QueryText =  "*:*";
                searchVM.EhsQuery = ehsQuery;
                
            }
            else
            {
                ehsQuery = searchVM.EhsQuery;
            }

            if(searchVM.filters != null)
            {
                ehsQuery = checkForSearchFilters(searchVM.filters, ehsQuery);
            }
           
            var searchService = new EHSSearch();


            QueryResponse result = searchService.DoSearch(ehsQuery);

            searchVM.QueryResponse = result;          
            searchVM.EhsQuery = new EhsQuery();
            searchVM.filters = new List<FilterCheckBox>()
            {
                new FilterCheckBox() {FilterName = "Tracer" },
                new FilterCheckBox() {FilterName = "Auditor" },
                new FilterCheckBox() {FilterName = "Scout" },
            };

            return View(searchVM);
        }

        private EhsQuery checkForSearchFilters(List<FilterCheckBox> filters, EhsQuery ehsQuery)
        {
            foreach (var filter in filters)
            {
                if (filter.Selected == true)
                {
                    ehsQuery.ProductTypeFilter.Add(filter.FilterName);
                }
            }

            return ehsQuery;
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