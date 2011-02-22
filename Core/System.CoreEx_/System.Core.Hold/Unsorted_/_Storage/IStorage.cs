//namespace System.Patterns.Storage
//{
//    /// <summary>
//    /// Provides a generic contract definition for classes that operate as temporary or persistent storage for application data.
//    /// </summary>
//    [CodeVersion(CodeVersionKind.Instinct, "1.0")]
//    public interface IStorage : Collections.IIndexer<string, object>
//    {
//        /// <summary>
//        /// Initialized the DefinePrimaryKey collection to the array provided.
//        /// </summary>
//        /// <param name="primaryKeyArray">The primary key array.</param>
//        void DefinePrimaryKeys(params string[] primaryKeys);
//        /// <summary>
//        /// Initialized the DefineRowVersion collection to the array provided.
//        /// </summary>
//        /// <param name="rowVersionArray">The rowversion array.</param>
//        void DefineRowVersions(params string[] rowVersions);
//        /// <summary>
//        /// Clears this instance.
//        /// </summary>
//        void Clear();
//        /// <summary>
//        /// Gets a value indicating whether this instance is enabled.
//        /// </summary>
//        /// <value><c>true</c> if this instance is enable; otherwise, <c>false</c>.</value>
//        bool Enable { get; }
//        /// <summary>
//        /// Gets a value indicating whether this instance is new.
//        /// </summary>
//        /// <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
//        bool IsNew { get; }
//        /// <summary>
//        /// Loads the state.
//        /// </summary>
//        /// <param name="savedState">State of the saved.</param>
//        void LoadState(object state);
//        /// <summary>
//        /// Gets and sets the primary key values for the implementating instance.
//        /// </summary>
//        /// <value>The primary key values.</value>
//        string[] PrimaryKeys { get; set; }
//        /// <summary>
//        /// Gets and sets the rowversion values for the implementating instance.
//        /// </summary>
//        /// <value>The rowversion values.</value>
//        string[] RowVersions { get; set; }
//        /// <summary>
//        /// Saves the state.
//        /// </summary>
//        /// <returns></returns>
//        object SaveState();
//        /// <summary>
//        /// Initiates an update of the information to the underlying storage layer.
//        /// </summary>
//        /// <param name="storageFlag">The storage flag.</param>
//        void Update(StorageUpdateOptions updateOptions);
//        /// <summary>
//        /// Gets the value associated with the implementating instance.
//        /// </summary>
//        /// <value>The value.</value>
//        Collections.TypeCodeIndexer<string, object> Value { get; }
//    }
//}