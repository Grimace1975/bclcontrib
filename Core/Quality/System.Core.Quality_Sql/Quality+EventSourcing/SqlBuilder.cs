using System.Runtime.Serialization.Json;
using System.IO;
using System.Quality.EventSourcing;
using System.Text;
namespace System
{
    public static class SqlBuilder
    {
        public static T FromJson<T>(Type type, string json)
            where T : class
        {
            var serializer = new DataContractJsonSerializer(type);
            using (var s = new MemoryStream(Encoding.Default.GetBytes(json)))
                return (serializer.ReadObject(s) as T);
        }

        public static string ToJson(Type type, object value)
        {
            var serializer = new DataContractJsonSerializer(type);
            using (var s = new MemoryStream())
            {
                serializer.WriteObject(s, value);
                return Encoding.Default.GetString(s.ToArray());
            }
        }
    }
}