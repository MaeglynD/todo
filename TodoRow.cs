using System;

namespace todo
{
    // Model for adding a table row
    public class TodoRow
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Due { get; set; } = DateTime.Now.AddDays(12).ToString("dd/MM/yyyy");
        public bool Completed { get; set; } = false;
        public long Unix()
        {
            try
            {
                return DateTime.Parse(Due).Ticks / 10000000L - 62135596800L;
            }
            catch (FormatException)
            {
                return 0;
            }
        }
    }

}
