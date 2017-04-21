using Indexer.Models;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indexer;
using Indexer.Workflows;

namespace UIConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            ////to delete indexed documents before starting run the following command from a browser
            ////http://localhost:8983/solr/EHS/update?stream.body=<delete><query>*:*</query></delete>&commit=true


            Startup.Init<EHSDoc>("http://localhost:8983/solr/EHS");

            List<IWorkFlow> DocWorkFlows = new List<IWorkFlow>()
            {
                new AuditMappingWorkFlow(),
                new SmartFormMappingWorkflow(), 
                new TaskMappingWorkflow()
            };

            foreach (var workflow in DocWorkFlows)
            {
                workflow.Execute();
            }
             
        }
    }
}

