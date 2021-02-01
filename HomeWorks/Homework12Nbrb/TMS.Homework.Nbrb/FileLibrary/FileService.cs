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

        public void Save<T>(T data) { }
        public T Load<T>()
        {
            return default(T);
        }

        public void Save(T data)
        {
            throw new NotImplementedException();
        }

        public T Load()
        {
            throw new NotImplementedException();
        }
    }
}
