using System;
using System.Linq;

namespace todo
{
    class Flags : Program
    {
        public static void Add()
        {
            // Initialize new row
            TodoRow Row = new TodoRow
            {
                // Assign unique ID
                Id = TodoList
                        .Select(x => x.Id)
                        .DefaultIfEmpty(0)
                        .Max() + 1,
            };

            // Add flag values, add row to table
            Row = Utils.AddFlagValues(Row);
            TodoList.Add(Row);
        }

        public static void Edit()
        {
            // Convert user's id list into an array of ints
            int[] Ids = Utils.GetIds("Edit");

            // Find row by id in the data, change relevant flag values
            foreach(int Id in Ids)
            {
                TodoRow Row = TodoList.Find(x => x.Id == Id);

                if (Row != null)
                    Row = Utils.AddFlagValues(Row);
            }
        }

        public static void Sort()
        {
            // Get the -sort flag value
            string Flag = Utils.GetFlag("Sort");

            // Convert from '-title' to 'Title'
            string FlagCorrected = $"{Char.ToUpper(Flag[1])}{Flag.Substring(2)}";

            // If its not a valid header, return
            if (!new[] { "Id", "Title", "Due", "Completed" }.Contains(FlagCorrected)) 
                return;

            // Sort
            TodoList = TodoList
                .OrderBy(x => 
                    FlagCorrected == "Due" ? x.Unix() : x
                        .GetType()
                        .GetProperty(FlagCorrected)
                        .GetValue(x, null))
                .ToList();

            // Reverse if descending
            if (Utils.Args.Contains("-desc")) 
                TodoList.Reverse();
        }

        public static void Filter() {}

        public static void Delete()
        {
            int[] Ids = Utils.GetIds("Delete");

            TodoList = TodoList
                .Where(x => !Ids.Contains(x.Id))
                .ToList();
        }
    }
}