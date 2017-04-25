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

        internal FacetParameters BuildFacets()
        {
            return new FacetParameters
            {
                Queries = new List<ISolrFacetQuery>
                {
                     new SolrFacetFieldQuery("productType"){Limit=5},
                     new SolrFacetFieldQuery("status") {Limit=5},
                     new SolrFacetFieldQuery("orgLocation") { Limit=5},
                     new SolrFacetDateQuery("createDate",DateTime.Now.AddYears(-10),DateTime.Now,"+1MONTH") 
                }
            };
        }
    }
}
