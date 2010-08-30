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
using System.Globalization;
using System.Collections.Generic;
using System.Threading;
namespace System.Patterns.Schema
{
    /// <summary>
    /// CultureSchema
    /// </summary>
    public class CultureSchema : CultureSchemaBase
    {
        private bool _isReadOnly;
        private List<CultureInfoEx> _cultures = new List<CultureInfoEx>();
        private CultureInfoEx _defaultCulture;

        public CultureSchema() { }
        public CultureSchema(Configuration.CultureSchemaConfiguration c)
            : this() { }

        public override IEnumerable<CultureInfoEx> Cultures
        {
            get { return _cultures; }
            protected set { }
        }

        public override CultureInfoEx DefaultCulture
        {
            get { return _defaultCulture; }
            protected set
            {
                if (_isReadOnly)
                    throw new InvalidOperationException("_isBound");
                _defaultCulture = value;
            }
        }

        public override event EventHandler CultureChanged;

        protected virtual void OnCultureChanged()
        {
            EventHandler cultureChanged = CultureChanged;
            if (cultureChanged != null)
                cultureChanged(this, EventArgs.Empty);
        }

        //public CultureInfo CultureInfo
        //{
        //    get { return _cultureInfo; }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");
        //        _cultureInfo = value;
        //        OnCultureChanged(value);
        //        if (_isCultureInitialized)
        //            SetCulture(value);
        //    }
        //}

        #region FluentConfig
        public override CultureSchemaBase AddCulture(CultureInfoEx culture)
        {
            if (_isReadOnly)
                throw new InvalidOperationException("_isReadOnly");
            _cultures.Add(culture);
            return this;
        }

        public override CultureSchemaBase MakeReadOnly()
        {
            _isReadOnly = true;
            return this;
        }
        #endregion

        //public virtual void DetermineCulture()
        //{
        //    DetermineCulture(CultureInfo.CurrentUICulture, null);
        //}
        //public void DetermineCulture(string cultureName, string requestCultureName)
        //{
        //    if (string.IsNullOrEmpty(cultureName))
        //        throw new ArgumentNullException("cultureName");
        //    if (requestCultureName == null)
        //        throw new ArgumentNullException("requestCultureName");
        //    CultureInfo cultureInfo;
        //    if (!TryGetCultureInfo(cultureName, out cultureInfo))
        //        cultureInfo = s_defaultCultureInfo;
        //    CultureInfo requestCultureInfo;
        //    if (requestCultureName.Length > 0)
        //    {
        //        if (!TryGetCultureInfo(requestCultureName, out requestCultureInfo))
        //            throw new InvalidOperationException();
        //    }
        //    else
        //        requestCultureInfo = null;
        //    DetermineCulture(cultureInfo, requestCultureInfo);
        //}

        //public void DetermineCulture(CultureInfo cultureInfo, CultureInfo requestCultureInfo)
        //{
        //    m_contextDefaultCultureInfo = cultureInfo;
        //    if (requestCultureInfo != null)
        //        cultureInfo = requestCultureInfo;
        //    // locate configuration element
        //    string cultureName = cultureInfo.Name;
        //    var cultureConfigItem = _cultures.Find(c => c.Name == cultureName);
        //    if (cultureConfigItem != null)
        //    {
        //        // search thru defaults
        //        string locationCode = cultureName.Split('-')[0];
        //        foreach (var culture in Cultures)
        //            if (culture.DefaultForLocationCode == locationCode)
        //            {
        //                cultureConfigItem = culture;
        //                break;
        //            }
        //        if ((cultureConfigItem == null) || (!TryGetCultureInfo(cultureConfigItem.Name, out cultureInfo)))
        //            cultureInfo = s_defaultCultureInfo;
        //    }
        //    Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //}

    }
}
