using BetterConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace todo
{
    class Program
    {
        static void Main(string[] Args)
        {
            // For filtering data, set to false
            bool CommitSave = true;

            // Data ready to be loaded? Load it
            if (File.Exists(Path))
            {
                TodoList = JsonConvert
                    .DeserializeObject<List<TodoRow>>(File.ReadAllText(Path));
            }

            // Lowercase all the args, keep accessible to other classes
            Utils.Args = Array.ConvertAll(Args, x => x.ToLower());
                  
            // Check for arguments 
            foreach (string x in new[] { "Add", "Delete", "Edit", "Sort", "Filter" })
            {
                // Checks if flag is present and has a value
                if (!string.IsNullOrWhiteSpace(Utils.GetFlag(x)))
                {
                    // Invoke the relevant method
                    (new Flags())
                        .GetType()
                        .GetMethod(x)
                        .Invoke(null, null);

                    if (x == "Filter") 
                        CommitSave = false;

                    // Only one command per execution
                    break;
                }
            }

            // Create & configure the table
            Table TodoTable = new Table("Id", "Title", "Due", "Completed");
            TodoTable.Config = TableConfiguration.UnicodeAlt();

            // Add data to table
            foreach (TodoRow Row in TodoList)
            {
                TodoTable.AddRow(Row.Id, Row.Title, Row.Due, Row.Completed);
            }

            // Log and save
            Console.Clear();

            // Log data, if there is any
            if (TodoList.Count > 0)
                Console.Write(TodoTable.ToString());
            else
                Console.WriteLine("No data found...");

            // Log helper text
            Utils.Help();
            
            // If viewing a filter, dont save
            if (CommitSave)
                Utils.SaveTodoList();
        }

        // Path to saved list
        public static string Path = "todo.json";

        // Todo list, To be serialised and saved once all changes are committed
        public static List<TodoRow> TodoList = new List<TodoRow>();


    }
}
