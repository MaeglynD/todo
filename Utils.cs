using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace todo
{
    class Utils : Program
    {
        public static string[] Args { get; set; }

        // Returns the value of a given flag
        public static string GetFlag(string option)
        {
            return Args
                .SkipWhile(i => i != $"-{option.ToLower()}")
                .Skip(1)
                .Take(1)
                .FirstOrDefault() ?? "";
        }

        // Returns an array of Id's from a given string
        public static int[] GetIds(string Flag)
        {
            return Array.ConvertAll(
                GetFlag(Flag).Split(','),
                s => Int32.TryParse(s, out var x) ? x : -1
            );
        }

        // Saves the current todo list to file
        public static void SaveTodoList()
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(TodoList));
        }

        // Logs helper text
        public static void Help()
        {
            Console.WriteLine(@"

Add Item:
    -add -title ""TASK TITLE"" -due ""20/08/2020"" -completed ""TRUE""

Edit items using ID's:
    -edit ""1, 3, 5"" -title ""TASK TITLE"" -due ""20/08/2020"" -completed ""FALSE""

            
Delete items using ID's:
    -delete ""1, 3, 5""
            
Sort table using ID's:
    -sort -id         -desc
          -title
          -due
          -completed
            
            ");
        }

        // Evaluates x in relation to y according to a given (string) operator
        public static Boolean IntOperator(string logic, int x, int y)
        {
            switch (logic)
            {
                case ">": return x > y;
                case "<": return x < y;
                case ">=": case "=>": return x >= y;
                case "=<": case "<=": return x <= y;
                case "==": case "=": return x == y;
                case "!=": case "=!": return x != y;
                default: throw new Exception("invalid logic");
            }
        }

        // Evals same as above, but for strings
        public static Boolean StrOperator(string logic, string x, string y)
        {
            switch (logic)
            {
                case "==": case "=": return x == y;
                case "!=": case "=!": return x != y;
                default: throw new Exception("invalid logic");
            }
        }

        // Checks for relevant flags, returns a row with said flag values in
        public static TodoRow AddFlagValues(TodoRow Row)
        {
            bool Title = Args.Contains("-title");
            string Due = GetFlag("Due");
            string Completed = GetFlag("Completed");

            if (Title)
                Row.Title = GetFlag("Title");

            if (!string.IsNullOrWhiteSpace(Due))
                Row.Due = Due;

            if (!string.IsNullOrWhiteSpace(Completed))
            {
                bool.TryParse(Completed, out bool IsParsable);
                Row.Completed = IsParsable;
            }

            return Row;
        }
    }
}
