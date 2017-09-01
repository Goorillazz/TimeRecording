using DataReader;
using System.IO;
using System.Linq;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {            
            var lines = File.ReadAllLines(args[0]).Skip(1);
            var data = lines.Select(Reader.ReadData);
        }

    }
}
