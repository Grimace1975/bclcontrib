//using Instinct.Collections.Frugal;
//namespace System.Collections
//{
//    public delegate object CoerceValueCallback(BaseObject d, object baseValue);

//    public delegate void PropertyChangedCallback(BaseObject d, DependencyPropertyChangedEventArgs e);

//    internal delegate bool FreezeValueCallback(BaseObject d, BaseProperty dp, EntryIndex entryIndex, PropertyMetadata metadata, bool isChecking);

//    public delegate void DependencyPropertyChangedEventHandler(object sender, DependencyPropertyChangedEventArgs e);

//    internal delegate object GetReadOnlyValueCallback(BaseObject d, out BaseValueSourceInternal source);

//    /// <summary>
//    /// PropertyMetadata
//    /// </summary>
//    public class PropertyMetadata
//    {
//        private CoerceValueCallback _coerceValueCallback;
//        //private static FreezeValueCallback _defaultFreezeValueCallback = new FreezeValueCallback(PropertyMetadata.DefaultFreezeValueCallback);
//        private object _defaultValue;
//        private static readonly UncommonField<FrugalMapBase> _defaultValueFactoryCache = new UncommonField<FrugalMapBase>();
//        internal MetadataFlags _flags;
//        private FreezeValueCallback _freezeValueCallback;
//        private static FrugalMapIterationCallback _promotionCallback = new FrugalMapIterationCallback(PropertyMetadata.DefaultValueCachePromotionCallback);
//        private PropertyChangedCallback _propertyChangedCallback;
//        private static FrugalMapIterationCallback _removalCallback = new FrugalMapIterationCallback(PropertyMetadata.DefaultValueCacheRemovalCallback);

//        #region Class Types
//        internal enum MetadataFlags : uint
//        {
//            DefaultValueModifiedID = 1,
//            FW_AffectsArrangeID = 0x80,
//            FW_AffectsMeasureID = 0x40,
//            FW_AffectsParentArrangeID = 0x200,
//            FW_AffectsParentMeasureID = 0x100,
//            FW_AffectsRenderID = 0x400,
//            FW_BindsTwoWayByDefaultID = 0x2000,
//            FW_DefaultUpdateSourceTriggerEnumBit1 = 0x40000000,
//            FW_DefaultUpdateSourceTriggerEnumBit2 = 0x80000000,
//            FW_DefaultUpdateSourceTriggerModifiedID = 0x4000000,
//            FW_InheritsModifiedID = 0x100000,
//            FW_IsNotDataBindableID = 0x1000,
//            FW_OverridesInheritanceBehaviorID = 0x800,
//            FW_OverridesInheritanceBehaviorModifiedID = 0x200000,
//            FW_ReadOnlyID = 0x8000000,
//            FW_ShouldBeJournaledID = 0x4000,
//            FW_ShouldBeJournaledModifiedID = 0x1000000,
//            FW_SubPropertiesDoNotAffectRenderID = 0x8000,
//            FW_SubPropertiesDoNotAffectRenderModifiedID = 0x10000,
//            FW_UpdatesSourceOnLostFocusByDefaultID = 0x2000000,
//            Inherited = 0x10,
//            SealedID = 2,
//            UI_IsAnimationProhibitedID = 0x20
//        }
//        #endregion Class Types

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
//        /// </summary>
//        public PropertyMetadata()
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
//        /// </summary>
//        /// <param name="defaultValue">The default value.</param>
//        public PropertyMetadata(object defaultValue)
//        {
//            DefaultValue = defaultValue;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
//        /// </summary>
//        /// <param name="propertyChangedCallback">The property changed callback.</param>
//        public PropertyMetadata(PropertyChangedCallback propertyChangedCallback)
//        {
//            PropertyChangedCallback = propertyChangedCallback;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
//        /// </summary>
//        /// <param name="defaultValue">The default value.</param>
//        /// <param name="propertyChangedCallback">The property changed callback.</param>
//        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
//        {
//            DefaultValue = defaultValue;
//            PropertyChangedCallback = propertyChangedCallback;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
//        /// </summary>
//        /// <param name="defaultValue">The default value.</param>
//        /// <param name="propertyChangedCallback">The property changed callback.</param>
//        /// <param name="coerceValueCallback">The coerce value callback.</param>
//        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
//        {
//            DefaultValue = defaultValue;
//            PropertyChangedCallback = propertyChangedCallback;
//            CoerceValueCallback = coerceValueCallback;
//        }

