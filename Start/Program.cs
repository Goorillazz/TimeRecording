using System;
using DataReader;
using System.IO;
using System.Linq;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = Reader.CreateReader(Properties.Settings.Default.Ungültig);
            var path = Properties.Settings.Default.Path;
            var lines = File.ReadAllLines(path).Skip(1);
            var data = lines.Select(Reader.ReadData).Where(l => reader.IstGültig(l.taskId));

            var tagesArbeitszeit = data.GroupBy(e => e.day);

            double aktuellerStand = 0;

            foreach (var item in tagesArbeitszeit)
            {
                int sum = 0;

                foreach (var sdf in item)
                {
                    sum += sdf.minutes;
                }
                var sdfsdf = item.FirstOrDefault();

                var stunden = sum / 60.0;
                var differenz = Reader.GetDifferenz(sdfsdf.day, sum);

                aktuellerStand += differenz;

                System.Console.WriteLine($"Am {sdfsdf.day.Day}.{sdfsdf.day.Month}.{sdfsdf.day.Year} wurden {stunden} gearbeitet. Differenz: {differenz}");
            }
            Console.WriteLine();
            Console.WriteLine($"Insgesamt {aktuellerStand} Überstunden.");
            System.Console.ReadLine();
        }

    }
}
