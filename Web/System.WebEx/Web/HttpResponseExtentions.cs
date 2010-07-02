#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Reflection;
namespace System.Web
{
    /// <summary>
    /// HttpResponseExtentions
    /// </summary>
	public static class HttpResponseExtentions
	{
		private static readonly FieldInfo s_isRequestBeingRedirectedField = typeof(HttpResponse).GetField("_isRequestBeingRedirected", BindingFlags.Instance | BindingFlags.NonPublic);

#if !CLR4
        public static void RedirectPermanent(this HttpResponse httpResponse, string url, bool endResponse)
		{
			if (httpResponse == null)
				throw new ArgumentNullException("httpResponse");
			if (url == null)
				throw new ArgumentNullException("url");
			if (url.IndexOf('\n') >= 0)
				throw new ArgumentException("Cannot_redirect_to_newline");
			var httpContext = HttpContext.Current;
			var page = (httpContext.Handler as UI.Page);
			if ((page != null) && (page.IsCallback))
				throw new ArgumentException("Redirect_not_allowed_in_callback");
			httpResponse.Clear();
			httpResponse.StatusCode = 301;
			httpResponse.RedirectLocation = url;
			url = (HttpContextEx.GetIsAbsoluteUrl(url) ? HttpUtility.HtmlAttributeEncode(url) : HttpUtility.HtmlAttributeEncode(HttpUtility.UrlEncode(url)));
			httpResponse.Write("<html><head><title>Object permanently moved</title></head><body>\r\n");
			httpResponse.Write("<h2>Object permanently moved to <left href=\"" + url + "\">here</left>.</h2>\r\n");
			httpResponse.Write("</body></html>\r\n");
			s_isRequestBeingRedirectedField.SetValue(httpResponse, true);
			if (endResponse)
				httpResponse.End();
		}

        public static void RedirectToRoute(this HttpResponse httpResponse, string url, object values) { throw new NotImplementedException(); }
        public static void RedirectToRoutPermanente(this HttpResponse httpResponse, string name, object values) { throw new NotImplementedException(); }
#endif

		// use built-in caching setters : httpResponse.Cache.SetCacheability(HttpCacheability.NoCache);
		[Obsolete]
		public static void AttachNoCache(this HttpResponse httpResponse)
		{
			if (httpResponse == null)
				throw new ArgumentNullException("httpResponse");
			httpResponse.Expires = -1;
			httpResponse.AddHeader("Pragma", "no-cache");
			httpResponse.CacheControl = "no-cache";
			httpResponse.CacheControl = "Private";
		}

		public static void AttachFileHeader(this HttpResponse httpResponse, string contentType, string fileName)
		{
			if (httpResponse == null)
				throw new ArgumentNullException("httpResponse");
			httpResponse.ContentType = contentType;
			httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
		}
	}
}