//        /// <summary>
//        /// Clears the cached default value.
//        /// </summary>
//        /// <param name="owner">The owner.</param>
//        /// <param name="property">The property.</param>
//        internal void ClearCachedDefaultValue(BaseObject owner, BaseProperty property)
//        {
//            FrugalMapBase base2 = _defaultValueFactoryCache.GetValue(owner);
//            if (base2.Count == 1)
//            {
//                _defaultValueFactoryCache.ClearValue(owner);
//            }
//            else
//            {
//                base2.RemoveEntry(property.GlobalIndex);
//            }
//        }

//        /// <summary>
//        /// Copies the specified dp.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <returns></returns>
//        internal PropertyMetadata Copy(BaseProperty dp)
//        {
//            PropertyMetadata metadata = CreateInstance();
//            metadata.InvokeMerge(this, dp);
//            return metadata;
//        }

//        /// <summary>
//        /// Creates the instance.
//        /// </summary>
//        /// <returns></returns>
//        internal virtual PropertyMetadata CreateInstance()
//        {
//            return new PropertyMetadata();
//        }

//        ///// <summary>
//        ///// Defaults the freeze value callback.
//        ///// </summary>
//        ///// <param name="d">The d.</param>
//        ///// <param name="dp">The dp.</param>
//        ///// <param name="entryIndex">Index of the entry.</param>
//        ///// <param name="metadata">The metadata.</param>
//        ///// <param name="isChecking">if set to <c>true</c> [is checking].</param>
//        ///// <returns></returns>
//        //private static bool DefaultFreezeValueCallback(BaseObject d, BaseProperty dp, EntryIndex entryIndex, PropertyMetadata metadata, bool isChecking)
//        //{
//        //    if (isChecking && d.HasExpression(entryIndex, dp))
//        //    {
//        //        if (TraceFreezable.IsEnabled == true)
//        //        {
//        //            TraceFreezable.Trace(TraceEventType.Warning, TraceFreezable.UnableToFreezeExpression, d, dp, dp.OwnerType);
//        //        }
//        //        return false;
//        //    }
//        //    if (dp.IsValueType == false)
//        //    {
//        //        object obj2 = d.GetValueEntry(entryIndex, dp, metadata, RequestFlags.FullyResolved).Value;
//        //        if (obj2 != null)
//        //        {
//        //            Freezable freezable = (obj2 as Freezable);
//        //            if (freezable != null)
//        //            {
//        //                if (!freezable.Freeze(isChecking))
//        //                {
//        //                    if (TraceFreezable.IsEnabled)
//        //                    {
//        //                        TraceFreezable.Trace(TraceEventType.Warning, TraceFreezable.UnableToFreezeFreezableSubProperty, d, dp, dp.OwnerType);
//        //                    }
//        //                    return false;
//        //                }
//        //            }
//        //            else
//        //            {
//        //                DispatcherObject obj3 = obj2 as DispatcherObject;
//        //                if ((obj3 != null) && (obj3.Dispatcher != null))
//        //                {
//        //                    if (TraceFreezable.IsEnabled == true)
//        //                    {
//        //                        TraceFreezable.Trace(TraceEventType.Warning, TraceFreezable.UnableToFreezeDispatcherObjectWithThreadAffinity, new object[] { d, dp, dp.OwnerType, obj3 });
//        //                    }
//        //                    return false;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return true;
//        //}

