using Microsoft.Practices.ServiceLocation;
using SearchLibrary.Models;
using SearchLibrary.Models.EHS;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.SolrUtilities
{
    internal static class Connection
    {

        private static string coreUrl = "http://localhost:8983/solr/EHS";

        public static ISolrOperations<EHSDoc> SolrOperations;

        private static bool initialized;
        public static bool Initialized
        {
            get
            {
                if(!initialized)
                {
                    InitializeConnection(coreUrl);
                    initialized = true;
                }
                return initialized;
            } 
        }

       
        private static void InitializeConnection(string CoreUrl)
        {
            Startup.Init<EHSDoc>(CoreUrl);
            GetSolrInstance();
        }

        internal static void GetSolrInstance()
        {
            SolrOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EHSDoc>>();
        }
    }
}
