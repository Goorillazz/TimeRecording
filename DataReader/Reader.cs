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
        private int[] _invalidTasks;        

        private Reader(params int[] invalidTasks)
        {            
            _invalidTasks = invalidTasks;
        }

        public static Reader CreateReader(params int[] invalidTasks)
        {            
            return new Reader(invalidTasks);
        }
        
        public IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> ReadAllLines(string path)
        {
            var lines = File.ReadAllLines(path).Skip(1);
            return lines.Select(ReadData).Where(l => IsValid(l.taskId));
        }

        private (DateTime day, int minutes, int taskId, string taskName) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));

            var calenderDay = GetCalenderDay(words.ElementAt(0));
            var minutes = Convert.ToInt32(words.ElementAt(1));
            var taskID = Convert.ToInt32(words.ElementAt(2));
            var taskTitle = words.ElementAt(3);

            return (calenderDay, minutes, taskID, taskTitle);
        }

        private bool IsValid(int taskId)
        {
            return !_invalidTasks.Contains(taskId);
        }

        public double GetDifferenz(DateTime day, int minutes)
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                return minutes / 60.0;

            return minutes / 60.0 - 8.0;
        }

        private DateTime GetCalenderDay(string dateStr)
        {
            var date = dateStr.Split('.');
            int year = Convert.ToInt32(date.ElementAt(2).Substring(0, 4));
            int month = Convert.ToInt32(date.ElementAt(1));
            int day = Convert.ToInt32(date.ElementAt(0));
            return new DateTime(year, month, day);
        }


    }
}