//        /// <summary>
//        /// Defaults the value cache promotion callback.
//        /// </summary>
//        /// <param name="list">The list.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="value">The value.</param>
//        private static void DefaultValueCachePromotionCallback(System.Collections.ArrayList list, int key, object value)
//        {
//            //Freezable freezable = (value as Freezable);
//            //if (freezable != null)
//            //{
//            //    freezable.FireChanged();
//            //}
//        }

//        /// <summary>
//        /// Defaults the value cache removal callback.
//        /// </summary>
//        /// <param name="list">The list.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="value">The value.</param>
//        private static void DefaultValueCacheRemovalCallback(System.Collections.ArrayList list, int key, object value)
//        {
//            //Freezable freezable = (value as Freezable);
//            //if (freezable != null)
//            //{
//            //    freezable.ClearContextAndHandlers();
//            //    freezable.Freeze();
//            //}
//        }

//        /// <summary>
//        /// Defaults the value was set.
//        /// </summary>
//        /// <returns></returns>
//        internal bool DefaultValueWasSet()
//        {
//            return IsModified(MetadataFlags.DefaultValueModifiedID);
//        }

//        /// <summary>
//        /// Gets the cached default value.
//        /// </summary>
//        /// <param name="owner">The owner.</param>
//        /// <param name="property">The property.</param>
//        /// <returns></returns>
//        private object GetCachedDefaultValue(BaseObject owner, BaseProperty property)
//        {
//            FrugalMapBase base2 = _defaultValueFactoryCache.GetValue(owner);
//            if (base2 == null)
//            {
//                return BaseProperty.UnsetValue;
//            }
//            return base2.Search(property.GlobalIndex);
//        }

//        /// <summary>
//        /// Gets the default value.
//        /// </summary>
//        /// <param name="owner">The owner.</param>
//        /// <param name="property">The property.</param>
//        /// <returns></returns>
//        internal object GetDefaultValue(BaseObject owner, BaseProperty property)
//        {
//            DefaultValueFactory factory = (_defaultValue as DefaultValueFactory);
//            if (factory == null)
//            {
//                return _defaultValue;
//            }
//            if (owner.IsSealed == true)
//            {
//                return factory.DefaultValue;
//            }
//            object cachedDefaultValue = this.GetCachedDefaultValue(owner, property);
//            if (cachedDefaultValue == BaseProperty.UnsetValue)
//            {
//                cachedDefaultValue = factory.CreateDefaultValue(owner, property);
//                property.ValidateFactoryDefaultValue(cachedDefaultValue);
//                SetCachedDefaultValue(owner, property, cachedDefaultValue);
//            }
//            return cachedDefaultValue;
//        }

//        /// <summary>
//        /// Invokes the merge.
//        /// </summary>
//        /// <param name="baseMetadata">The base metadata.</param>
//        /// <param name="dp">The dp.</param>
//        internal void InvokeMerge(PropertyMetadata baseMetadata, BaseProperty dp)
//        {
//            Merge(baseMetadata, dp);
//        }

//        /// <summary>
//        /// Determines whether the specified id is modified.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        /// <returns>
//        /// 	<c>true</c> if the specified id is modified; otherwise, <c>false</c>.
//        /// </returns>
//        private bool IsModified(MetadataFlags id)
//        {
//            return ((id & _flags) != ((MetadataFlags)0));
//        }

