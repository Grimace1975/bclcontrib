using System.Resources;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    internal class DataAnnotationsResources
    {
        private static readonly ResourceManager s_resourceMan = new ResourceManager("System.ComponentModel.DataAnnotations.Resources.DataAnnotationsResources", typeof(RequiredAttribute).Assembly);

        internal static string RequiredAttribute_ValidationError
        {
            get { return s_resourceMan.GetString("RequiredAttribute_ValidationError", ResourceCulture); }
        }

        private static CultureInfo ResourceCulture { get; set; }
    }
}
