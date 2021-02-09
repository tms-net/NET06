using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TMS.Homework.MVConsole.Service
{
    public class CsvService : ICsvService
    {
        /// <summary>
        /// Method that get`s solution root folder
        /// </summary>
        /// <param name="path">Let it be null if you are looking for your current solustion path</param>
        /// <returns>Solution path</returns>
        public DirectoryInfo GetSolutionRootFolder(string path = null)
        {
            var solutionPath = new DirectoryInfo(path ?? Directory.GetCurrentDirectory());
            while (solutionPath != null && !solutionPath.GetFiles("*.sln").Any())
            {
                solutionPath = solutionPath.Parent;
            }
            return solutionPath;
        }

        /// <summary>
        /// Method to save your IEnumarable models to CSV file.
        /// </summary>
        /// <typeparam name="T">Model`s type</typeparam>
        /// <param name="models">IEnumerable models</param>
        public void SaveToCSV<T>(IEnumerable<T> models)
        {
            var path = GetSolutionRootFolder() + $"\\{DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.csv";

            // TODO: otimize it
            if (File.Exists(path))
                Thread.Sleep(1000);

            SaveToCSV(models, path);
        }
        
        /// <summary>
        /// Method to save your IEnumarable models to CSV file.
        /// </summary>
        /// <typeparam name="T">Model`s type</typeparam>
        /// <param name="models">IEnumerable models</param>
        /// <param name="path">Path to file</param>
        public void SaveToCSV<T>(IEnumerable<T> models, string path)
        {

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                var header = string.Join(",", properties.Select(p => p.Name));

                //for (int i = 0; i < properties.Length; i++)
                //{
                //    fileStream.Write(Encoding.UTF8.GetBytes($"{properties[i].Name}"));
                //    if (i != properties.Length - 1)
                //        fileStream.Write(Encoding.UTF8.GetBytes(","));
                //}

                fileStream.Write(Encoding.UTF8.GetBytes(header));
                fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));

                // Student {
                //      public DateTime BirthDay { get; set; }
                // }

                foreach (var m in models)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var propValue = (m.GetType()
                            .GetProperty(properties[i].Name)
                            .GetValue(m, null)?
                                .ToString());

                        propValue ??= string.Empty;

                        fileStream.Write(Encoding.UTF8.GetBytes(propValue));
                        if (i != properties.Length - 1)
                            fileStream.Write(Encoding.UTF8.GetBytes(","));
                    }
                    fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));
                }
            }
        }

        /// <summary>
        /// Method to save your IEnumarable models to CSV file.
        /// </summary>
        /// <typeparam name="T">Model`s type</typeparam>
        /// <param name="models">IEnumerable models</param>
        /// <param name="path">Path to file</param>
        /// <param name="fileName">Specific .csv file name</param>
        /// <exception cref="e">File already exists</exception>
        public void SaveToCSV<T>(IEnumerable<T> models, string path, string fileName)
        {
            path = path + $"\\{fileName}.csv";
            var e = new Exception("Файл с таким именем уже существует!");
            if (File.Exists(path))
                throw e;

            SaveToCSV(models, path);
        }

    }
}
