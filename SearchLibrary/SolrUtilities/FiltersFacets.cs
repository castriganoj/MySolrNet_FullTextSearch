using SearchLibrary.Models.Query;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.SolrUtilities
{
    internal class FiltersFacets
    {

        internal ICollection<ISolrQuery> BuildFilterQueries(EhsQuery query)
        {
            ICollection<ISolrQuery> filters = new List<ISolrQuery>();

            List<SolrQueryByField> refinersDocumentType = new List<SolrQueryByField>();
            List<SolrQueryByField> refinersStatus = new List<SolrQueryByField>();
            List<SolrQueryByField> refinersOrgLocation = new List<SolrQueryByField>();
            List<SolrQueryByRange<DateTime?>> refinersCreateDate = new List<SolrQueryByRange<DateTime?>>(); 

            //ie filter for auditor and tracer
            foreach (string documentType in query.ProductTypeFilter)
            {
                refinersDocumentType.Add(new SolrQueryByField("documentType", documentType));
            }

            foreach (string orgLocation in query.OrgLocationFilter)
            {
                refinersOrgLocation.Add(new SolrQueryByField("orgLocation", orgLocation));
            }

            foreach (string statusFilter in query.StatusFilter)
            {
                refinersStatus.Add(new SolrQueryByField("status", statusFilter));
            }

            //ISolrOperations<Product> solr = ...
            //var products = solr.Query(new SolrQueryByRange<decimal>("price", 100m, 250.50m)); // search for price between 100 and 250.50

            foreach (DateTime createYearFilter in query.DateFilter)
            {
                //[2008-01-01T00:00:00Z TO 2008-12-31T23:59:59Z]
                //string year = createYearFilter.Year.ToString();
                //string dateForSolr = "[" + year + "-01-01T00:00:00Z TO " + year + "-12-31T23:59:59Z]";
                
                refinersCreateDate.Add(new SolrQueryByRange<DateTime?>("createDate", createYearFilter, createYearFilter.AddYears(+1).AddDays(-1)));
            }


            if (refinersDocumentType.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersDocumentType, "OR")); 
            }


            if (refinersOrgLocation.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersOrgLocation, "OR")); 
            }


            if (refinersStatus.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersStatus, "OR")); 
            }

            if (refinersCreateDate.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersCreateDate, "OR"));
            }



            return filters;
        }

        internal FacetParameters BuildFacets()
        {
            return new FacetParameters
            {
                Queries = new List<ISolrFacetQuery>
                {
                     new SolrFacetFieldQuery("documentType"){Limit=10},
                     new SolrFacetFieldQuery("status"),
                     new SolrFacetFieldQuery("orgLocation") { Limit=5},
                     new SolrFacetDateQuery("createDate",new DateTime(DateTime.Now.Year, 1, 1).AddYears(-10),new DateTime(DateTime.Now.Year, 1, 1).AddYears(+1),"+1YEAR") 
                }
            };
        }
    }
}
