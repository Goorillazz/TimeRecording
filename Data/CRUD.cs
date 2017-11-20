using DataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CRUD : ICRUD
    {
        Dictionary<int, string> _nameTable = new Dictionary<int, string>();
        Dictionary<(DateTime day, int taskId), int> _recordTable = new Dictionary<(DateTime day, int taskId), int>();
        string _names = @"..\..\names.txt";
        string _records = @"..\..\records.txt";

        public void Open()
        {
            OpenNames();
            OpenRecord();
        }

        private void OpenNames()
        {
            _nameTable.Clear();
            if (!File.Exists(_names)) { File.Create(_names); return; }

            foreach (var item in File.ReadAllLines(_names).Select(CreateNameTuble))
            {
                _nameTable.Add(item.id, item.name);
            }
        }

        private void CloseName()
        {
            File.WriteAllLines(_names, _nameTable.Select(i => $"{i.Key},{i.Value}"));
        }

        private void OpenRecord()
        {
            _recordTable.Clear();
            if (!File.Exists(_records)) { File.Create(_records); return; }

            foreach (var item in File.ReadAllLines(_records).Select(CreateRecordTuble))
            {
                _recordTable.Add((item.day, item.taskId), item.minutes);
            }
        }

        private void CloseRecord()
        {
            File.WriteAllLines(_records, _recordTable.Select(i => $"{i.Key.day.Day}.{i.Key.day.Month}.{i.Key.day.Year},{i.Key.taskId},{i.Value}"));
        }

        (int id, string name) CreateNameTuble(string line)
        {
            var idAndName = line.Split(',');
            return (Convert.ToInt32(idAndName.ElementAt(0)), idAndName.ElementAt(1));
        }

        (DateTime day, int taskId, int minutes) CreateRecordTuble(string line)
        {
            var words = line.Split(',').Select(w => w.Replace("\"", string.Empty));

            var calenderDay = GetCalenderDay(words.ElementAt(0));
            var taskID = Convert.ToInt32(words.ElementAt(1));
            var minutes = Convert.ToInt32(words.ElementAt(2));

            return (calenderDay, taskID, minutes);
        }

        private static DateTime GetCalenderDay(string dateStr)
        {
            var date = dateStr.Split('.');
            int year = Convert.ToInt32(date.ElementAt(2).Substring(0, 4));
            int month = Convert.ToInt32(date.ElementAt(1));
            int day = Convert.ToInt32(date.ElementAt(0));
            return new DateTime(year, month, day);
        }

        public void Close()
        {
            CloseName();
            CloseRecord();
        }

        #region records

        public bool Create(DateTime day, int taskId, int minutes)
        {
            (DateTime day, int taskId) key = (day, taskId);
            if (_recordTable.ContainsKey(key))
                return false;

            _recordTable.Add(key, minutes);
            return true;
        }

        public bool Update(DateTime day, int taskId, int minutes)
        {
            (DateTime day, int taskId) key = (day, taskId);
            if (!_recordTable.ContainsKey(key))
                return Create(day, taskId, minutes);

            _recordTable[key] = minutes;
            return true;
        }

        public bool Read(DateTime day, int taskId, out (DateTime day, int taskId, int minutes) value)
        {
            (DateTime day, int taskId) key = (day, taskId);
            if (_recordTable.TryGetValue(key, out int minutes))
            {
                value = (day, taskId, minutes);
                return true;
            }

            value = (DateTime.MinValue, -1, -1);
            return false;
        }

        public IEnumerable<(DateTime day, int taskId, int minutes)> ReadAllRecords()
        {
            foreach (var record in _recordTable)
            {
                yield return (record.Key.day, record.Key.taskId, record.Value);
            }
        }

        public bool Delete(DateTime day, int taskId)
        {
            (DateTime day, int taskId) key = (day, taskId);
            if (!_recordTable.ContainsKey(key))
                return false;
            _recordTable.Remove(key);
            return true;
        }

        #endregion

        #region names

        public bool Create(int taskId, string taskName)
        {
            if (_nameTable.ContainsKey(taskId))
                return false;

            _nameTable.Add(taskId, taskName);
            return true;
        }

        public bool Read(int taskId, out (int id,string taskName) value)
        {
            if (_nameTable.TryGetValue(taskId, out string name))
            {
                value = (taskId, name);
                return true;
            }

            value = (-1, string.Empty);
            return false;
        }

        public IEnumerable<(int taskId, string taskName)> ReadAllNames()
        {
            foreach (var name in _nameTable)
            {
                yield return (name.Key, name.Value);
            }
        }

        public bool Update(int taskId, string taskName)
        {
            if (!_nameTable.ContainsKey(taskId))
            {
                return Create(taskId, taskName);
            }

            _nameTable[taskId] = taskName;
            return true;
        }

        public bool Delete(int taskId)
        {
            if (!_nameTable.ContainsKey(taskId))
                return false;

            _nameTable.Remove(taskId);
            return true;
        }
           
        #endregion

    }
}
