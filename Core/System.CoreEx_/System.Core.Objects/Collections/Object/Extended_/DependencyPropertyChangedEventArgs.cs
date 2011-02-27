//namespace System.Collections
//{
//    /// <summary>
//    /// DependencyPropertyChangedEventArgs
//    /// </summary>
//    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//    public struct DependencyPropertyChangedEventArgs
//    {
//        private BaseProperty _property;
//        private PropertyMetadata _metadata;
//        private PrivateFlags _flags;
//        private EffectiveValueEntry _oldEntry;
//        private EffectiveValueEntry _newEntry;
//        private OperationType _operationType;

//        #region Class Types
//        /// <summary>
//        /// PrivateFlags
//        /// </summary>
//        private enum PrivateFlags : byte
//        {
//            /// <summary>
//            /// IsASubPropertyChange
//            /// </summary>
//            IsASubPropertyChange = 2,
//            /// <summary>
//            /// IsAValueChange
//            /// </summary>
//            IsAValueChange = 1
//        }
//        #endregion Class Types

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventArgs"/> struct.
//        /// </summary>
//        /// <param name="property">The property.</param>
//        /// <param name="oldValue">The old value.</param>
//        /// <param name="newValue">The new value.</param>
//        public DependencyPropertyChangedEventArgs(BaseProperty property, object oldValue, object newValue)
//        {
//            _property = property;
//            _metadata = null;
//            _oldEntry = new EffectiveValueEntry(property);
//            _newEntry = _oldEntry;
//            _oldEntry.Value = oldValue;
//            _newEntry.Value = newValue;
//            _flags = (PrivateFlags)0;
//            _operationType = OperationType.Unknown;
//            IsAValueChange = true;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventArgs"/> struct.
//        /// </summary>
//        /// <param name="property">The property.</param>
//        /// <param name="metadata">The metadata.</param>
//        /// <param name="oldValue">The old value.</param>
//        /// <param name="newValue">The new value.</param>
//        internal DependencyPropertyChangedEventArgs(BaseProperty property, PropertyMetadata metadata, object oldValue, object newValue)
//        {
//            _property = property;
//            _metadata = metadata;
//            _oldEntry = new EffectiveValueEntry(property);
//            _newEntry = _oldEntry;
//            _oldEntry.Value = oldValue;
//            _newEntry.Value = newValue;
//            _flags = (PrivateFlags)0;
//            _operationType = OperationType.Unknown;
//            IsAValueChange = true;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventArgs"/> struct.
//        /// </summary>
//        /// <param name="property">The property.</param>
//        /// <param name="metadata">The metadata.</param>
//        /// <param name="value">The value.</param>
//        internal DependencyPropertyChangedEventArgs(BaseProperty property, PropertyMetadata metadata, object value)
//        {
//            _property = property;
//            _metadata = metadata;
//            _oldEntry = new EffectiveValueEntry(property);
//            _oldEntry.Value = value;
//            _newEntry = _oldEntry;
//            _flags = (PrivateFlags)0;
//            _operationType = OperationType.Unknown;
//            IsASubPropertyChange = true;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventArgs"/> struct.
//        /// </summary>
//        /// <param name="property">The property.</param>
//        /// <param name="metadata">The metadata.</param>
//        /// <param name="isAValueChange">if set to <c>true</c> [is A value change].</param>
//        /// <param name="oldEntry">The old entry.</param>
//        /// <param name="newEntry">The new entry.</param>
//        /// <param name="operationType">Type of the operation.</param>
//        internal DependencyPropertyChangedEventArgs(BaseProperty property, PropertyMetadata metadata, bool isAValueChange, EffectiveValueEntry oldEntry, EffectiveValueEntry newEntry, OperationType operationType)
//        {
//            _property = property;
//            _metadata = metadata;
//            _oldEntry = oldEntry;
//            _newEntry = newEntry;
//            _flags = (PrivateFlags)0;
//            _operationType = operationType;
//            IsAValueChange = isAValueChange;
//            IsASubPropertyChange = operationType == OperationType.ChangeMutableDefaultValue;
//        }

//        /// <summary>
//        /// Gets the property.
//        /// </summary>
//        /// <value>The property.</value>
//        public BaseProperty Property
//        {
//            get { return _property; }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is A value change.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is A value change; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsAValueChange
//        {
//            get { return ReadPrivateFlag(PrivateFlags.IsAValueChange); }
//            set { WritePrivateFlag(PrivateFlags.IsAValueChange, value); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is A sub property change.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is A sub property change; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsASubPropertyChange
//        {
//            get { return ReadPrivateFlag(PrivateFlags.IsASubPropertyChange); }
//            set { WritePrivateFlag(PrivateFlags.IsASubPropertyChange, value); }
//        }

//        /// <summary>
//        /// Gets the metadata.
//        /// </summary>
//        /// <value>The metadata.</value>
//        internal PropertyMetadata Metadata
//        {
//            get { return _metadata; }
//        }

//        /// <summary>
//        /// Gets the type of the operation.
//        /// </summary>
//        /// <value>The type of the operation.</value>
//        internal OperationType OperationType
//        {
//            get { return _operationType; }
//        }

