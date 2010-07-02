//namespace System.Ioc
//{
//    public static class Registration
//    {
//        public static ServiceRegistration Custom<TService>(Func<Type, Type, bool> filter, Action<IServiceLocator, Type> regAction)
//            where TService : class
//        {
//            return new ServiceRegistration
//            {
//                TypeFilter = filter,
//                RegistrationHandler = regAction,
//                ServiceType = typeof(TService),
//            };
//        }

//        public static ServiceRegistration Keyed<TService>()
//            where TService : class
//        {
//            return Keyed<TService>(RegistrationFilters.DefaultFilter);
//        }

//        public static ServiceRegistration Keyed<TService>(Func<Type, Type, bool> filter)
//            where TService : class
//        {
//            return Custom<TService>(RegistrationFilters.DefaultFilter, delegate(IServiceLocator locator, Type type)
//            {
//                locator.Register(type.FullName.ToLower(), type);
//            });
//        }

//        public static ServiceRegistration Simple<TService>() where TService : class
//        {
//            return Simple<TService>(RegistrationFilters.DefaultFilter);
//        }

//        public static ServiceRegistration Simple<TService>(Func<Type, Type, bool> filter)
//            where TService : class
//        {
//            return Custom<TService>(filter, delegate(IServiceLocator locator, Type type)
//            {
//                locator.Register<TService>(type);
//            });
//        }
//    }
//}
