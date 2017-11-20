using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public interface ICRUD
    {
        #region records

        bool Create(DateTime day, int taskId, int minutes);
        bool Read(DateTime day, int taskId, out (DateTime day, int taskId, int minutes) value);
        IEnumerable<(DateTime day, int taskId, int minutes)> ReadAllRecords();
        bool Update(DateTime day, int taskId, int minutes);
        bool Delete(DateTime day, int taskId);

        #endregion

        #region names

        bool Create(int taskId, string taskName);        
        bool Read(int taskId, out (int id, string taskName) value);
        IEnumerable<(int taskId, string taskName)> ReadAllNames();
        bool Update(int taskId, string taskName);
        bool Delete(int taskId);

        #endregion

        void Open();
        void Close();
    }
}
