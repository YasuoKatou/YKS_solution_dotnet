using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace json.util
{
    public class JsonUtil
    {
        public T paeseJson<T>(string jsonStr)
        where T : new() {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
            // return new T();
        }
    }
}