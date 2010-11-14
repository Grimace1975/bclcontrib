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
using System.IO;
using System.Text;
using System.Primitives.Codecs;
using System.Primitives.DataTypes;
using System.Collections.Generic;
using System.Reflection;
namespace System.Patterns.Reporting
{
    /// <summary>
    /// FlatFileCsvEmitter
    /// </summary>
    public class FlatFileCsvEmitter
    {
        private const int FLUSHCOUNTDOWN = 500;

        public class FieldAttrib
        {
            public bool? DoNotEncode;
            public bool? AsExcelFunction;
        }

        public void Emit<TItem>(TextWriter w, IEnumerable<TItem> set) { Emit<TItem>((FlatFileContext)null, w, set); }
        public void Emit<TItem>(FlatFileContext context, TextWriter w, IEnumerable<TItem> set)
        {
            if (w == null)
                throw new ArgumentNullException("w");
            if (set == null)
                throw new ArgumentNullException("set");
            // get names
            var itemProperties = GetItemProperties<TItem>();
            // header
            var fields = (context != null ? context.Fields : null);
            var b = new StringBuilder();
            if ((context == null) || (context.HasHeaderRow))
            {
                foreach (var itemProperty in itemProperties)
                {
                    // decode value
                    string name = itemProperty.Name;
                    FlatFileField field;
                    if ((fields != null) && (fields.TryGetValue(name, out field)) && (field != null))
                        if (field.IsIgnore)
                            continue;
                    b.Append(CsvCodec.Encode(name) + ",");
                }
                if (b.Length > 0)
                    b.Length--;
                w.Write(b.ToString() + Environment.NewLine);
            }
            // rows
            int flushCountDown = FLUSHCOUNTDOWN;
            foreach (var item in set)
            {
                b.Length = 0;
                foreach (var itemProperty in itemProperties)
                {
                    string valueAsText;
                    object value = itemProperty.GetValue(item, null);
                    // decode value
                    FlatFileField field;
                    if ((fields != null) && (fields.TryGetValue(itemProperty.Name, out field)) && (field != null))
                    {
                        if (field.IsIgnore)
                            continue;
                        IDataTypeFormatter dataTypeFormatter;
                        var fieldFormatter = field.CustomFieldFormatter;
                        if (fieldFormatter != null)
                        {
                            // formatter
                            valueAsText = fieldFormatter(field, item, value);
                            if (!string.IsNullOrEmpty(valueAsText))
                            {
                                var fieldAttrib = field.Attrib.Get<FieldAttrib>();
                                if (fieldAttrib != null)
                                {
                                    if (fieldAttrib.DoNotEncode == true)
                                    {
                                        b.Append(valueAsText + ",");
                                        continue;
                                    }
                                    if (fieldAttrib.AsExcelFunction == true)
                                        valueAsText = "=" + valueAsText;
                                }
                            }
                        }
                        else if ((dataTypeFormatter = field.DataTypeFormatter) != null)
                            // datatype
                            valueAsText = dataTypeFormatter.Format(value, field.DefaultValue, field.Attrib);
                        else
                        {
                            // default formatter
                            valueAsText = (value != null ? value.ToString() : string.Empty);
                            if (valueAsText.Length == 0)
                                valueAsText = field.DefaultValue;
                        }
                    }
                    else
                        valueAsText = (value != null ? value.ToString() : string.Empty);
                    // append value
                    b.Append(CsvCodec.Encode(valueAsText) + ",");
                }
                b.Length--;
                w.Write(b.ToString() + Environment.NewLine);
                // flush
                if ((--flushCountDown) == 0)
                {
                    w.Flush();
                    flushCountDown = FLUSHCOUNTDOWN;
                }
            }
            w.Flush();
        }

        private static PropertyInfo[] GetItemProperties<T>()
        {
            return typeof(T).GetProperties().ToArray();
        }
    }
}