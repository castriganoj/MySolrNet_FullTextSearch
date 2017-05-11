using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using SearchLibrary.SolrUtilities;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary
{
    public class EHSSearch
    {

        public QueryResponse DoSearch(EhsQuery query)
        {
            FiltersFacets filtersFacets = new FiltersFacets();
            Highlights highlights = new Highlights();
           

            //Create an object to hold results
            SolrQueryResults<EHSDoc> solrResults;
            QueryResponse queryResponse = new QueryResponse();
            
            //Echo back the original query 
            queryResponse.QueryText = query;          

            //Create method for increased readibility.
            //Move to some sort of application start or build eveuntally.
            bool initialize = Connection.Initialized;
            ISolrOperations<EHSDoc> solr = Connection.SolrOperations;
            
            QueryOptions queryOptions = new QueryOptions
            {
                Rows = query.Rows,
                Facet = filtersFacets.BuildFacets(), 
                FilterQueries = filtersFacets.BuildFilterQueries(query), 
                Highlight = highlights.BuildHighlightParameters()
            };

            //Execute the query
            ISolrQuery solrQuery = new SolrQuery(query.QueryText);

            solrResults = solr.Query(solrQuery, queryOptions);

           //Set response
            ResponseExtraction extractResponse = new ResponseExtraction();

            extractResponse.SetHeader(queryResponse, solrResults);
            extractResponse.SetBody(queryResponse, solrResults);
            extractResponse.SetFacets(queryResponse, solrResults);

            //Return response;
            return queryResponse;
        }
    }
}
