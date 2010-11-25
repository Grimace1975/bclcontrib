using System.Reflection;
using System.Collections.Generic;
#if !SqlServer
using System.Linq;
#endif
namespace System
{
    public static class TypeExtensions
    {
        public static MethodInfo GetGenericMethod(this Type type, string name) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, new Type[] { }, new Type[] { }); }
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] genericTypes, Type[] types) { return GetGenericMethod(type, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, name, genericTypes, types); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name) { return GetGenericMethod(type, bindingAttr, name, new Type[] { }, new Type[] { }); }
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingAttr, string name, Type[] genericTypes, Type[] types)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (genericTypes == null)
                throw new ArgumentNullException("genericTypes");
#if !SqlServer
            var genericMethod = type.GetMethods(bindingAttr)
                .Where(m => m.IsGenericMethod)
                .Where(m => (m.ContainsGenericParameters) && (m.Name == name))
                .Where(m => ((genericTypes.Length == 0) || (m.GetGenericArguments().Single().GetGenericParameterConstraints().Match(genericTypes, (x, y) => x.Equals(y), true))))
                .Where(m => ((types == null) && (!m.GetParameters().Any())) || ((types != null) && (MatchParameters(m, genericTypes, types))))
                .SingleOrDefault();
            return genericMethod.GetGenericMethodDefinition();
        }

        private static bool MatchParameters(MethodInfo m, Type[] genericTypes, Type[] types)
        {
            return ((types.Length == 0) || (m.MakeGenericMethod(genericTypes)
                .GetParameters()
                .Select(c => c.ParameterType)
                .Match(types, (x, y) => x.Equals(y), true)));
        }
#else
            throw new NotImplementedException();
        }
#endif

        public static Type GetEnumerableElementType(Type type)
        {
            var enumerable = FindIEnumerable(type);
            return (enumerable == null ? type : enumerable.GetGenericArguments()[0]);
        }

        private static Type FindIEnumerable(Type type)
        {
            if ((type == null) || (type == CoreExInternal.StringType))
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
            return ((type.BaseType != null) && (type.BaseType != CoreExInternal.ObjectType) ? FindIEnumerable(type.BaseType) : null);
        }

        //use (value is Type)
        //public static bool IsType<TType>(this Type type) { return typeof(TType).IsAssignableFrom(type); }
    }
}