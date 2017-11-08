using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class Reader
    {
        private readonly IEnumerable<int> _ungültigeTasks;

        private Reader(IEnumerable<int> ungültigeTasks)
        {
            _ungültigeTasks = ungültigeTasks;
        }

        public static Reader CreateReader(string ungültigeTasks)
        {
           return new Reader(ungültigeTasks.Split(',').Select(s => Convert.ToInt32(s)));
        }

        public static (DateTime day, int minutes, int taskId, string taskName) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));

            var kalenderTag = NewMethod(words.ElementAt(0));
            var minuten = Convert.ToInt32(words.ElementAt(1));
            var taskID = Convert.ToInt32(words.ElementAt(2));
            var taskTitle = words.ElementAt(3);

            return (kalenderTag, minuten, taskID, taskTitle);
        }

        public bool IstGültig(int taskId)
        {
            return !_ungültigeTasks.Contains(taskId);
        }

        public static double GetDifferenz(DateTime day, int minutes)
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                return minutes / 60.0;

            return minutes / 60.0 - 8.0;
        }

        private static DateTime NewMethod(string datumStr)
        {
            var datum = datumStr.Split('.');
            int jahr = Convert.ToInt32(datum.ElementAt(2).Substring(0, 4));
            int monat = Convert.ToInt32(datum.ElementAt(1));
            int tag = Convert.ToInt32(datum.ElementAt(0));
            var kalenderTag = new DateTime(jahr, monat, tag);
            return kalenderTag;
        }

        
    }
}
