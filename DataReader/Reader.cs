using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class Reader
    {


        public static (DateTime day, int minutes, int taskId, string taskName) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));

            var kalenderTag = NewMethod(words.ElementAt(0));
            var minuten = Convert.ToInt32(words.ElementAt(1));
            var taskID = Convert.ToInt32(words.ElementAt(2));
            var taskTitle = words.ElementAt(3);

            return (kalenderTag, minuten, taskID, taskTitle);
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
