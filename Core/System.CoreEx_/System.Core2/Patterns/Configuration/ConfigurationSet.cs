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
using System.Configuration;
namespace System.Patterns.Configuration
{
    /// <summary>
    /// A generic hash-based class extending <see cref="T:System.Configuration.ConfigurationElementCollection"/>. 
    /// This class is used as the basis for all objects in Instinct that provide object wrappers over groupings
    /// of configuration settings.
    /// </summary>
    /// <typeparam name="T">Specific type contained within the generic hash instance.</typeparam>

    public class ConfigurationSet<T> : ConfigurationElementCollection
        where T : Configuration, new()
    {
        private AttributeIndex _attributeIndex;
        private ConfigurationElementCollectionType _collectionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSet&lt;T&gt;"/> class.
        /// </summary>
        protected ConfigurationSet()
            : this(ConfigurationElementCollectionType.AddRemoveClearMap) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSet&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collectionType">Type of the collection.</param>
        protected ConfigurationSet(ConfigurationElementCollectionType collectionType)
        {
            _collectionType = collectionType;
            _attributeIndex = new AttributeIndex(this);
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Configuration.ConfigurationSection"/> or
        /// <see cref="System.Configuration.ConfigurationElement"/> instance at the specified index.
        /// </summary>
        /// <value>
        /// An instance of a class that derives from <see cref="System.Configuration.ConfigurationElement"/>
        /// </value>
        public T this[int index]
        {
            get { return (T)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="System.Configuration.ConfigurationSection"/> or
        /// <see cref="System.Configuration.ConfigurationElement"/> instance at the specified index.
        /// </summary>
        /// <value>
        /// An instance of a class that derives from <see cref="System.Configuration.ConfigurationElement"/>
        /// </value>
        public new T this[string name]
        {
            get { return (T)BaseGet(name); }
        }

        /// <summary>
        /// Adds if undefined.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public T AddIfUndefined(string name)
        {
            bool isNew;
            return AddIfUndefined(name, out isNew);
        }
        /// <summary>
        /// Adds if undefined.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isNew">if set to <c>true</c> [is new].</param>
        /// <returns></returns>
        public T AddIfUndefined(string name, out bool isNew)
        {
            T t = (T)BaseGet(name);
            if (t == null)
            {
                t = (T)CreateNewElement();
                t.Name = name;
                BaseAdd(t);
                isNew = true;
                return t;
            }
            isNew = false;
            return t;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElementCollection"/> object is read only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Configuration.ConfigurationElementCollection"/> object is read only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        ///// <summary>
        ///// Gets the property collection.
        ///// </summary>
        ///// <value>The property collection.</value>
        //public ConfigurationPropertyCollection PropertyCollection
        //{
        //    get { return base.Properties; }
        //}

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// 	<c>true</c> if the <see cref="T:Hash`2">Hash</see> contains an element with the specified key; otherwise, <c>false</c>
        /// </returns>
        public bool TryGetValue(object key, out T value)
        {
            value = (T)BaseGet(key);
            return (value != null);
        }

        #region Inheriting
        /// <summary>
        /// Applies the configuration.
        /// </summary>
        /// <param name="inherit">The inherit.</param>
        public void ApplyConfiguration(ConfigurationSet<T> inheritConfiguration)
        {
            ApplyConfigurationValues(inheritConfiguration);
            ApplyConfigurationElements(inheritConfiguration);
        }

        /// <summary>
        /// Applies the configuration values.
        /// </summary>
        /// <param name="inheritConfiguration">The inherit configuration.</param>
        protected virtual void ApplyConfigurationValues(ConfigurationSet<T> inheritConfiguration) { }

        /// <summary>
        /// Applies the configuration elements.
        /// </summary>
        /// <param name="inherit">The inherit.</param>
        protected virtual void ApplyConfigurationElements(ConfigurationSet<T> inheritConfiguration) { }

        /// <summary>
        /// Applies the default values.
        /// </summary>
        protected virtual void ApplyDefaultValues() { }
        #endregion

        #region ConfigurationElementCollection
        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection">ConfigurationElementCollection</see>.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"></see> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection">ConfigurationElementCollection</see>
        /// returned by accessing <see cref="P:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap">AddRemoveClearMap</see>.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType"></see> of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return _collectionType; }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement">ConfigurationElement</see>
        /// of the generic parameter type T.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// Gets the Name for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"></see> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object"></see> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"></see>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Name;
        }
        #endregion

        #region Attribute
        /// <summary>
        /// Gets the AttributeIndex of this class.
        /// </summary>
        /// <value>The attribute.</value>
        public Collections.Indexer<string, object> Attribute
        {
            get { return _attributeIndex; }
        }

        /// <summary>
        /// AttributeIndex
        /// </summary>
        private class AttributeIndex : Collections.Indexer<string, object>
        {
            private ConfigurationSet<T> _parent;

            /// <summary>
            /// Initializes a new instance of the <see cref="ConfigurationHashBase&lt;T&gt;.AttributeIndex"/> class.
            /// </summary>
            /// <param name="configCollection">The config collection.</param>
            public AttributeIndex(ConfigurationSet<T> parent)
            {
                _parent = parent;
            }

            /// <summary>
            /// Gets or sets the <see cref="System.Object"/> with the specified key.
            /// </summary>
            /// <value></value>
            public override object this[string key]
            {
                get { return _parent.GetBaseItem(key); }
                set { _parent.SetBaseItem(key, value); }
            }
        }

        /// <summary>
        /// Gets the item associated with the key provided from the underlying base config collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Configuration value.</returns>
        protected object GetBaseItem(string key)
        {
            return base[key];
        }

        /// <summary>
        /// Sets the item associated with the key provided from the underlying base config collection.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="value">The value to set.</param>
        protected void SetBaseItem(string key, object value)
        {
            base[key] = value;
        }
        #endregion
    }
}