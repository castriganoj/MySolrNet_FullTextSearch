
using SearchLibrary.Models.EHS;
using SearchLibrary.Models.Query;
using SolrNet;
using SolrNet.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchLibrary.SolrUtilities
{
    internal class ResponseExtraction
    {
        internal QueryResponse ExtractResponse(SolrQueryResults<EHSDoc> solrResults)
        {

            throw new NotImplementedException();
        }
        //Extract parts of the SolrNet response and set them in QueryResponse class
        internal void SetHeader(QueryResponse queryResponse, SolrQueryResults<EHSDoc> solrResults)
        {
            queryResponse.QueryTime = solrResults.Header.QTime;
            queryResponse.Status = solrResults.Header.Status;
            queryResponse.TotalHits = solrResults.NumFound;
        }

        internal void SetBody(QueryResponse queryResponse, SolrQueryResults<EHSDoc> solrResults)
        {
            queryResponse.Results = solrResults;
        }

        internal void SetFacets(QueryResponse queryResponse, SolrQueryResults<EHSDoc> solrResults)
        {
            //ProductTypes
            if (solrResults.FacetFields.ContainsKey("productType"))
            {
                queryResponse.ProductTypeFacet = solrResults.FacetFields["productType"]
                    .Select(facet => new KeyValuePair<string, int>(facet.Key, facet.Value))
                    .ToList();
            }

            //StatusTypes
            if (solrResults.FacetFields.ContainsKey("status"))
            {
                queryResponse.StatusTypeFacet = solrResults.FacetFields["status"]
                    .Select(facet => new KeyValuePair<string, int>(facet.Key, facet.Value))
                    .ToList();
            }

            //orlLocation
            if (solrResults.FacetFields.ContainsKey("orgLocation"))
            {
                queryResponse.OrgLocationFacet = solrResults.FacetFields["orgLocation"]
                    .Select(facet => new KeyValuePair<string, int>(facet.Key, facet.Value))
                    .ToList();
            }

            //createDate
            if (solrResults.FacetDates.ContainsKey("createDate"))
            {
                queryResponse.DateCreatedFacet = solrResults.FacetDates["createDate"].DateResults
                    .Select(facet => new KeyValuePair<string, int>(facet.Key.Year.ToString(), facet.Value))
                    .ToList();
            }

        }

    }
}

