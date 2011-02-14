using System.Collections.Generic;
namespace System.Collections
{
    /// <summary>
    /// TableSchema
    /// </summary>
    //: should this inherit from a dictionary.
    [CodeVersion(CodeVersionKind.Instinct, "1.0")]
    public class TableSchema : ICollectionIndexer<string, TableColumn>
    {
        protected Dictionary<string, TableColumn> _hash = new Dictionary<string, TableColumn>();
        /// <summary>
        /// private member variable access to the underlying List&lt;string&gt; instance containing the
        /// primary keys for this table.
        /// </summary>
        private List<string> _commitExpressionList;
        /// <summary>
        /// private member variable access to the underlying List&lt;string&gt; instance containing the
        /// primary keys for this table.
        /// </summary>
        private List<string> _primaryKeys;
        /// <summary>
        /// private member variable access to the underlying List&lt;string&gt; instance containing the
        /// rowversion keys for this table.
        /// </summary>
        private List<string> _rowVersions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSchema"/> class.
        /// </summary>
        public TableSchema() { }

        /// <summary>
        /// Gets or sets the <see cref="Instinct.TableColumn"/> with the specified key.
        /// </summary>
        /// <value></value>
        public TableColumn this[string key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                TableColumn tableColumn;
                if (!_hash.TryGetValue(key, out tableColumn))
                {
                    // create
                    tableColumn = new TableColumn();
                    _hash.Add(key, tableColumn);
                }
                return tableColumn;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                _hash[key] = value;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _hash.Clear();
            //      if (m_commitExpressionList != null)
            //         m_commitExpressionList.Clear();
            //      if (m_primaryKeyList != null)
            //         m_primaryKeyList.Clear();
            //      if (m_rowVersionList != null)
            //         m_rowVersionList.Clear();
            _commitExpressionList = null;
            _primaryKeys = null;
            _rowVersions = null;
        }

        /// <summary>
        /// Gets the commit expression list.
        /// </summary>
        /// <value>The commit expression list.</value>
        public List<string> CommitExpressionList
        {
            get { return _commitExpressionList; }
        }

        /// <summary>
        /// Gets the count of the items in collection.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return _hash.Count; }
        }

