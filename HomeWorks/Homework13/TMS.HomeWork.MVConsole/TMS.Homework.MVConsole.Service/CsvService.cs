using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TMS.Homework.MVConsole.Service
{
    public class CsvService
    {
        private readonly string _rootPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MVConsole");

        void Persist<T>(IEnumerable<T> models)
        {
            var path = Path.Combine(_rootPath, DateTime.Now.ToString());
            using (var fs = new FileStream(path, FileMode.Create))
            {
                foreach(var model in models)
                {
                    fs.Write(Encoding.UTF8.GetBytes(model.ToString()));
                }
            }
        }
    }
}
