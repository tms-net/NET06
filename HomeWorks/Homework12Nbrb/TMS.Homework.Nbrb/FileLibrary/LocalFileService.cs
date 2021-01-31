using System;
using System.IO;
using System.Text.Json;

namespace FileLibrary
{
    public class LocalFileService<T> : FileService<T>
    {
        public LocalFileService(string path) : base(path)
        {
        }

        public override async void Save(T data)
        {
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, data);
                Console.WriteLine("Data has been saved to file");
            }
        }

        public override T Load()
        {
            T loadedData;
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                loadedData = JsonSerializer.Deserialize<T>(array);
            }

            return loadedData;
        }
    }
}
