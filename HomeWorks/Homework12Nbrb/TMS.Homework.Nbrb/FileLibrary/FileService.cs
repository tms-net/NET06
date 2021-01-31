using System;

namespace FileLibrary
{
    public abstract class FileService<T>
    {
        protected string _path;

        public FileService(string path)
        {
            _path = path;
        }

        public abstract void Save(T data);
        public abstract T Load();
    }
}