//        public object OldValue
//        {
//            get
//            {
//                EffectiveValueEntry flattenedEntry = OldEntry.GetFlattenedEntry(RequestFlags.FullyResolved);
//                if (flattenedEntry.IsDeferredReference == true)
//                {
//                    flattenedEntry.Value = ((DeferredReference)flattenedEntry.Value).GetValue(flattenedEntry.BaseValueSourceInternal);
//                    flattenedEntry.IsDeferredReference = false;
//                }
//                return flattenedEntry.Value;
//            }
//        }

//        /// <summary>
//        /// Gets the old entry.
//        /// </summary>
//        /// <value>The old entry.</value>
//        internal EffectiveValueEntry OldEntry
//        {
//            get { return _oldEntry; }
//        }

//        /// <summary>
//        /// Gets the old value source.
//        /// </summary>
//        /// <value>The old value source.</value>
//        internal BaseValueSourceInternal OldValueSource
//        {
//            get { return _oldEntry.BaseValueSourceInternal; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is old value modified.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is old value modified; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsOldValueModified
//        {
//            get { return _oldEntry.HasModifiers; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is old value deferred.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is old value deferred; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsOldValueDeferred
//        {
//            get { return _oldEntry.IsDeferredReference; }
//        }

//        public object NewValue
//        {
//            get
//            {
//                EffectiveValueEntry flattenedEntry = NewEntry.GetFlattenedEntry(RequestFlags.FullyResolved);
//                if (flattenedEntry.IsDeferredReference == true)
//                {
//                    flattenedEntry.Value = ((DeferredReference)flattenedEntry.Value).GetValue(flattenedEntry.BaseValueSourceInternal);
//                    flattenedEntry.IsDeferredReference = false;
//                }
//                return flattenedEntry.Value;
//            }
//        }

//        /// <summary>
//        /// Gets the new entry.
//        /// </summary>
//        /// <value>The new entry.</value>
//        internal EffectiveValueEntry NewEntry
//        {
//            get { return _newEntry; }
//        }

//        /// <summary>
//        /// Gets the new value source.
//        /// </summary>
//        /// <value>The new value source.</value>
//        internal BaseValueSourceInternal NewValueSource
//        {
//            get { return _newEntry.BaseValueSourceInternal; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is new value modified.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is new value modified; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsNewValueModified
//        {
//            get { return _newEntry.HasModifiers; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is new value deferred.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is new value deferred; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsNewValueDeferred
//        {
//            get { return _newEntry.IsDeferredReference; }
//        }

//        /// <summary>
//        /// Returns the hash code for this instance.
//        /// </summary>
//        /// <returns>
//        /// A 32-bit signed integer that is the hash code for this instance.
//        /// </returns>
//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }

//        /// <summary>
//        /// Indicates whether this instance and a specified object are equal.
//        /// </summary>
//        /// <param name="obj">Another object to compare to.</param>
//        /// <returns>
//        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
//        /// </returns>
//        public override bool Equals(object obj)
//        {
//            return Equals((DependencyPropertyChangedEventArgs)obj);
//        }
//        /// <summary>
//        /// Equalses the specified args.
//        /// </summary>
//        /// <param name="args">The <see cref="Instinct.Collections.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
//        /// <returns></returns>
//        public bool Equals(DependencyPropertyChangedEventArgs args)
//        {
//            return ((((((_property == args._property) && (_metadata == args._metadata)) && ((_oldEntry.Value == args._oldEntry.Value) && (_newEntry.Value == args._newEntry.Value))) && (((_flags == args._flags) && (_oldEntry.BaseValueSourceInternal == args._oldEntry.BaseValueSourceInternal)) && ((_newEntry.BaseValueSourceInternal == args._newEntry.BaseValueSourceInternal) && (_oldEntry.HasModifiers == args._oldEntry.HasModifiers)))) && (((_newEntry.HasModifiers == args._newEntry.HasModifiers) && (_oldEntry.IsDeferredReference == args._oldEntry.IsDeferredReference)) && (_newEntry.IsDeferredReference == args._newEntry.IsDeferredReference))) && (_operationType == args._operationType));
//        }

//        /// <summary>
//        /// Implements the operator ==.
//        /// </summary>
//        /// <param name="left">The <see cref="Instinct.Collections.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
//        /// <param name="right">The <see cref="Instinct.Collections.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
//        /// <returns>The result of the operator.</returns>
//        public static bool operator ==(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
//        {
//            return left.Equals(right);
//        }

//        /// <summary>
//        /// Implements the operator !=.
//        /// </summary>
//        /// <param name="left">The <see cref="Instinct.Collections.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
//        /// <param name="right">The <see cref="Instinct.Collections.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
//        /// <returns>The result of the operator.</returns>
//        public static bool operator !=(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
//        {
//            return !left.Equals(right);
//        }

//        /// <summary>
//        /// Writes the private flag.
//        /// </summary>
//        /// <param name="bit">The bit.</param>
//        /// <param name="value">if set to <c>true</c> [value].</param>
//        private void WritePrivateFlag(PrivateFlags bit, bool value)
//        {
//            if (value == true)
//            {
//                _flags = (PrivateFlags)((byte)(_flags | bit));
//            }
//            else
//            {
//                _flags = (PrivateFlags)((byte)(_flags & ((byte)~bit)));
//            }
//        }

//        /// <summary>
//        /// Reads the private flag.
//        /// </summary>
//        /// <param name="bit">The bit.</param>
//        /// <returns></returns>
//        private bool ReadPrivateFlag(PrivateFlags bit)
//        {
//            return (((byte)(_flags & bit)) != 0);
//        }
//    }
//}