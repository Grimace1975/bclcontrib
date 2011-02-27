#if !CODE_ANALYSIS
namespace System.Interop.CSyntax
#else
namespace SystemEx.Interop.CSyntax
#endif
{
    public partial class PrintfHelper
    {
        public static string Sprintf(string fmt, object[] vargs)
        {
            return ((vargs == null) || (vargs.Length == 0) ? fmt : new PrintfFormat(fmt).SprintfArray(vargs));
        }
    }
}
