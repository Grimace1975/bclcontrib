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
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace System.Patterns.Caching
{
    /// <summary>
    /// DataStoreHttpExtensions
    /// </summary>
    public static class DataStoreHttpExtensions
    {
        public static string FormatJson(this DataStore dataStore) { return FormatJson(null); }
        public static string FormatJson(this DataStore dataStore, IEnumerable<Type> knownTypes)
        {
            object data = dataStore.Data;
            if (data != null)
            {
                var dataType = data.GetType();
                var stream = new MemoryStream();
                var s = (knownTypes == null ? new DataContractJsonSerializer(dataType) : new DataContractJsonSerializer(dataType, knownTypes));
                s.WriteObject(stream, dataStore.Data);
                return ASCIIEncoding.ASCII.GetString(stream.GetBuffer());
            }
            return null;
        }
        public static string FormatJson(this DataStore dataStore, DataContractJsonSerializer s)
        {
            object data = dataStore.Data;
            if (data != null)
            {
                var stream = new MemoryStream();
                s.WriteObject(stream, data);
                return ASCIIEncoding.ASCII.GetString(stream.GetBuffer());
            }
            return null;
        }

        public static DataStore ParseJson(this DataStore dataStore, string value) { return ParseJson(dataStore, value, new JavaScriptSerializer()); }
        public static DataStore ParseJson(this DataStore dataStore, string value, JavaScriptSerializer s)
        {
            //var dataHash = s.Deserialize<Dictionary<string, string>>(data);
            return dataStore;
        }
    }
}