//        /// <summary>
//        /// Merges the specified base metadata.
//        /// </summary>
//        /// <param name="baseMetadata">The base metadata.</param>
//        /// <param name="dp">The dp.</param>
//        protected virtual void Merge(PropertyMetadata baseMetadata, BaseProperty dp)
//        {
//            if (baseMetadata == null)
//            {
//                throw new ArgumentNullException("baseMetadata");
//            }
//            if (Sealed == true)
//            {
//                throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
//            }
//            if (IsModified(MetadataFlags.DefaultValueModifiedID) == false)
//            {
//                _defaultValue = baseMetadata.DefaultValue;
//            }
//            if (baseMetadata.PropertyChangedCallback != null)
//            {
//                System.Delegate[] invocationList = baseMetadata.PropertyChangedCallback.GetInvocationList();
//                if (invocationList.Length > 0)
//                {
//                    PropertyChangedCallback a = (PropertyChangedCallback)invocationList[0];
//                    for (int i = 1; i < invocationList.Length; i++)
//                    {
//                        a = (PropertyChangedCallback)System.Delegate.Combine(a, (PropertyChangedCallback)invocationList[i]);
//                    }
//                    a = (PropertyChangedCallback)System.Delegate.Combine(a, this._propertyChangedCallback);
//                    _propertyChangedCallback = a;
//                }
//            }
//            if (_coerceValueCallback == null)
//            {
//                _coerceValueCallback = baseMetadata.CoerceValueCallback;
//            }
//            //if (_freezeValueCallback == null)
//            //{
//            //    _freezeValueCallback = baseMetadata.FreezeValueCallback;
//            //}
//        }

//        /// <summary>
//        /// Called when [apply].
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <param name="targetType">Type of the target.</param>
//        protected virtual void OnApply(BaseProperty dp, System.Type targetType)
//        {
//        }

//        /// <summary>
//        /// Promotes all cached default values.
//        /// </summary>
//        /// <param name="owner">The owner.</param>
//        internal static void PromoteAllCachedDefaultValues(BaseObject owner)
//        {
//            FrugalMapBase base2 = _defaultValueFactoryCache.GetValue(owner);
//            if (base2 != null)
//            {
//                base2.Iterate(null, _promotionCallback);
//            }
//        }

//        /// <summary>
//        /// Reads the flag.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        /// <returns></returns>
//        internal bool ReadFlag(MetadataFlags id)
//        {
//            return ((id & _flags) != ((MetadataFlags)0));
//        }

//        ///// <summary>
//        ///// Removes all cached default values.
//        ///// </summary>
//        ///// <param name="owner">The owner.</param>
//        //internal static void RemoveAllCachedDefaultValues(Freezable owner)
//        //{
//        //    FrugalMapBase base2 = _defaultValueFactoryCache.GetValue(owner);
//        //    if (base2 != null)
//        //    {
//        //        base2.Iterate(null, _removalCallback);
//        //        _defaultValueFactoryCache.ClearValue(owner);
//        //    }
//        //}

//        /// <summary>
//        /// Seals the specified dp.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <param name="targetType">Type of the target.</param>
//        internal void Seal(BaseProperty dp, System.Type targetType)
//        {
//            OnApply(dp, targetType);
//            Sealed = true;
//        }

//        /// <summary>
//        /// Sets the cached default value.
//        /// </summary>
//        /// <param name="owner">The owner.</param>
//        /// <param name="property">The property.</param>
//        /// <param name="value">The value.</param>
//        private void SetCachedDefaultValue(BaseObject owner, BaseProperty property, object value)
//        {
//            FrugalMapBase base2 = _defaultValueFactoryCache.GetValue(owner);
//            if (base2 == null)
//            {
//                base2 = new SingleObjectMap();
//                _defaultValueFactoryCache.SetValue(owner, base2);
//            }
//            else if ((base2 is HashObjectMap) == false)
//            {
//                FrugalMapBase newMap = new HashObjectMap();
//                base2.Promote(newMap);
//                base2 = newMap;
//                _defaultValueFactoryCache.SetValue(owner, base2);
//            }
//            base2.InsertEntry(property.GlobalIndex, value);
//        }

//        /// <summary>
//        /// Sets the modified.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        private void SetModified(MetadataFlags id)
//        {
//            _flags |= id;
//        }

