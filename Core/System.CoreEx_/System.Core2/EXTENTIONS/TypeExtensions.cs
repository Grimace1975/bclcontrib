using System.Reflection;
#if !SqlServer
using System.Linq;
#endif
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
                .Where(m => m.IsGenericMethod)
                .Where(m => (m.ContainsGenericParameters) && (m.Name == name))
                .Where(m => ((genericTypes == null) && (!m.GetGenericArguments().Any())) || ((genericTypes != null) && (m.GetGenericArguments().Single().GetGenericParameterConstraints().Match(genericTypes, true))))
                .Where(m => ((types == null) && (!m.GetParameters().Any())) || ((types != null) && (m.GetParameters().Select(c => c.ParameterType).Match(types, ParameterMatchPredicate, true))))
                .Single();
            return genericMethod.GetGenericMethodDefinition();
#else
            throw new NotImplementedException();
#endif
        }

        private static bool ParameterMatchPredicate(Type left, Type right)
        {
            if (left.ContainsGenericParameters)
                return left.Equals(right);
            return true;
        }
    }
}