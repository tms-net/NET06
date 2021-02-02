using System;
using System.Threading.Tasks;

namespace FileLibrary
{
    public interface IFileService
    {
        Task SaveAsync<T>(T data);
        Task<T> Load<t>();
    }
}