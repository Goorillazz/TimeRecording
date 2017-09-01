using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class Reader
    {


        public static (string day, string time, string taskId) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));
            return (words.ElementAt(0), words.ElementAt(1), words.ElementAt(2));
        }
    }
}
