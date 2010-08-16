using System.Reflection;
using System.Collections.Generic;
#if !SqlServer
using System.Linq;
#endif
namespace System
{
    public static class TypeExtensions
    {
        public static Type GetTypeForQuery(this Type type, string methodWithReturnType)
        {
            var method = type.GetMethod(methodWithReturnType);
            if (method == null)
                throw new InvalidOperationException(string.Format("Unable: {1}", methodWithReturnType));
            return method.ReturnType;
        }

        public static MethodInfo GetGenericMethod(this Type type, string name) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, new Type[] { }, new Type[] { }); }
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] genericTypes, Type[] types) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, genericTypes, types); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name) { return GetGenericMethod(type, bindingAttr, name, new Type[] { }, new Type[] { }); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name, Type[] genericTypes, Type[] types)
        {
#if !SqlServer
            var genericMethod = type.GetMethods(bindingAttr)
                .Where(m => m.IsGenericMethod)
                .Where(m => (m.ContainsGenericParameters) && (m.Name == name))
                .Where(m => ((genericTypes == null) && (!m.GetGenericArguments().Any())) || ((genericTypes != null) && ((genericTypes.Length == 0) || (m.GetGenericArguments().Single().GetGenericParameterConstraints().Match(genericTypes, true)))))
                .Where(m => ((types == null) && (!m.GetParameters().Any())) || ((types != null) && ((types.Length == 0) || (m.GetParameters().Select(c => c.ParameterType).Match(types, ParameterMatchPredicate, true)))))
                .SingleOrDefault();
            return genericMethod.GetGenericMethodDefinition();
#else
            throw new NotImplementedException();
#endif
        }

        private static bool ParameterMatchPredicate(Type left, Type right)
        {
            return (left.ToString() == right.ToString());
        }

        public static Type GetEnumerableElementType(Type type)
        {
            var enumerable = FindIEnumerable(type);
            return (enumerable == null ? type : enumerable.GetGenericArguments()[0]);
        }

        private static Type FindIEnumerable(Type type)
        {
            if ((type == null) || (type == CoreEx.StringType))
                return null;
            if (type.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());
            if (type.IsGenericType)
                foreach (var argument in type.GetGenericArguments())
                {
                    var enumerable = typeof(IEnumerable<>).MakeGenericType(argument);
                    if (enumerable.IsAssignableFrom(type))
                        return enumerable;
                }
            var interfaces = type.GetInterfaces();
            if (interfaces.Length > 0)
                foreach (var @interface in interfaces)
                {
                    var enumerable = FindIEnumerable(@interface);
                    if (enumerable != null)
                        return enumerable;
                }
            return ((type.BaseType != null) && (type.BaseType != CoreEx.ObjectType) ? FindIEnumerable(type.BaseType) : null);
        }
    }
}