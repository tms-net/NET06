using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileLibrary
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Serializes object to json and save to specified file
        /// </summary>
        /// <typeparam name="T">Serializable object</typeparam>
        /// <param name="path">The path to the file</param>
        /// <param name="data">The object that will be written to the file</param>
        /// <returns>Task</returns>
        public async Task SaveAsync<T>(string path, T data)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, data);
                Console.WriteLine("Data has been saved to file");
            }
        }

        /// <summary>
        /// The object is deserialized from the file
        /// </summary>
        /// <typeparam name="T">Deserializable object</typeparam>
        /// <param name="path">The path to the file in which the object is written</param>
        /// <returns>Object</returns>
        public async Task<T> LoadAsync<T>(string path)
        {
            T loadedData;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                loadedData = await JsonSerializer.DeserializeAsync<T>(fs);
            }

            return loadedData;
        }
    }
}
