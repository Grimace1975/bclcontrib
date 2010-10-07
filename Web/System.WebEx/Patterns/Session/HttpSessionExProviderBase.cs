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
using System.Web;
namespace System.Patterns.Session
{
    /// <summary>
    /// HttpSessionProviderBase
    /// </summary>
    public abstract class HttpSessionExProviderBase : Collections.IIndexer<string, object>
    {
        public HttpSessionExProviderBase()
            : base()
        {
            IsNewSession = true;
            SessionUid = ((string)GetValue("_suid") ?? string.Empty);
        }

        public object this[string key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }
        }

        public virtual void Delete()
        {
            IsNewSession = true;
        }

        public abstract object GetValue(object key);

        public bool IsNewSession { get; protected set; }

        public virtual string Name { get; set; }

        public event EventHandler NewSessionCreated;

        public virtual void OnNewSessionCreated()
        {
            var sessionUid = SessionUid = Guid.NewGuid().ToString();
            SetValue("_suid", sessionUid);
            var newSessionCreated = NewSessionCreated;
            if (newSessionCreated != null)
                newSessionCreated(this, EventArgs.Empty);
        }

        public virtual void OnSessionAcquired()
        {
            var sessionAcquired = SessionAcquired;
            if (sessionAcquired != null)
                sessionAcquired(this, EventArgs.Empty);
        }

        public event EventHandler SessionAcquired;

        public string SessionId { get; set; }

        public string SessionUid { get; protected set; }

        public abstract void SetValue(object key, object value);

        protected static void SetSessionProvider(HttpContext httpContext, HttpSessionExProviderBase sessionExProvider)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (sessionExProvider == null)
                throw new ArgumentNullException("sessionExProvider");
            httpContext.SetSessionExProvider(sessionExProvider);
        }
    }
}