using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class Reporter
    {
        public static double GetDifference(DateTime day, int minutes)
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                return minutes / 60.0;

            return minutes / 60.0 - 8.0;
        }

        public static IEnumerable<(DateTime day, int minutes)> GetByDay(IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> records)
        {
            var dayRecords = records.GroupBy(_ => _.day);
            foreach (var item in dayRecords)
            {
                var sum = item.Select(_ => _.minutes).Sum();
                yield return (item.Key, sum);
            }
        }

        public static IEnumerable<(DateTime day, int minutes)> GetByDay(IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> records, Func<int,bool> validator)
        {
            return GetByDay(records.Where(_ => validator(_.taskId)));
        }            
    }
}
