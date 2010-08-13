namespace System.DirectoryServices
{
    public static class PropertyValueCollectionExtensions
    {
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
