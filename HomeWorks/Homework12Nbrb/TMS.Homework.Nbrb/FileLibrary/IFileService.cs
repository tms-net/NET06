using System;
using System.Threading.Tasks;

namespace FileLibrary
{
    public interface IFileService
    {
        Task SaveAsync<T>(string path, T data);
        Task<T> LoadAsync<t>(string path);
    }
}