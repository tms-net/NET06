using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TMS.Homework.MVConsole.Service
{
    public class CsvService
    {
        private readonly string _rootPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MVConsole");

        public void Persist<T>(IEnumerable<T> models)
        {
            var path = Path.Combine(_rootPath, DateTime.Now.ToString());
            using (var fs = new FileStream(path, FileMode.Create))
            {
                foreach (var model in models)
                {
                    fs.Write(Encoding.UTF8.GetBytes(model.ToString()));
                }
            }
        }

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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        public void SaveToCSV<T>(IEnumerable<T> models)
        {
            var path = GetSolutionRootFolder() + $"\\{DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.csv";
            if (File.Exists(path))
                Thread.Sleep(1000);
            path = GetSolutionRootFolder() + $"\\{DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.csv";
            var properties = typeof(T).GetProperties();
            var stringBuilder = new StringBuilder();
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    fileStream.Write(Encoding.UTF8.GetBytes($"{properties[i].Name}"));
                    if (i != properties.Length - 1)
                        fileStream.Write(Encoding.UTF8.GetBytes(","));
                }
                fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));

                foreach (var m in models)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        fileStream.Write(Encoding.UTF8.GetBytes($"{m.GetType().GetProperty(properties[i].Name).GetValue(m, null)}"));
                        if (i != properties.Length - 1)
                            fileStream.Write(Encoding.UTF8.GetBytes(","));
                    }
                    fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <param name="path"></param>
        public void SaveToCSV<T>(IEnumerable<T> models, string path)
        {
            var pathToSave = path + $"\\{DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.csv";
            if (File.Exists(pathToSave))
                Thread.Sleep(1000);
            pathToSave = null;
            pathToSave = path + $"\\{DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.csv";
            var properties = typeof(T).GetProperties();
            var stringBuilder = new StringBuilder();
            using (var fileStream = new FileStream(pathToSave, FileMode.Create))
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    fileStream.Write(Encoding.UTF8.GetBytes($"{properties[i].Name}"));
                    if (i != properties.Length - 1)
                        fileStream.Write(Encoding.UTF8.GetBytes(","));
                }
                fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));

                foreach (var m in models)
                {
                    for(int i = 0; i < properties.Length; i++)
                    {
                        fileStream.Write(Encoding.UTF8.GetBytes($"{m.GetType().GetProperty(properties[i].Name).GetValue(m, null)}"));
                        if (i != properties.Length-1)
                            fileStream.Write(Encoding.UTF8.GetBytes(","));
                    }
                    fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <param name="path"></param>
        public void SaveToCSV<T>(IEnumerable<T> models, string path, string fileName)
        {
            path = path + $"\\{fileName}.csv";
            
            if(File.Exists(path))
                throw new Exception("Файл с таким именем уже существует!");
            
            var properties = typeof(T).GetProperties();
            var stringBuilder = new StringBuilder();
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    fileStream.Write(Encoding.UTF8.GetBytes($"{properties[i].Name}"));
                    if (i != properties.Length - 1)
                        fileStream.Write(Encoding.UTF8.GetBytes(","));
                }
                fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));

                foreach (var m in models)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        fileStream.Write(Encoding.UTF8.GetBytes($"{m.GetType().GetProperty(properties[i].Name).GetValue(m, null)}"));
                        if (i != properties.Length - 1)
                            fileStream.Write(Encoding.UTF8.GetBytes(","));
                    }
                    fileStream.Write(Encoding.UTF8.GetBytes("\r\n"));
                }
            }
        }

    }
    public class Foo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}
