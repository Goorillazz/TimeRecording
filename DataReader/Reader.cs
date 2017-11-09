using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class Reader
    {        
        public static IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> ReadAllLines(string path)
        {
            var lines = File.ReadAllLines(path).Skip(1);
            return lines.Select(ReadData);
        }

        private static (DateTime day, int minutes, int taskId, string taskName) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));

            var calenderDay = GetCalenderDay(words.ElementAt(0));
            var minutes = Convert.ToInt32(words.ElementAt(1));
            var taskID = Convert.ToInt32(words.ElementAt(2));
            var taskTitle = words.ElementAt(3);

            return (calenderDay, minutes, taskID, taskTitle);
        }

        private static DateTime GetCalenderDay(string dateStr)
        {
            var date = dateStr.Split('.');
            int year = Convert.ToInt32(date.ElementAt(2).Substring(0, 4));
            int month = Convert.ToInt32(date.ElementAt(1));
            int day = Convert.ToInt32(date.ElementAt(0));
            return new DateTime(year, month, day);
        }


    }
}
