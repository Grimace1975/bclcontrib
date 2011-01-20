using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
namespace System.Quality
{
    public class JsonSerializer : ISerializer
    {
        public T ReadObject<T>(Type type, Stream s)
            where T : class
        {
            var serializer = new DataContractJsonSerializer(type);
            return (serializer.ReadObject(s) as T);
        }

        public void WriteObject<T>(Type type, Stream s, T graph)
            where T : class
        {
            var serializer = new DataContractJsonSerializer(type);
            serializer.WriteObject(s, graph);
        }
    }
}