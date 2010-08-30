//namespace System.Collections
//{
//    /// <summary>
//    /// BaseObject
//    /// </summary>
//    public class BaseObject
//    {
//        public BaseObjectType BaseObjectType;
//        private EffectiveValueEntry[] _effectiveValues;
//        internal object _contextStorage;
//        private uint _packedData;

//        /// <summary>
//        /// Gets a value indicating whether this instance is sealed.
//        /// </summary>
//        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
//        public bool IsSealed
//        {
//            get { return DO_Sealed; }
//        }

//        /// <summary>
//        /// Sets the effective value.
//        /// </summary>
//        /// <param name="entryIndex">Index of the entry.</param>
//        /// <param name="dp">The dp.</param>
//        /// <param name="metadata">The metadata.</param>
//        /// <param name="newEntry">The new entry.</param>
//        /// <param name="oldEntry">The old entry.</param>
//        internal void SetEffectiveValue(EntryIndex entryIndex, BaseProperty dp, PropertyMetadata metadata, EffectiveValueEntry newEntry, EffectiveValueEntry oldEntry)
//        {
//            //if (((metadata != null) && (metadata.IsInherited == true)) && ((newEntry.BaseValueSourceInternal != BaseValueSourceInternal.Inherited) && (IsSelfInheritanceParent == false)))
//            //{
//            //    SetIsSelfInheritanceParent();
//            //    entryIndex = CheckEntryIndex(entryIndex, dp.GlobalIndex);
//            //}
//            //bool flag = false;
//            //if ((oldEntry.HasExpressionMarker == true) && (newEntry.HasExpressionMarker == false))
//            //{
//            //    BaseValueSourceInternal baseValueSourceInternal = newEntry.BaseValueSourceInternal;
//            //    flag = ((((baseValueSourceInternal == BaseValueSourceInternal.ThemeStyle) || (baseValueSourceInternal == BaseValueSourceInternal.ThemeStyleTrigger)) || ((baseValueSourceInternal == BaseValueSourceInternal.Style) || (baseValueSourceInternal == BaseValueSourceInternal.TemplateTrigger))) || ((baseValueSourceInternal == BaseValueSourceInternal.StyleTrigger) || (baseValueSourceInternal == BaseValueSourceInternal.ParentTemplate))) || (baseValueSourceInternal == BaseValueSourceInternal.ParentTemplateTrigger);
//            //}
//            //if (flag == true)
//            //{
//            //    newEntry.RestoreExpressionMarker();
//            //}
//            //else if ((oldEntry.IsExpression == true) && (oldEntry.ModifiedValue.ExpressionValue == Expression.NoValue))
//            //{
//            //    newEntry.SetExpressionValue(newEntry.Value, oldEntry.ModifiedValue.BaseValue);
//            //}
//            //if (entryIndex.Found == true)
//            //{
//            //    _effectiveValues[entryIndex.Index] = newEntry;
//            //}
//            //else
//            //{
//            //    InsertEntry(newEntry, entryIndex.Index);
//            //    if ((metadata != null) && (metadata.IsInherited == true))
//            //    {
//            //        InheritableEffectiveValuesCount++;
//            //    }
//            //}
//        }

