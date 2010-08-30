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
using System.Linq;
using System.Text;
using System.Patterns.Schema;
namespace System.Globalization
{
    /// <summary>
    /// CultureInfoEx
    /// </summary>
    public class CultureInfoEx : CultureInfo
    {
        public CultureInfoEx(int culture)
            : base(culture) { }
        public CultureInfoEx(string name)
            : base(name) { }
        public CultureInfoEx(int culture, bool useUserOverride)
            : base(culture, useUserOverride) { }
        public CultureInfoEx(string name, bool useUserOverride)
            : base(name, useUserOverride) { }

        public string Id { get; set; }
        public string DisplayNameEx { get; set; }
        public string LocalizedName { get; set; }
        public string DefaultForLocationCode { get; set; }

        public static bool TryGetCultureInfo(string name, out CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            try
            {
                cultureInfo = new CultureInfo(name);
                return true;
            }
            catch (Exception)
            {
                cultureInfo = null;
                return false;
            }
        }

        public static bool TryGetCultureInfoEx(string name, CultureSchemaBase schema, out CultureInfoEx cultureInfo)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (schema != null)
            {
                cultureInfo = schema.Cultures.Where(c => string.CompareOrdinal(c.Name, name) == 0).FirstOrDefault();
                if (cultureInfo != null)
                    return true;
            }
            try
            {
                cultureInfo = new CultureInfoEx(name);
                return true;
            }
            catch (Exception)
            {
                cultureInfo = null;
                return false;
            }
        }
    }
}