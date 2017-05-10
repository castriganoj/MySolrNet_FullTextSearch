using SearchLibrary;
using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using SearchWebUi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;

namespace SearchWebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(SearchVM searchVM)
        {
            EhsQuery ehsQuery;

            if (searchVM.EhsQuery == null)
            {
                ehsQuery = new EhsQuery();
                ehsQuery.QueryText = "*:*";
                searchVM.EhsQuery = ehsQuery;
            }
            else
            {
                ehsQuery = searchVM.EhsQuery;
            }

            if (searchVM.filters != null)
            {
                ehsQuery = CheckForSeaerchFilters(searchVM.filters, ehsQuery);
            }

            var searchService = new EHSSearch();


            QueryResponse result = searchService.DoSearch(ehsQuery);

            ModelState.Clear();
            searchVM.QueryResponse = result;
            searchVM.EhsQuery = new EhsQuery();
            searchVM.EhsQuery.QueryText = result.QueryText.QueryText;
            searchVM.filters = BuildFilterCheckBoxes(result, searchVM.filters);
            

            return View(searchVM);
        }

        public ActionResult SearchPreview()
        {
            return View();
        }

        private List<FilterCheckBox> BuildFilterCheckBoxes(QueryResponse result, List<FilterCheckBox> filters)
        {
            var filterCheckBoxes = new List<FilterCheckBox>();

            foreach (var orgFacet in result.OrgLocationFacet)
            {
                var checkBox = new FilterCheckBox();
                checkBox.FilterName = orgFacet.Key;
                checkBox.Type = FilterType.OrgLocation;

                filterCheckBoxes.Add(checkBox);
            }

            foreach (var orgFacet in result.DocumentTypeFacet)
            {
                var checkBox = new FilterCheckBox();
                checkBox.FilterName = orgFacet.Key;
                checkBox.Type = FilterType.Document;
                filterCheckBoxes.Add(checkBox);

            }

            foreach (var orgFacet in result.StatusTypeFacet)
            {
                var checkBox = new FilterCheckBox();
                checkBox.FilterName = orgFacet.Key;
                checkBox.Type = FilterType.Status;
                filterCheckBoxes.Add(checkBox);
            }

            foreach (var orgFacet in result.DateCreatedFacet)
            {
                var checkBox = new FilterCheckBox();
                checkBox.FilterName = orgFacet.Key;
                checkBox.Type = FilterType.Date;
                filterCheckBoxes.Add(checkBox);
            }

            return filterCheckBoxes;

        }

        private EhsQuery CheckForSeaerchFilters(List<FilterCheckBox> filters, EhsQuery ehsQuery)
        {
            foreach (var filter in filters)
            {
                if (filter.Selected == true)
                {
                    switch (filter.Type)
                    {
                        case FilterType.Document:
                            ehsQuery.ProductTypeFilter.Add(filter.FilterName);
                            break;

                        case FilterType.OrgLocation:
                            ehsQuery.OrgLocationFilter.Add(filter.FilterName);
                            break;

                        case FilterType.Status:
                            ehsQuery.StatusFilter.Add(filter.FilterName);
                            break;
                          
                        case FilterType.Date:
                            ehsQuery.DateFilter.Add(new DateTime(Int32.Parse(filter.FilterName), 1, 1));
                            break;

                        default:
                            break;
                    }
                }
            }

            return ehsQuery;
        }

    }
}