//        /// <summary>
//        /// Sets the effective value.
//        /// </summary>
//        /// <param name="entryIndex">Index of the entry.</param>
//        /// <param name="dp">The dp.</param>
//        /// <param name="targetIndex">Index of the target.</param>
//        /// <param name="metadata">The metadata.</param>
//        /// <param name="value">The value.</param>
//        /// <param name="valueSource">The value source.</param>
//        internal void SetEffectiveValue(EntryIndex entryIndex, BaseProperty dp, int targetIndex, PropertyMetadata metadata, object value, BaseValueSourceInternal valueSource)
//        {
//            //EffectiveValueEntry entry;
//            //if (((metadata != null) && (metadata.IsInherited == true)) && ((valueSource != BaseValueSourceInternal.Inherited) && (IsSelfInheritanceParent == false)))
//            //{
//            //    SetIsSelfInheritanceParent();
//            //    entryIndex = CheckEntryIndex(entryIndex, dp.GlobalIndex);
//            //}
//            //if (entryIndex.Found == true)
//            //{
//            //    entry = _effectiveValues[entryIndex.Index];
//            //}
//            //else
//            //{
//            //    entry = new EffectiveValueEntry();
//            //    entry.PropertyIndex = targetIndex;
//            //    InsertEntry(entry, entryIndex.Index);
//            //    if ((metadata != null) && (metadata.IsInherited == true))
//            //    {
//            //        InheritableEffectiveValuesCount++;
//            //    }
//            //}
//            //bool hasExpressionMarker = value == ExpressionInAlternativeStore;
//            //if (((hasExpressionMarker == false) && (entry.HasExpressionMarker == true)) && ((((valueSource == BaseValueSourceInternal.ThemeStyle) || (valueSource == BaseValueSourceInternal.ThemeStyleTrigger)) || ((valueSource == BaseValueSourceInternal.Style) || (valueSource == BaseValueSourceInternal.TemplateTrigger))) || (((valueSource == BaseValueSourceInternal.StyleTrigger) || (valueSource == BaseValueSourceInternal.ParentTemplate)) || (valueSource == BaseValueSourceInternal.ParentTemplateTrigger))))
//            //{
//            //    entry.BaseValueSourceInternal = valueSource;
//            //    entry.IsDeferredReference = false;
//            //    entry.SetExpressionValue(value, ExpressionInAlternativeStore);
//            //    entry.ResetAnimatedValue();
//            //    entry.ResetCoercedValue();
//            //}
//            //else if ((entry.IsExpression == true) && (entry.ModifiedValue.ExpressionValue == Expression.NoValue))
//            //{
//            //    entry.SetExpressionValue(value, entry.ModifiedValue.BaseValue);
//            //}
//            //else
//            //{
//            //    entry.BaseValueSourceInternal = valueSource;
//            //    entry.ResetValue(value, hasExpressionMarker);
//            //    if (valueSource != BaseValueSourceInternal.Default)
//            //    {
//            //        entry.IsDeferredReference = value is DeferredReference;
//            //    }
//            //    else
//            //    {
//            //        entry.IsDeferredReference = false;
//            //    }
//            //}
//            //_effectiveValues[entryIndex.Index] = entry;
//        }

//        ///// <summary>
//        ///// Sets the is self inheritance parent.
//        ///// </summary>
//        //internal void SetIsSelfInheritanceParent()
//        //{
//        //    BaseObject inheritanceParent = InheritanceParent;
//        //    if (inheritanceParent != null)
//        //    {
//        //        MergeInheritableProperties(inheritanceParent);
//        //        SetInheritanceParent(null);
//        //    }
//        //    _packedData |= 0x100000;
//        //}

//        ///// <summary>
//        ///// Sets the inheritance parent.
//        ///// </summary>
//        ///// <param name="newParent">The new parent.</param>
//        //private void SetInheritanceParent(BaseObject newParent)
//        //{
//        //    if (_contextStorage != null)
//        //    {
//        //        _contextStorage = newParent;
//        //    }
//        //    else if (newParent != null)
//        //    {
//        //        if (IsSelfInheritanceParent == true)
//        //        {
//        //            MergeInheritableProperties(newParent);
//        //        }
//        //        else
//        //        {
//        //            _contextStorage = newParent;
//        //        }
//        //    }
//        //}

//        ///// <summary>
//        ///// Merges the inheritable properties.
//        ///// </summary>
//        ///// <param name="inheritanceParent">The inheritance parent.</param>
//        //private void MergeInheritableProperties(BaseObject inheritanceParent)
//        //{
//        //    EffectiveValueEntry[] effectiveValues = inheritanceParent.EffectiveValues;
//        //    uint effectiveValuesCount = inheritanceParent.EffectiveValuesCount;
//        //    for (uint i = 0; i < effectiveValuesCount; i++)
//        //    {
//        //        EffectiveValueEntry entry = effectiveValues[i];
//        //        BaseProperty dp = BaseProperty.RegisteredPropertyList.List[entry.PropertyIndex];
//        //        if (dp != null)
//        //        {
//        //            PropertyMetadata metadata = dp.GetMetadata(BaseObjectType);
//        //            if (metadata.IsInherited)
//        //            {
//        //                object obj2 = inheritanceParent.GetValueEntry(new EntryIndex(i), dp, metadata, RequestFlags.SkipDefault | RequestFlags.DeferredReferences).Value;
//        //                if (obj2 != BaseProperty.UnsetValue)
//        //                {
//        //                    EntryIndex entryIndex = this.LookupEntry(dp.GlobalIndex);
//        //                    SetEffectiveValue(entryIndex, dp, dp.GlobalIndex, metadata, obj2, BaseValueSourceInternal.Inherited);
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

