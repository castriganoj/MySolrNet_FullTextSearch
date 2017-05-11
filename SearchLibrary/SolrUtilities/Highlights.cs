using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchLibrary.SolrUtilities
{
    internal class Highlights
    {

        internal HighlightingParameters BuildHighlightParameters()
        {
            HighlightingParameters parameters = new HighlightingParameters()
            {
                Fields = new List<string>() { "*" },
                Fragsize = 200, 
                Snippets = 20, 
                BeforeTerm = "<span class = 'highlight'>",
                AfterTerm = "</span>"
            };;

            return parameters;
        }
    }
}
