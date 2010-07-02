//namespace System.Ioc
//{
//    [Serializable]
//    public class ServiceRegistration
//    {
//        public bool IsValid()
//        {
//            return (((ServiceType != null) && (RegistrationHandler != null)) && (TypeFilter != null));
//        }
//        public Action<IServiceLocator, Type> RegistrationHandler { get; set; }
//        public Type ServiceType { get; set; }
//        public Func<Type, Type, bool> TypeFilter { get; set; }
//    }
//}
