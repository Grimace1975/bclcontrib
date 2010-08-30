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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
namespace System.Interop.AudioVideo.Native_.DirectShow
{
    /// <summary>
    /// FilterInfo
    /// </summary>
    public class FilterInfo : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterInfo"/> class.
        /// </summary>
        /// <param name="monikerString">The moniker string.</param>
        public FilterInfo(string monikerString)
        {
            MonikerString = monikerString;
            Name = GetName(monikerString);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterInfo"/> class.
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        internal FilterInfo(IMoniker moniker)
        {
            MonikerString = GetMonikerString(moniker);
            Name = GetName(moniker);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
			FilterInfo filterInfo = (FilterInfo)value;
            return (filterInfo == null ? 1 : Name.CompareTo(filterInfo.Name));
        }

        /// <summary>
        /// Creates the filter.
        /// </summary>
        /// <param name="filterMoniker">The filter moniker.</param>
        /// <returns></returns>
        internal static IBaseFilter CreateFilter(string filterMoniker)
        {
            object filterObject = null;
            IBindCtx bindCtx = null;
            IMoniker moniker = null;
            if (CreateBindCtx(0, out bindCtx) == 0)
            {
                int n = 0;
                if (MkParseDisplayName(bindCtx, filterMoniker, ref n, out moniker) == 0)
                {
                    Guid filterId = typeof(IBaseFilter).GUID;
                    moniker.BindToObject(null, null, ref filterId, out filterObject);
                    Marshal.ReleaseComObject(moniker); moniker = null;
                }
                Marshal.ReleaseComObject(bindCtx); bindCtx = null;
            }
            return (filterObject as IBaseFilter);
        }

        /// <summary>
        /// Gets the moniker string.
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        /// <returns></returns>
        private string GetMonikerString(IMoniker moniker)
        {
            string text;
            moniker.GetDisplayName(null, null, out text);
            return text;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        /// <returns></returns>
        private string GetName(IMoniker moniker)
        {
            object bagObject = null;
            IPropertyBag bag = null;
            try
            {
                System.Guid bagId = typeof(IPropertyBag).GUID;
                moniker.BindToStorage(null, null, ref bagId, out bagObject);
                bag = (IPropertyBag)bagObject;
                object value = string.Empty;
                int hresult = bag.Read("FriendlyName", ref value, IntPtr.Zero);
                if (hresult != 0)
                {
                    Marshal.ThrowExceptionForHR(hresult);
                }
                string returnValue = (string)value;
                if ((returnValue == null) || (returnValue.Length < 1))
                {
                    throw new ApplicationException();
                }
                return returnValue;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
            finally
            {
                bag = null;
                if (bagObject != null)
                {
                    Marshal.ReleaseComObject(bagObject); bagObject = null;
                }
            }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="monikerString">The moniker string.</param>
        /// <returns></returns>
        private string GetName(string monikerString)
        {
            IBindCtx bindCtx = null;
            IMoniker moniker = null;
            string name = string.Empty;            
            if (CreateBindCtx(0, out bindCtx) == 0)
            {
                int n = 0;
                if (MkParseDisplayName(bindCtx, monikerString, ref n, out moniker) == 0)
                {
                    name = GetName(moniker);
                    Marshal.ReleaseComObject(moniker); moniker = null;
                }
                Marshal.ReleaseComObject(bindCtx); bindCtx = null;
            }
            return name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// MonikerString
        /// </summary>
        public readonly string MonikerString;

        /// <summary>
        /// Creates the bind CTX.
        /// </summary>
        /// <param name="reserved">The reserved.</param>
        /// <param name="ppbc">The PPBC.</param>
        /// <returns></returns>
        [DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        /// <summary>
        /// Mks the display name of the parse.
        /// </summary>
        /// <param name="pbc">The PBC.</param>
        /// <param name="szUserName">Name of the sz user.</param>
        /// <param name="pchEaten">The PCH eaten.</param>
        /// <param name="ppmk">The PPMK.</param>
        /// <returns></returns>
        [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
        public static extern int MkParseDisplayName(IBindCtx pbc, string szUserName, ref int pchEaten, out IMoniker ppmk);
    }
}
