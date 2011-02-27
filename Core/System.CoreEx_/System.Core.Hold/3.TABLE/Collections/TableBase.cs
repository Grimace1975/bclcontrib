using System.Primitives;
using System.Collections.Generic;
using System.Patterns;
using System.Primitives.TextPacks;
namespace System.Collections
{
    /// <summary>
    /// Base class for all table-oriented application data types throughout Instinct.NET. Provide core 
    /// functions for adding, manipulating, updating, and interacting with application data.
    /// </summary>
    [CodeVersion(CodeVersionKind.Instinct, "1.0")]
    public abstract class TableBase
    {
        /// <summary>
        /// Protected member variable providing access to the underlying string representing the update information for this table.
        /// </summary>
        protected string _updateTable;
        /// <summary>
        /// Protected member variable providing access to the underlying string representing the table schema.
        /// </summary>
        protected string _schema;
        /// <summary>
        /// Protected member variable providing access to the rowIndex value.
        /// </summary>
        protected int _rowIndex = -1;
        /// <summary>
        /// Protected member variable providing access to the tableIndex value.
        /// </summary>
        protected int _tableIndex = -1;
        /// <summary>
        /// Protected member variable to the TextPackBase instance used by this class.
        /// </summary>
        protected TextPackBase _textPack;
        private object _textPackState;
        protected TableSchema _tableSchema = new TableSchema();
        protected Patterns.IContract _updateContract;
        protected TableUpdateMode _updateMode = TableUpdateMode.UpdateByTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableBase"/> class.
        /// </summary>
        protected TableBase()
        {
        }
        protected TableBase(TableBase table)
        {
            _textPack = table._textPack;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value></value>
        public object this[string key]
        {
            get { return GetValue(key, null); }
            set
            {
                //System.Type.GetTypeCode(value.GetType());
                SetValue(key, TypeCode.Empty, value);
            }
        }
        /// <summary>
        /// Sets the <see cref="System.Object"/> with the specified key and TypeCode value.
        /// </summary>
        /// <value></value>
        public object this[string key, TypeCode typeCode]
        {
            set { SetValue(key, typeCode, value); }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public virtual void Close()
        {
            //Dispose();
        }

        /// <summary>
        /// Creates a default instance of a TableBase
        /// </summary>
        /// <returns></returns>
        public TableBase CreateTable()
        {
            return CreateTable(-1);
        }
        /// <summary>
        /// Creates an instance using an initial count value provided.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public virtual TableBase CreateTable(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an XML fragment based on the contents of this instance.
        /// </summary>
        /// <returns></returns>
        public abstract string CreateXmlText();

        /// <summary>
        /// Disconnects this instance from its <see cref="Instinct.DbaseConnection"/>. Used during a CreateTable operation.
        /// </summary>
        protected virtual void Disconnect()
        {
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified key value provided. If not found, the defaultValue
        /// specified is returned.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        /// <value></value>
        public abstract object GetValue(string key, object defaultValue);

        /// <summary>
        /// Gets a value indicating whether this instance is EOT.
        /// </summary>
        /// <value><c>true</c> if this instance is EOT; otherwise, <c>false</c>.</value>
        public abstract bool IsEot { get; }

        /// <summary>
        /// Determines whether the item in collection with specified key exists.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified item exists; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool ContainsKey(string key);

        /// <summary>
        /// Gets a value indicating whether this instance is new.
        /// </summary>
        /// <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
        public abstract bool IsNew { get; }

        /// <summary>
        /// Gets ICollection instance containing the list of key values associated with this object instance.
        /// </summary>
        /// <value>The key enum.</value>
        public abstract ICollection<string> Keys { get; }

        /// <summary>
        /// Loads the dbase template instance associated with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public abstract void LoadDbaseTemplate(string key);

        /// <summary>
        /// Loads the value hash.
        /// </summary>
        /// <param name="valueHash">The value hash.</param>
        public void LoadValueHash(Dictionary<string, TypeCodeObject<object>> valueHash)
        {
            if (valueHash == null)
                throw new ArgumentNullException("valueHash");
            foreach (string valueKey in valueHash.Keys)
            {
                TypeCodeObject<object> value = valueHash[valueKey];
                SetPackValue(valueKey, value.Value, value.TypeCode);
            }
        }

        /// <summary>
        /// Moves the current position within the table by the count specified.
        /// </summary>
        /// <param name="count">The count.</param>
        public abstract void Move(int count);

        /// <summary>
        /// Moves the current position to the first item in the table.
        /// </summary>
        public abstract void MoveFirst();

        /// <summary>
        /// Moves the current position to the next item in the table.
        /// </summary>
        public abstract void _MoveNext();

        /// <summary>
        /// Sets up and prepares to add a new row to the table.
        /// </summary>
        public virtual void NewRow()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the next table available.
        /// </summary>
        /// <returns></returns>
        public bool NextTable()
        {
            return NextTable(true);
        }
        /// <summary>
        /// Gets the next table available.
        /// </summary>
        /// <param name="isDisposeTable">if set to <c>true</c> [is dispose table].</param>
        /// <returns></returns>
        public abstract bool NextTable(bool isDisposeTable);

        /// <summary>
        /// Gets or sets the primary key values associated with this instance of the Table.
        /// </summary>
        /// <value>The primary key values.</value>
        public string[] PrimaryKey
        {
            get
            {
                List<string> primaryKeys;
                if ((_tableSchema == null) || ((primaryKeys = _tableSchema.PrimaryKeys) == null))
                    return new string[0];
                string[] primaryKeyArray = new string[primaryKeys.Count];
                if (!IsEot)
                    for (int primaryKeyIndex = 0; primaryKeyIndex < primaryKeys.Count; primaryKeyIndex++)
                        primaryKeyArray[primaryKeyIndex] = this[primaryKeys[primaryKeyIndex]].ToString();
                return primaryKeyArray;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (_tableSchema == null)
                    throw new InvalidOperationException("Core_.Local.UndefinedTableSchema");
                var primaryKeys = _tableSchema.PrimaryKeys;
                if (value.Length != primaryKeys.Count)
                    throw new ArgumentException("Core_.Local.MismatchPrimaryKey", "value");
                for (int primaryKeyIndex = 0; primaryKeyIndex < primaryKeys.Count; primaryKeyIndex++)
                    this[primaryKeys[primaryKeyIndex]] = value[primaryKeyIndex];
            }
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public abstract bool Read();

        /// <summary>
        /// Gets the row count for the current table.
        /// </summary>
        /// <value>The row count.</value>
        public virtual int RowCount
        {
            get { return -1; }
        }

        /// <summary>
        /// Gets the index of the current row.
        /// </summary>
        /// <value>The index of the row.</value>
        public int RowIndex
        {
            get { return _rowIndex; }
        }

        /// <summary>
        /// Sets the <see cref="System.Object"/> with the specified key value provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="typeCode">The type code.</param>
        /// <param name="value">The value.</param>
        /// <value></value>
        public abstract void SetValue(string key, TypeCode typeCode, object value);

        /// <summary>
        /// Gets the table count of the current table.
        /// </summary>
        /// <value>The table count.</value>
        public virtual int TableCount
        {
            get { return -1; }
        }

        /// <summary>
        /// Gets the current table index value.
        /// </summary>
        /// <value>The index of the table.</value>
        public int TableIndex
        {
            get { return _tableIndex; }
        }

        /// <summary>
        /// Gets or sets the underlying TextPackBase instance.
        /// </summary>
        /// <value>The text pack.</value>
        public TextPackBase TextPack
        {
            get { return _textPack; }
            set { _textPack = value; }
        }

        /// <summary>
        /// Resets the underlying primary key hash instance.
        /// </summary>
        public virtual void Update()
        {
            if (_updateTable.Length == 0)
                throw new InvalidOperationException("Core_.Local.UndefinedTableBaseUpdate");
            if (_tableSchema == null)
                _tableSchema = new TableSchema();
            _tableSchema.PreUpdate();
        }

        /// <summary>
        /// Gets or sets the update contract.
        /// </summary>
        /// <value>The update contract.</value>
        public IContract UpdateContract
        {
            get { return _updateContract; }
            set { _updateContract = value; }
        }

        /// <summary>
        /// Gets or sets the update mode.
        /// </summary>
        /// <value>The update mode.</value>
        public TableUpdateMode UpdateMode
        {
            get { return _updateMode; }
            set { _updateMode = value; }
        }

        /// <summary>
        /// Provides access to the underlying string representing the update information for this table.
        /// </summary>
        /// <value>The update table.</value>
        public string UpdateTable
        {
            get { return _updateTable; }
        }

        #region Row-Version
        /// <summary>
        /// Gets the row version.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual string GetRowVersion(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the rowversion values associated with this instance of the Table.
        /// </summary>
        /// <value>The rowversion values.</value>
        public string[] RowVersion
        {
            get
            {
                List<string> rowVersions;
                if ((_tableSchema == null) || ((rowVersions = _tableSchema.RowVersions) == null))
                    return new string[0];
                string[] rowVersionArray = new string[rowVersions.Count];
                if (IsEot == false)
                    for (int rowVersionIndex = 0; rowVersionIndex < rowVersions.Count; rowVersionIndex++)
                        rowVersionArray[rowVersionIndex] = GetRowVersion(rowVersions[rowVersionIndex]);
                else
                    for (int rowVersionIndex = 0; rowVersionIndex < rowVersions.Count; rowVersionIndex++)
                        rowVersionArray[rowVersionIndex] = string.Empty;
                return rowVersionArray;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if ((_tableSchema == null) || (value.Length == 0))
                    return;
                var rowVersions = _tableSchema.RowVersions;
                if (value.Length != rowVersions.Count)
                    throw new ArgumentException("Core_.Local.MismatchRowVersion", "value");
                for (int rowVersionIndex = 0; rowVersionIndex < rowVersions.Count; rowVersionIndex++)
                {
                    string rowVersion = value[rowVersionIndex];
                    if ((rowVersion != null) && (rowVersion.Length > 0))
                        SetRowVersion(rowVersions[rowVersionIndex], rowVersion);
                }
            }
        }

        /// <summary>
        /// Sets the row version.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual void SetRowVersion(string key, string value)
        {
            throw new NotImplementedException();
        }
        #endregion

        //#region GETVALUE
        //public bool GetBool(string key)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (bool)value : false);
        //}
        //public bool GetBool(string key, bool valueIfNull)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (bool)value : valueIfNull);
        //}

        //public System.DateTime GetDateTime(string key)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (System.DateTime)value : System.DateTime.MinValue);
        //}
        //public System.DateTime GetDateTime(string key, System.DateTime valueIfNull)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (System.DateTime)value : valueIfNull);
        //}

        //public decimal GetDecimal(string key)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (decimal)value : 0M);
        //}
        //public decimal GetDecimal(string key, decimal valueIfNull)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (decimal)value : valueIfNull);
        //}

        //public int GetInt32(string key)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (int)value : 0);
        //}
        //public int GetInt32(string key, int valueIfNull)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (int)value : valueIfNull);
        //}

        //public string GetText(string key)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (string)value : string.Empty);
        //}
        //public string GetText(string key, string valueIfNull)
        //{
        //    object value = GetValue(key, null);
        //    return (value != null ? (string)value : valueIfNull);
        //}
        //#endregion

        #region Table-Schema
        /// <summary>
        /// Sets the underlying List instance containing the list of primary keys to the string array provided.
        /// </summary>
        /// <param name="primaryKeyArray">The primary key array.</param>
        public void DefinePrimaryKey(params string[] primaryKeys)
        {
            if (_tableSchema == null)
                _tableSchema = new TableSchema();
            _tableSchema.DefinePrimaryKey(primaryKeys);
        }

        /// <summary>
        /// Sets the underlying List instance containing the list of rowverison keys to the string array provided.
        /// </summary>
        /// <param name="rowVersionArray">The rowversion array.</param>
        public void DefineRowVersion(params string[] rowVersions)
        {
            if (_tableSchema == null)
                _tableSchema = new TableSchema();
            _tableSchema.DefineRowVersion(rowVersions);
        }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public TableSchema Schema
        {
            get
            {
                if (_tableSchema == null)
                    _tableSchema = new TableSchema();
                return _tableSchema;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _tableSchema = value;
            }
        }
        #endregion

        #region Pack-Value
        /// <summary>
        /// Gets the value from the underlying TextPackBase instance associated with the key provided. If the TextPackBase
        /// instance is empty, returns the indexed value associated with the key provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetPackValue(string key)
        {
            if ((_textPack == null) || (key[key.Length - 1] != ']'))
                return GetValue(key, null);
            int boundIndex = key.LastIndexOf("[");
            if (boundIndex > -1)
            {
                string packKey = key.Substring(boundIndex + 1, key.Length - boundIndex - 2);
                key = key.Substring(0, boundIndex);
                //
                string fieldValue = _textPack.GetValue((string)GetValue(key, string.Empty), packKey, key, ref _textPackState);
                return (fieldValue.Length > 0 ? fieldValue : null);
            }
            return null;
        }

        /// <summary>
        /// Sets the pack value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetPackValue(string key, object value)
        {
            SetPackValue(key, value, TypeCode.Empty);
        }
        /// <summary>
        /// Sets the value associated with the specified key contained within the underlying instance of TextPackBase used
        /// by this class. If the TextPackBase instance is null, the indexed value associated with the specified key and
        /// type code is assigned to the value provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="typeCode">The type code.</param>
        public void SetPackValue(string key, object value, TypeCode typeCode)
        {
            if ((_textPack == null) || (key[key.Length - 1] != ']'))
            {
                SetValue(key, typeCode, value);
                return;
            }
            int boundIndex = key.LastIndexOf("[");
            if (boundIndex > -1)
            {
                string packKey = key.Substring(boundIndex + 1, key.Length - boundIndex - 2);
                key = key.Substring(0, boundIndex);
                //
                SetValue(key, TypeCode.String, _textPack.SetValue((string)GetValue(key, string.Empty), packKey, (value != null ? value.ToString() : string.Empty), key, ref _textPackState));
            }
            else
                SetValue(key, typeCode, value);
        }
        #endregion
    }
}