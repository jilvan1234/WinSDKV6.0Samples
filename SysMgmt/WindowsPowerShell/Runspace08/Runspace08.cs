//
// Copyright (c) 2006 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
// PARTICULAR PURPOSE.
//
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;

namespace Microsoft.Samples.PowerShell.Runspaces
{
    class Runspace08
    {
        /// <summary>
        /// This sample uses the Runspace and Pipeline classes directly. It builds
        /// a pipeline that executes the get-process cmdlet piped into sort-object.
        /// The Command class is used to illustrate adding parameters to a command.
        /// </summary>
        /// <param name="args">Unused</param>
        /// <remarks>
        /// 1. Using the RunspaceFactory class to create a runspace.
        /// 2. Creating a Pipeline object
        /// 3. Adding individual commands to that runspace
        /// 4. Adding parameters to the commands before adding them to the pipeline
        /// 5. Synchronously invoking the constructed pipeline.
        /// 6. Working with PSObject to extract properties from the objects returned.
        /// </remarks>
        static void Main(string[] args)
        {
            Collection<PSObject> results; // Holds the result of the pipeline execution.

            // Create a runspace. We can't use the RunspaceInvoke class this time
            // because we need to get at the underlying runspace to explicitly
            // add the commands.
            // (Note that no PSHost instance is supplied in the constructor so the
            // default PSHost implementation is used. See the Hosting topics for
            // more information on creating your own PSHost class.)

            Runspace myRunSpace = RunspaceFactory.CreateRunspace();
            myRunSpace.Open();

            // Create a pipeline...
            Pipeline pipeLine = myRunSpace.CreatePipeline();

            // Use the using statement so we dispose of the Pipeline object
            // when we're done.

            using (pipeLine)
            {
                // Add the 'get-process' cmdlet(note that this is just the name
                // of a command, not a script.
                pipeLine.Commands.Add("get-process");

                // Create a command object so we can set some parameters
                // for this command.
                Command sort = new Command("sort-object");
                // Sort in descending order...
                sort.Parameters.Add("descending", true);
                // By handlecount...
                sort.Parameters.Add("property", "handlecount");

                // Add the sort command we've constructed
                pipeLine.Commands.Add(sort);

                // Execute the pipeline and save the objects returned.
                results = pipeLine.Invoke();
            }
            // Even after disposing of the pipeLine, we still need to set the
            // pipeLine variable to null so the garbage collector can clean it up.
            pipeLine = null;

            // Display the results of the execution 

            Console.WriteLine("Process              HandleCount");
            Console.WriteLine("--------------------------------");

            // Print out each result object...
            foreach (PSObject result in results)
            {
                Console.WriteLine("{0,-20} {1}",
                    result.Members["ProcessName"].Value,
                    result.Members["HandleCount"].Value);
            }

            // Finally close the runspace and set all variables to null to free
            // up any resources.

            myRunSpace.Close();
            myRunSpace = null;

            System.Console.WriteLine("Hit any key to exit...");
            System.Console.ReadKey();
        }
    }
}

