using System;

namespace FileLibrary
{
    public class FileService : IFileService
    {
        protected string _path;

        public FileService(string path)
        {
            _path = path;
        }

        public void Save<T>(T data)
        {

        }

        public T Load<T>()
        {
            return default(T);
        }
    }
}
