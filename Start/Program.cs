using System;
using DataReader;
using System.IO;
using System.Linq;
using Data;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var crud = new CRUD();
            var importedData = Reader.ReadAllLines(Properties.Settings.Default.Path);

            var persistanceManager = new PersistanceManager(crud);
            persistanceManager.Update(importedData);
            var data = persistanceManager.ReadAllRecords();

            var dailyReport = Reporter.GetByDay(data, Validator);
            double sum = 0;

            foreach (var item in dailyReport)
            {       
                double difference = Reporter.GetDifference(item.day, item.minutes);
                Console.WriteLine($"Am {item.day} wurden {item.minutes/60.0} gearbeitet. Differenz: {difference}");
                sum += difference;
            }

            Console.WriteLine();
            Console.WriteLine($"Insgesamt {sum} Überstunden.");
            Console.ReadLine();
        }

        private static bool Validator(int arg)
        {
            var invalidTasks = Properties.Settings.Default.Invalid;
            var invalidTasksIds = invalidTasks.Split(',').Select(_ => Convert.ToInt32(_));
            return !invalidTasksIds.Contains(arg);
        }
    }
}
