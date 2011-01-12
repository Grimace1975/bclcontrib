using System.Reflection;
using MongoDB.Configuration.Builders;
namespace System
{
    public static class MongoBuilder
    {
        private static readonly MethodInfo _mapMethod = typeof(MappingStoreBuilder).GetMethod("Map", Type.EmptyTypes);

        public static void MapType(Type type, MappingStoreBuilder mapping)
        {
            _mapMethod.MakeGenericMethod(type)
                .Invoke(mapping, new object[] { });
        }
    }
}