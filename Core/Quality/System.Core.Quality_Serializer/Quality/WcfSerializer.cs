using System.IO;
using System.Text;
namespace System.Quality
{
    public class WcfSerializer : ISerializer
    {
        public T ReadObject<T>(Type type, Stream s)
            where T : class
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (s == null)
                throw new ArgumentNullException("s");
            var serializer = new System.Runtime.Serialization.DataContractSerializer(type);
            return (serializer.ReadObject(s) as T);
        }

        public void WriteObject<T>(Type type, Stream s, T graph)
            where T : class
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (s == null)
                throw new ArgumentNullException("s");
            var serializer = new System.Runtime.Serialization.DataContractSerializer(type);
            serializer.WriteObject(s, graph);
        }
    }
}