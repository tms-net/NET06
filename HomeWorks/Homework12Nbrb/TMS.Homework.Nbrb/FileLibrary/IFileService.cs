namespace FileLibrary
{
    public interface IFileService
    {
        void Save<T>(T data);
        T Load<T>();
    }
}