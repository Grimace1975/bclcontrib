namespace System.DirectoryServices
{
    public static class PropertyValueCollectionExtensions
    {
        //[Flags]
        //public enum UacFlags
        //{
        //    PasswordExpired = 0x800000,
        //    Lockout = 0x0010,
        //}

        public static void SetValue(this PropertyValueCollection propertyValueCollection, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (propertyValueCollection.Count > 0)
                    propertyValueCollection.Clear();
                return;
            }
            propertyValueCollection.Value = value;
        }

        public static void SetValue(this PropertyValueCollection propertyValueCollection, bool value)
        {
            propertyValueCollection.Value = value;
        }
    }
}
