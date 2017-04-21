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
                     new SolrFacetFieldQuery("productType"){MinCount = 1},
                     new SolrFacetFieldQuery("status") {MinCount = 1 },
                     new SolrFacetFieldQuery("orgLocation") {MinCount = 2},
                     new SolrFacetFieldQuery("createDate") {MinCount = 2 }
                }
            };
        }
    }
}