//        /// <summary>
//        /// Writes the flag.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        /// <param name="value">if set to <c>true</c> [value].</param>
//        internal void WriteFlag(MetadataFlags id, bool value)
//        {
//            if (value)
//            {
//                _flags |= id;
//            }
//            else
//            {
//                _flags &= ~id;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the coerce value callback.
//        /// </summary>
//        /// <value>The coerce value callback.</value>
//        public CoerceValueCallback CoerceValueCallback
//        {
//            get { return _coerceValueCallback; }
//            set
//            {
//                if (Sealed == true)
//                {
//                    throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
//                }
//                _coerceValueCallback = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the default value.
//        /// </summary>
//        /// <value>The default value.</value>
//        public object DefaultValue
//        {
//            get
//            {
//                DefaultValueFactory factory = (_defaultValue as DefaultValueFactory);
//                if (factory == null)
//                {
//                    return this._defaultValue;
//                }
//                return factory.DefaultValue;
//            }
//            set
//            {
//                if (Sealed == true)
//                {
//                    throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
//                }
//                if (value == BaseProperty.UnsetValue)
//                {
//                    throw new ArgumentException(TR.Get("DefaultValueMayNotBeUnset"));
//                }
//                _defaultValue = value;
//                SetModified(MetadataFlags.DefaultValueModifiedID);
//            }
//        }

//        ///// <summary>
//        ///// Gets or sets the freeze value callback.
//        ///// </summary>
//        ///// <value>The freeze value callback.</value>
//        //internal FreezeValueCallback FreezeValueCallback
//        //{
//        //    get
//        //    {
//        //        if (_freezeValueCallback != null)
//        //        {
//        //            return _freezeValueCallback;
//        //        }
//        //        return _defaultFreezeValueCallback;
//        //    }
//        //    set
//        //    {
//        //        if (Sealed == true)
//        //        {
//        //            throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
//        //        }
//        //        _freezeValueCallback = value;
//        //    }
//        //}

//        /// <summary>
//        /// Gets the get read only value callback.
//        /// </summary>
//        /// <value>The get read only value callback.</value>
//        internal virtual GetReadOnlyValueCallback GetReadOnlyValueCallback
//        {
//            get { return null; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is default value modified.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is default value modified; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsDefaultValueModified
//        {
//            get { return IsModified(MetadataFlags.DefaultValueModifiedID); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is inherited.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is inherited; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsInherited
//        {
//            get { return ((MetadataFlags.Inherited & _flags) != ((MetadataFlags)0)); }
//            set
//            {
//                if (value == true)
//                {
//                    _flags |= MetadataFlags.Inherited;
//                }
//                else
//                {
//                    _flags &= ~MetadataFlags.Inherited;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is sealed.
//        /// </summary>
//        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
//        protected bool IsSealed
//        {
//            get { return Sealed; }
//        }

//        /// <summary>
//        /// Gets or sets the property changed callback.
//        /// </summary>
//        /// <value>The property changed callback.</value>
//        public PropertyChangedCallback PropertyChangedCallback
//        {
//            get { return _propertyChangedCallback; }
//            set
//            {
//                if (Sealed == true)
//                {
//                    throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
//                }
//                _propertyChangedCallback = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this <see cref="PropertyMetadata"/> is sealed.
//        /// </summary>
//        /// <value><c>true</c> if sealed; otherwise, <c>false</c>.</value>
//        internal bool Sealed
//        {
//            get { return ReadFlag(MetadataFlags.SealedID); }
//            set { WriteFlag(MetadataFlags.SealedID, value); }
//        }

//        /// <summary>
//        /// Gets a value indicating whether [using default value factory].
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if [using default value factory]; otherwise, <c>false</c>.
//        /// </value>
//        internal bool UsingDefaultValueFactory
//        {
//            get { return (_defaultValue is DefaultValueFactory); }
//        }
//    }
//}