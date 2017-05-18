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


            foreach (DateTime createYearFilter in query.DateFilter)
            {
                
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
