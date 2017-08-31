using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(args[0]).Skip(1);
            var data = lines.Select(l => ReadData(l));
        }

        static (string day, string time, string taskId) ReadData(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));
            return (words.ElementAt(0), words.ElementAt(1), words.ElementAt(2));
        }
    }
}