        /// <summary>
        /// Defines the column.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="typeCode">The type code.</param>
        public void DefineColumn(string key, TypeCode typeCode)
        {
            DefineColumn(key, new TableColumn(typeCode));
        }
        /// <summary>
        /// Defines the column.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="tableColumn">The table column.</param>
        public void DefineColumn(string key, TableColumn tableColumn)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (_hash.ContainsKey(key))
                throw new ArgumentException("Core_.Local.RedefineTableColumn", "key");
            _hash.Add(key, tableColumn);
        }

        /// <summary>
        /// Sets the underlying List instance containing the list of storeValueIsExpression keys to the string array provided.
        /// </summary>
        /// <param name="commitExpressionArray">The row version array.</param>
        public void DefineCommitExpression(params string[] commitExpressions) { DefineCommitExpression(new List<string>(commitExpressions)); }
        /// <summary>
        /// Sets the underlying List instance containing the list of storeValueIsExpression keys to the string array provided.
        /// </summary>
        /// <param name="commitExpressionList">The row version list.</param>
        public void DefineCommitExpression(List<string> commitExpressions)
        {
            if (_commitExpressionList == null)
                _commitExpressionList = commitExpressions;
            else
                _commitExpressionList.AddRange(commitExpressions);
            //if (_commitExpressionList != null)
            //    throw new ArgumentException(Core_.Local.RedefineCalculatedValue, "commitExpressionList");
            _commitExpressionList = commitExpressions;
            foreach (string commitExpression in commitExpressions)
                this[commitExpression].ColumnFlag |= TableColumnFlag.CommitExpression;
        }

        /// <summary>
        /// Sets the underlying List instance containing the list of primary keys to the string array provided.
        /// </summary>
        /// <param name="primaryKeyArray">The primary key array.</param>
        public void DefinePrimaryKey(params string[] primaryKeys)
        {
            // parse primarykeyarray
            Dictionary<string, TableColumnFlag> primaryKeyHash = null;
            List<string> primaryKeyList = new List<string>(primaryKeys);
            for (int primaryKeyIndex = 0; primaryKeyIndex < primaryKeyList.Count; primaryKeyIndex++)
            {
                string value;
                string primaryKey = primaryKeyList[primaryKeyIndex].ParseBoundedPrefix("[]", out value);
                if (value.Length > 0)
                {
                    var columnFlag = (TableColumnFlag)Enum.Parse(typeof(TableColumnFlag), value);
                    primaryKeyList[primaryKeyIndex] = primaryKey;
                    if (primaryKeyHash == null)
                        primaryKeyHash = new Dictionary<string, TableColumnFlag>(1);
                    primaryKeyHash.Add(primaryKey, columnFlag);
                }
            }
            DefinePrimaryKey(primaryKeyList);
            // apply primaryKeyHash hash
            if (primaryKeyHash != null)
                foreach (string primaryKey in primaryKeyHash.Keys)
                    this[primaryKey].ColumnFlag |= primaryKeyHash[primaryKey];
        }
        /// <summary>
        /// Sets the underlying List instance containing the list of primary keys to the string array provided.
        /// </summary>
        /// <param name="primaryKeyList">The primary key list.</param>
        public void DefinePrimaryKey(List<string> primaryKeys)
        {
            if (_primaryKeys != null)
                throw new ArgumentException("Core_.Local.RedefinePrimaryKey", "primaryKeyList");
            _primaryKeys = primaryKeys;
            foreach (string primaryKey in _primaryKeys)
                this[primaryKey].ColumnFlag |= TableColumnFlag.PrimaryKey;
        }

        /// <summary>
        /// Sets the underlying List instance containing the list of rowverison keys to the string array provided.
        /// </summary>
        /// <param name="rowVersionArray">The row version array.</param>
        public void DefineRowVersion(params string[] rowVersions) { DefineRowVersion(new List<string>(rowVersions)); }
        /// <summary>
        /// Sets the underlying List instance containing the list of row version keys to the string array provided.
        /// </summary>
        /// <param name="rowVersionList">The row version list.</param>
        public void DefineRowVersion(List<string> rowVersions)
        {
            if (_rowVersions != null)
                throw new ArgumentException("Core_.Local.RedefineRowVersion", "rowVersionList");
            _rowVersions = rowVersions;
            foreach (string rowVersion in _rowVersions)
                this[rowVersion].ColumnFlag |= TableColumnFlag.RowVersion;
        }

        /// <summary>
        /// Determines whether the item in collection with specified key exists.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified item exists; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return _hash.ContainsKey(key);
        }

        /// <summary>
        /// Resets the underlying primary key hash instance.
        /// </summary>
        public void PreUpdate()
        {
            //+ create default primary key if none specified
            if ((_primaryKeys == null) || (_primaryKeys.Count == 0))
            {
                List<string> primaryKeys = new List<string>(1);
                primaryKeys.Add("Key");
                DefinePrimaryKey(primaryKeys);
                this["Key"].ColumnFlag |= TableColumnFlag.IdentityPrimaryKey;
            }
        }

        /// <summary>
        /// Gets the primary key list.
        /// </summary>
        /// <value>The primary key list.</value>
        public List<string> PrimaryKeys
        {
            get { return _primaryKeys; }
        }

        /// <summary>
        /// Removes the item with the specified key.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _hash.Remove(key);
        }

        /// <summary>
        /// Gets the row version list.
        /// </summary>
        /// <value>The row version list.</value>
        public List<string> RowVersions
        {
            get { return _rowVersions; }
        }

        /// <summary>
        /// Return an instance of <see cref="System.Collections.Generic.ICollection{string}"/> representing the collection of
        /// keys in the indexed collection.
        /// </summary>
        /// <value>
        /// The <see cref="System.Collections.Generic.ICollection{string}"/> instance containing the collection of keys.
        /// </value>
        public ICollection<string> Keys
        {
            get { return _hash.Keys; }
        }

        /// <summary>
        /// Return an instance of <see cref="System.Collections.Generic.ICollection{TableField}"/> representing the collection of
        /// values in the indexed collection.
        /// </summary>
        /// <value>
        /// The <see cref="System.Collections.Generic.ICollection{TableField}"/> instance containing the collection of values.
        /// </value>
        public ICollection<TableColumn> Values
        {
            get { return _hash.Values; }
        }
    }
}