//        /// <summary>
//        /// Lookups the entry.
//        /// </summary>
//        /// <param name="targetIndex">Index of the target.</param>
//        /// <returns></returns>
//        internal EntryIndex LookupEntry(int targetIndex)
//        {
//            uint index = 0;
//            uint effectiveValuesCount = this.EffectiveValuesCount;
//            if (effectiveValuesCount > 0)
//            {
//                int propertyIndex;
//                while ((effectiveValuesCount - index) > 3)
//                {
//                    uint num4 = (effectiveValuesCount + index) / 2;
//                    propertyIndex = this._effectiveValues[num4].PropertyIndex;
//                    if (targetIndex == propertyIndex)
//                    {
//                        return new EntryIndex(num4);
//                    }
//                    if (targetIndex <= propertyIndex)
//                    {
//                        effectiveValuesCount = num4;
//                    }
//                    else
//                    {
//                        index = num4 + 1;
//                    }
//                }
//            Label_004B:
//                propertyIndex = _effectiveValues[index].PropertyIndex;
//                if (propertyIndex == targetIndex)
//                {
//                    return new EntryIndex(index);
//                }
//                if (propertyIndex <= targetIndex)
//                {
//                    index++;
//                    if (index < effectiveValuesCount)
//                    {
//                        goto Label_004B;
//                    }
//                }
//                return new EntryIndex(index, false);
//            }
//            return new EntryIndex(0, false);
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether [D o_ sealed].
//        /// </summary>
//        /// <value><c>true</c> if [D o_ sealed]; otherwise, <c>false</c>.</value>
//        private bool DO_Sealed
//        {
//            get { return ((this._packedData & 0x400000) != 0); }
//            set
//            {
//                if (value == true)
//                {
//                    _packedData |= 0x400000;
//                }
//                else
//                {
//                    _packedData &= 0xffbfffff;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is self inheritance parent.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is self inheritance parent; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsSelfInheritanceParent
//        {
//            get { return ((_packedData & 0x100000) != 0); }
//        }
 
//        /// <summary>
//        /// Gets or sets the effective values count.
//        /// </summary>
//        /// <value>The effective values count.</value>
//        internal uint EffectiveValuesCount
//        {
//            get { return (_packedData & 0x3ff); }
//            private set { _packedData = (_packedData & 0xfffffc00) | (value & 0x3ff); }
//        }

//        /// <summary>
//        /// Gets or sets the inheritable effective values count.
//        /// </summary>
//        /// <value>The inheritable effective values count.</value>
//        internal uint InheritableEffectiveValuesCount
//        {
//            get { return ((_packedData >> 10) & 0x1ff); }
//            set { _packedData = ((uint)((value & 0x1ff) << 10)) | (_packedData & 0xfff803ff); }
//        }

//        /// <summary>
//        /// Gets the effective values.
//        /// </summary>
//        /// <value>The effective values.</value>
//        internal EffectiveValueEntry[] EffectiveValues
//        {
//            get { return _effectiveValues; }
//        }

//        /// <summary>
//        /// Unsets the effective value.
//        /// </summary>
//        /// <param name="entryIndex">Index of the entry.</param>
//        /// <param name="dp">The dp.</param>
//        /// <param name="metadata">The metadata.</param>
//        internal void UnsetEffectiveValue(EntryIndex entryIndex, BaseProperty dp, PropertyMetadata metadata)
//        {
//            if (entryIndex.Found == true)
//            {
//                RemoveEntry(entryIndex.Index, dp);
//                if ((metadata != null) && (metadata.IsInherited == true))
//                {
//                    InheritableEffectiveValuesCount--;
//                }
//            }
//        }
//    }
//}