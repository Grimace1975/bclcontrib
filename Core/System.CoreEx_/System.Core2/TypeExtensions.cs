#if !SqlServer
using System.Linq;
#endif
using System.Reflection;
namespace System
{
    public static class TypeExtensions
    {
        public static MethodInfo GetGenericMethod(this Type type, string name) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, null, null); }
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] genericTypes, Type[] types) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, genericTypes, types); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name) { return GetGenericMethod(type, bindingAttr, name, null, null); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name, Type[] genericTypes, Type[] types)
        {
#if !SqlServer
            var genericMethod = type.GetMethods(bindingAttr)
                .Where(m => m.IsGenericMethod).Single(m => (m.ContainsGenericParameters) && (m.Name == name));
            return genericMethod.GetGenericMethodDefinition();
#else
            throw new NotImplementedException();
#endif
        }
    }
}