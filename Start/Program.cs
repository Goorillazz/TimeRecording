using DataReader;
using System.IO;
using System.Linq;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Properties.Settings.Default.Path;
            var lines = File.ReadAllLines(path).Skip(1);
            var data = lines.Select(Reader.ReadData);

            var tagesArbeitszeit = data.GroupBy(e => e.day);

            foreach (var item in tagesArbeitszeit)
            {
                int sum = 0;
                
                foreach(var sdf in item)
                {
                    sum = sum + sdf.minutes;
                }
                var sdfsdf = item.FirstOrDefault();

                var stunden = sum / 60.0;

                System.Console.WriteLine($"Am {sdfsdf.day.Day}.{sdfsdf.day.Month}.{sdfsdf.day.Year} wurden {stunden} gearbeitet.");
            }
            System.Console.ReadLine();
        }

    }
}
