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
namespace System.Configuration
{
    /// <summary>
    /// ConfigurationElementSyn
    /// </summary>
    public class ConfigurationElementSyn<TSyn>
        where TSyn : ConfigurationElement
    {
        public ConfigurationElementSyn(TSyn syn)
        {
            Syn = syn;
        }

        protected internal object this[string propertyName]
        {
            get
            {
                var property = Properties[propertyName];
                //if (property == null)
                //{
                //    property = Properties[string.Empty];
                //    if (property.ProvidedName != propertyName)
                //        return null;
                //}
                return this[property];
            }
            set { SetPropertyValue(Properties[propertyName], value, false); }
        }

        protected internal object this[ConfigurationProperty prop]
        {
            get
            {
                return ConfigurationElementExtensions.ItemProperty.GetValue(Syn, new[] { prop });
            }
            set { SetPropertyValue(prop, value, false); }
        }

        protected internal virtual ConfigurationPropertyCollection Properties
        {
            get
            {
                ConfigurationPropertyCollection result = null;
                if (ConfigurationElementExtensions.PropertiesFromType(GetType(), out result))
                {
                    ConfigurationElementExtensions.ApplyInstanceAttributesMethod.Invoke(null, new[] { this });
                    ConfigurationElementExtensions.ApplyValidatorsRecursiveMethod.Invoke(null, new[] { this });
                }
                return result;
            }
        }

        protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
        {
            ConfigurationElementExtensions.SetPropertyValueMethod.Invoke(Syn, new[] { prop, value, ignoreLocks });
        }

        public TSyn Syn { get; private set; }
    }
}
