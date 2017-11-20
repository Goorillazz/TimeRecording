using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public class PersistanceManager
    {
        private readonly ICRUD _iCRUD;

        public PersistanceManager(ICRUD iCRUD)
        {
            _iCRUD = iCRUD;
        }

        public bool Update(IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> records)
        {
            bool result = true;
            _iCRUD.Open();
            foreach (var record in records)
            {
                if (!_iCRUD.Update(record.taskId, record.taskName))
                    result = false;
                if (!_iCRUD.Update(record.day, record.taskId, record.minutes))
                    result = false;
            }
            _iCRUD.Close();
            return result;
        }

        public IEnumerable<(DateTime day, int minutes, int taskId, string taskName)> ReadAllRecords()
        {
            _iCRUD.Open();

            var names = _iCRUD.ReadAllNames();
            foreach (var record in _iCRUD.ReadAllRecords())
            {
                yield return (record.day, record.minutes, record.taskId, names.Where(i => i.taskId == record.taskId).FirstOrDefault().taskName);
            }

            _iCRUD.Close();

        }
    }
}
