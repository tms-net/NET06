using System.Collections.Generic;

namespace TMS.Homework.MVConsole.Service
{
    public interface ICsvService
    {
        void SaveToCSV<T>(IEnumerable<T> models);
    }
}