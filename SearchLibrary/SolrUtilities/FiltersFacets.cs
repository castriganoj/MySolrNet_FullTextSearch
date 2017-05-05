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

            List<SolrQueryByField> refinersProductType = new List<SolrQueryByField>();
            List<SolrQueryByField> refinersStatus = new List<SolrQueryByField>();
            List<SolrQueryByField> refinersOrgLocation = new List<SolrQueryByField>();
            List<SolrQueryByField> refinersCreateDate = new List<SolrQueryByField>();

            //ie filter for auditor and tracer
            foreach (string productType in query.ProductTypeFilter)
            {
                refinersProductType.Add(new SolrQueryByField("productType", productType));
            }

            foreach (string orgLocation in query.OrgLocationFilter)
            {
                refinersOrgLocation.Add(new SolrQueryByField("orgLocation", orgLocation));
            }

            foreach (string statusFilter in query.StatusFilter)
            {
                refinersStatus.Add(new SolrQueryByField("status", statusFilter));
            }


            if (refinersProductType.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersProductType, "OR")); //OR
            }


            if (refinersOrgLocation.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersOrgLocation, "OR")); //OR
            }


            if (refinersStatus.Count > 0)
            {
                filters.Add(new SolrMultipleCriteriaQuery(refinersStatus, "OR")); //OR
            }



            return filters;
        }

        internal FacetParameters BuildFacets()
        {
            return new FacetParameters
            {
                Queries = new List<ISolrFacetQuery>
                {
                     new SolrFacetFieldQuery("productType"){Limit=5},
                     new SolrFacetFieldQuery("status"),
                     new SolrFacetFieldQuery("orgLocation") { Limit=5},
                     new SolrFacetDateQuery("createDate",DateTime.Now.AddYears(-10),DateTime.Now,"+1YEAR") 
                }
            };
        }
    }
}
