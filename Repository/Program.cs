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

            //Notes
            //create start menu with
            //1. add
            //2. update
            //3. delete

            //Add activates the mapping workflow for all document and 
            //runs query to retrieve all data.
            //Update needs to find one record for indexing.
                //Use the same mapping code.
                //Could reuse all existing code by overloading 
                //with id. 

            //*can Add() ienumerable..ie bulk add is available
            

            List<IWorkFlow> DocWorkFlows = new List<IWorkFlow>()
            {
                new AuditMappingWorkFlow(),
                new SmartFormMappingWorkflow(), 
                new TaskMappingWorkflow()
            };

            foreach (var workflow in DocWorkFlows)
            {
                //Refactor
                //1. Solr operations from workflows to here
                //2. Execute returns collection of EHS Doc
                //3. Solr Add
                //4. Solr Commmit
                workflow.Execute();
            }
             
        }
    }
}

