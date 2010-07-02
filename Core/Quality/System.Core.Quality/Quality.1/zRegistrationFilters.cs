//namespace System.Ioc
//{
//    public static class RegistrationFilters
//    {
//        private static readonly Func<Type, Type, bool> defaultFilter = BuildDefaultFilter();

//        private static Func<Type, Type, bool> BuildDefaultFilter()
//        {
//            var attrType = typeof(Attribute);
//            return delegate(Type serviceType, Type registrationType)
//            {
//                return ((((!attrType.IsAssignableFrom(serviceType) && registrationType.IsAssignableFrom(serviceType)) && ((serviceType != registrationType) && !serviceType.IsAbstract)) && !serviceType.IsGenericTypeDefinition) && !serviceType.ContainsGenericParameters);
//            };
//        }

//        public static Func<Type, Type, bool> DefaultFilter
//        {
//            get { return defaultFilter; }
//        }
//    }
//}
