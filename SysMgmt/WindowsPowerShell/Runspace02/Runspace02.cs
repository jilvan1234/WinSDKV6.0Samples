//
// Copyright (c) 2006 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
// PARTICULAR PURPOSE.
//
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Management.Automation.Runspaces;
using System.Management.Automation;

namespace Microsoft.Samples.PowerShell.Runspaces
{
    class Runspace02
    {
        static void CreateForm()
        {
            Form form = new Form();
            DataGridView grid = new DataGridView();
            form.Controls.Add(grid);
            grid.Dock = DockStyle.Fill;

            // Create an instance of the RunspaceInvoke class.
            // This takes care of all building all of the other
            // data structures needed...
            RunspaceInvoke invoker = new RunspaceInvoke();

            Collection<PSObject> results = invoker.Invoke("get-process | sort-object ID");

            // The generic collection needs to be re-wrapped in an ArrayList
            // for data-binding to work...
            ArrayList objects = new ArrayList();
            objects.AddRange(results);

            // The DataGridView will use the PSObjectTypeDescriptor type
            // to retrieve the properties.
            grid.DataSource = objects;

            form.ShowDialog();
        }

        /// <summary>
        /// This sample uses the RunspaceInvoke class to execute
        /// the get-process cmdlet synchronously. Windows Forms and data
        /// binding are then used to display the results in a
        /// DataGridView control.
        /// </summary>
        /// <param name="args">Unused</param>
        /// <remarks>
        /// This sample demonstrates the following:
        /// 1. Creating an instance of the RunspaceInvoke class.
        /// 2. Using this instance to invoke a PowerShell command.
        /// 3. Using the output of RunspaceInvoke in a DataGridView
        ///    in a Windows Forms application 
        /// </remarks
        static void Main(string[] args)
        {
            Runspace02.CreateForm();
        }
    }
}




