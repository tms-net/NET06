using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
        public void SaveToCSV<T> (IEnumerable<T> models, string path = null)
        {

        }
    }
}
