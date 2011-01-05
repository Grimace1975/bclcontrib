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
using System.Configuration;
using System.ComponentModel;
using System.Reflection;
namespace System.Web.Configuration
{
    /// <summary>
    /// CustomErrorsSectionSyn
    /// </summary>
    public class CustomErrorsSectionSyn : ConfigurationElementSyn<CustomErrorsSection>
    {
        private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
        private static readonly ConfigurationProperty _propDefaultUrlRoutingType = new ConfigurationProperty("defaultUrlRoutingType", typeof(Type), null, new TypeNameConverter(), null, ConfigurationPropertyOptions.None, "Enabled UrlRouting Custom Errors");

        static CustomErrorsSectionSyn()
        {
            _properties.Add(_propDefaultUrlRoutingType);
            // add to config
            var propertiesField = typeof(CustomErrorsSection).GetField("_properties", BindingFlags.NonPublic | BindingFlags.Static);
            var properties = (ConfigurationPropertyCollection)propertiesField.GetValue(null);
            foreach (var property in _properties)
                properties.Add((ConfigurationProperty)property);
        }
        public CustomErrorsSectionSyn(CustomErrorsSection syn)
            : base(syn) { }

        [ConfigurationProperty("defaultUrlRoutingType")]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type DefaultUrlRoutingType
        {
            get { return (Type)base[_propDefaultUrlRoutingType]; }
            set { base[_propDefaultUrlRoutingType] = value; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        public static void RegisterByTouch() { CustomErrorSyn.RegisterByTouch(); }
    }
}