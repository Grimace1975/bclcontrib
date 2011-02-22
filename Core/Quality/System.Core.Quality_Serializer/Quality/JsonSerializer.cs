using System.IO;
using System.Text;
namespace System.Quality
{
    public class JsonSerializer : ISerializer
    {
        public T ReadObject<T>(Type type, Stream s)
            where T : class
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (s == null)
                throw new ArgumentNullException("s");
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(type);
            return (serializer.ReadObject(s) as T);
        }

        public void WriteObject<T>(Type type, Stream s, T graph)
            where T : class
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (s == null)
                throw new ArgumentNullException("s");
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(type);
            serializer.WriteObject(s, graph);
        }
    }
}