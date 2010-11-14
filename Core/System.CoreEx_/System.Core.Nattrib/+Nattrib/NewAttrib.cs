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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace System
{
    public class NewAttrib : Dictionary<string, object>
    {
        private static Type s_type = typeof(Dictionary<string, object>);
        private static FieldInfo s_comparerField = s_type.GetField("comparer", BindingFlags.Instance | BindingFlags.NonPublic);

        public NewAttrib()
        {
            s_comparerField.SetValue(this, StringComparer.OrdinalIgnoreCase);
        }

        public NewAttrib(IDictionary<string, object> dictionary)
        {
            s_comparerField.SetValue(this, StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, object> pair in dictionary)
                Add(pair.Key, pair.Value);
        }

        public NewAttrib(object values)
        {
            s_comparerField.SetValue(this, StringComparer.OrdinalIgnoreCase);
            AddValues(values);
        }

        private void AddValues(object values)
        {
            if (values != null)
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                    Add(descriptor.Name, descriptor.GetValue(values));
        }

        public void AddRange(IDictionary<string, object> dictionary)
        {
            if (dictionary != null)
                foreach (var pair in dictionary)
                    Add(pair.Key, pair.Value);
        }

        public static NewAttrib Parse(NewAttrib attrib) { return (attrib != null ? attrib : null); }
        public static NewAttrib Parse(IDictionary<string, object> dictionary) { return (dictionary != null ? new NewAttrib(dictionary) : null); }
        public static NewAttrib Parse(object values) { return (values != null ? new NewAttrib(values) : null); }
    }
}
