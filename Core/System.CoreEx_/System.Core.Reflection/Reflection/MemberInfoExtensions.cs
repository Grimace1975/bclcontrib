namespace System.Reflection
{
    public static class MemberInfoExtensions
    {
        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            if (memberInfo is MethodInfo)
                return ((MethodInfo)memberInfo).ReturnType;
            if (memberInfo is PropertyInfo)
                return ((PropertyInfo)memberInfo).PropertyType;
            if (memberInfo is FieldInfo)
                return ((FieldInfo)memberInfo).FieldType;
            return null;
        }

        public static IMemberAccessor ToMemberAccessor(this MemberInfo accessorCandidate)
        {
            FieldInfo fieldInfo = (accessorCandidate as FieldInfo);
            if (fieldInfo != null)
                return (accessorCandidate.DeclaringType.IsValueType ? ((IMemberAccessor)new Internal.ValueTypeFieldAccessor(fieldInfo)) : ((IMemberAccessor)new Internal.FieldAccessor(fieldInfo)));
            PropertyInfo propertyInfo = (accessorCandidate as PropertyInfo);
            if (propertyInfo != null)
                return (accessorCandidate.DeclaringType.IsValueType ? ((IMemberAccessor)new Internal.ValueTypePropertyAccessor(propertyInfo)) : ((IMemberAccessor)new Internal.PropertyAccessor(propertyInfo)));
            return null;
        }

        public static IMemberGetter ToMemberGetter(this MemberInfo accessorCandidate)
        {
            if (accessorCandidate != null)
            {
                if (accessorCandidate is PropertyInfo)
                    return new Internal.PropertyGetter((PropertyInfo)accessorCandidate);
                if (accessorCandidate is FieldInfo)
                    return new Internal.FieldGetter((FieldInfo)accessorCandidate);
                if (accessorCandidate is MethodInfo)
                    return new Internal.MethodGetter((MethodInfo)accessorCandidate);
            }
            return null;
        }
    }
}