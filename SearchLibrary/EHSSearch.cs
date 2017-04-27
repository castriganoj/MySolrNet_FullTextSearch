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
        //private Connection connection;

        public EHSSearch()
        {
            //Initialize connection
            //Connection can be initialized once and then retrieved via ServiceLocator.GetInstance
            //But we are creating it for every search library instantiation for demo purposes            
            //connection = new Connection("http://localhost:8983/solr/EHS");
        }

        public QueryResponse DoSearch(EhsQuery query)
        {
            FiltersFacets filtersFacets = new FiltersFacets();

            //Create an object to hold results
            SolrQueryResults<EHSDoc> solrResults;
            QueryResponse queryResponse = new QueryResponse();
            
            //Echo back the original query 
            queryResponse.QueryText = query;          

            //Get a connection
            //Connection will be in web app startup, for now
            //move initalized check into static getter check
            //on SolrOperations field. 
            bool initialized = Connection.Initialized;
            ISolrOperations<EHSDoc> solr = Connection.SolrOperations;
            QueryOptions queryOptions = new QueryOptions
            {
                Rows = query.Rows,
                Facet = filtersFacets.BuildFacets(), 
                FilterQueries = filtersFacets.BuildFilterQueries(query)
            };

            //Execute the query
            ISolrQuery solrQuery = new SolrQuery(query.QueryText);

            //solrResults = solr.Query(solrQuery, queryOptions);
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
