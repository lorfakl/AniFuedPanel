using System;
using System.IO;
using Newtonsoft.Json;

namespace Utilities
{
    

    public static class JsonHelper
    {
        public static T ParseJson<T>(string filePath) where T : class
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file was not found", filePath);
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                T result = JsonConvert.DeserializeObject<T>(jsonContent);
                return result ?? throw new InvalidOperationException("Deserialization resulted in a null object");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to parse JSON file", ex);
            }
        }
    }

}

