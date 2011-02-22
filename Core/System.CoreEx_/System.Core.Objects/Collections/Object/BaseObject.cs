namespace System.Collections.Object
{
    /// <summary>
    /// BaseObject
    /// </summary>
    public class BaseObject
    {
        public BaseObjectType BaseObjectType;
        private Simple.EffectiveValueEntry[] _effectiveValues;
        private uint _packedData;

        /// <summary>
        /// Inserts the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="entryIndex">Index of the entry.</param>
        private void InsertEntry(Simple.EffectiveValueEntry entry, uint entryIndex)
        {
            if (CanModifyEffectiveValues == false)
            {
                throw new InvalidOperationException(TR.Get("LocalValueEnumerationInvalidated"));
            }
            uint effectiveValuesCount = EffectiveValuesCount;
            if (effectiveValuesCount > 0)
            {
                if (_effectiveValues.Length == effectiveValuesCount)
                {
                    int num2 = (int)(effectiveValuesCount * (IsInPropertyInitialization ? 2.0 : 1.2));
                    if (num2 == effectiveValuesCount)
                    {
                        num2++;
                    }
                    Simple.EffectiveValueEntry[] destinationArray = new Simple.EffectiveValueEntry[num2];
                    System.Array.Copy(this._effectiveValues, 0L, destinationArray, 0L, (long)entryIndex);
                    destinationArray[entryIndex] = entry;
                    System.Array.Copy(_effectiveValues, (long)entryIndex, destinationArray, (long)(entryIndex + 1), (long)(effectiveValuesCount - entryIndex));
                    _effectiveValues = destinationArray;
                }
                else
                {
                    System.Array.Copy(_effectiveValues, (long)entryIndex, this._effectiveValues, (long)(entryIndex + 1), (long)(effectiveValuesCount - entryIndex));
                    _effectiveValues[entryIndex] = entry;
                }
            }
            else
            {
                if (_effectiveValues == null)
                {
                    _effectiveValues = new Simple.EffectiveValueEntry[EffectiveValuesInitialSize];
                }
                _effectiveValues[0] = entry;
            }
            EffectiveValuesCount = effectiveValuesCount + 1;
        }

        /// <summary>
        /// Sets the effective value.
        /// </summary>
        /// <param name="entryIndex">Index of the entry.</param>
        /// <param name="dp">The dp.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="newEntry">The new entry.</param>
        /// <param name="oldEntry">The old entry.</param>
        internal void SetEffectiveValue(Simple.EntryIndex entryIndex, BaseProperty dp, PropertyMetadata metadata, Simple.EffectiveValueEntry newEntry, Simple.EffectiveValueEntry oldEntry)
        {
            //if (((metadata != null) && (metadata.IsInherited == true)) && ((newEntry.ValueSource.Base != ValueSource.Base.Inherited) && (IsSelfInheritanceParent == false)))
            //{
            //    SetIsSelfInheritanceParent();
            //    entryIndex = CheckEntryIndex(entryIndex, dp.GlobalIndex);
            //}
            //bool flag = false;
            //if ((oldEntry.HasExpressionMarker == true) && (newEntry.HasExpressionMarker == false))
            //{
            //    ValueSource.Base baseValueSourceInternal = newEntry.ValueSource.Base;
            //    flag = ((((baseValueSourceInternal == ValueSource.Base.ThemeStyle) || (baseValueSourceInternal == ValueSource.Base.ThemeStyleTrigger)) || ((baseValueSourceInternal == ValueSource.Base.Style) || (baseValueSourceInternal == ValueSource.Base.TemplateTrigger))) || ((baseValueSourceInternal == ValueSource.Base.StyleTrigger) || (baseValueSourceInternal == ValueSource.Base.ParentTemplate))) || (baseValueSourceInternal == ValueSource.Base.ParentTemplateTrigger);
            //}
            //if (flag == true)
            //{
            //    newEntry.RestoreExpressionMarker();
            //}
            //else if ((oldEntry.IsExpression == true) && (oldEntry.ModifiedValue.ExpressionValue == Expression.NoValue))
            //{
            //    newEntry.SetExpressionValue(newEntry.Value, oldEntry.ModifiedValue.BaseValue);
            //}
            if (entryIndex.Found == true)
            {
                _effectiveValues[entryIndex.Index] = newEntry;
            }
            else
            {
                InsertEntry(newEntry, entryIndex.Index);
                if ((metadata != null) && (metadata.IsInherited == true))
                {
                    InheritableEffectiveValuesCount++;
                }
            }
        }

        /// <summary>
        /// Sets the effective value.
        /// </summary>
        /// <param name="entryIndex">Index of the entry.</param>
        /// <param name="dp">The dp.</param>
        /// <param name="targetIndex">Index of the target.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="value">The value.</param>
        /// <param name="valueSource">The value source.</param>
        internal void SetEffectiveValue(Simple.EntryIndex entryIndex, BaseProperty dp, int targetIndex, PropertyMetadata metadata, object value, Simple.ValueSource.Base valueSource)
        {
            Simple.EffectiveValueEntry entry;
            //if (((metadata != null) && (metadata.IsInherited == true)) && ((valueSource != ValueSource.Base.Inherited) && (IsSelfInheritanceParent == false)))
            //{
            //    SetIsSelfInheritanceParent();
            //    entryIndex = CheckEntryIndex(entryIndex, dp.GlobalIndex);
            //}
            if (entryIndex.Found == true)
            {
                entry = _effectiveValues[entryIndex.Index];
            }
            else
            {
                entry = new Simple.EffectiveValueEntry();
                entry.PropertyIndex = targetIndex;
                InsertEntry(entry, entryIndex.Index);
                if ((metadata != null) && (metadata.IsInherited == true))
                {
                    InheritableEffectiveValuesCount++;
                }
            }
            bool hasExpressionMarker = false; // value == ExpressionInAlternativeStore;
            //if (((hasExpressionMarker == false) && (entry.HasExpressionMarker == true)) && ((((valueSource == ValueSource.Base.ThemeStyle) || (valueSource == ValueSource.Base.ThemeStyleTrigger)) || ((valueSource == ValueSource.Base.Style) || (valueSource == ValueSource.Base.TemplateTrigger))) || (((valueSource == ValueSource.Base.StyleTrigger) || (valueSource == ValueSource.Base.ParentTemplate)) || (valueSource == ValueSource.Base.ParentTemplateTrigger))))
            //{
            //    entry.ValueSource.Base = valueSource;
            //    entry.IsDeferredReference = false;
            //    entry.SetExpressionValue(value, ExpressionInAlternativeStore);
            //    entry.ResetAnimatedValue();
            //    entry.ResetCoercedValue();
            //}
            //else if ((entry.IsExpression == true) && (entry.ModifiedValue.ExpressionValue == Expression.NoValue))
            //{
            //    entry.SetExpressionValue(value, entry.ModifiedValue.BaseValue);
            //}
            //else
            //{
            entry.BaseValueSourceInternal = valueSource;
            entry.ResetValue(value, hasExpressionMarker);
            if (valueSource != Simple.ValueSource.Base.Default)
            {
                entry.IsDeferredReference = (value is Simple.DeferredReference);
            }
            else
            {
                entry.IsDeferredReference = false;
            }
            //}
            _effectiveValues[entryIndex.Index] = entry;
        }

        /// <summary>
        /// Lookups the entry.
        /// </summary>
        /// <param name="targetIndex">Index of the target.</param>
        /// <returns></returns>
        internal Simple.EntryIndex LookupEntry(int targetIndex)
        {
            uint index = 0;
            uint effectiveValuesCount = EffectiveValuesCount;
            if (effectiveValuesCount > 0)
            {
                int propertyIndex;
                while ((effectiveValuesCount - index) > 3)
                {
                    uint num4 = (effectiveValuesCount + index) / 2;
                    propertyIndex = _effectiveValues[num4].PropertyIndex;
                    if (targetIndex == propertyIndex)
                    {
                        return new Simple.EntryIndex(num4);
                    }
                    if (targetIndex <= propertyIndex)
                    {
                        effectiveValuesCount = num4;
                    }
                    else
                    {
                        index = num4 + 1;
                    }
                }
            Label_004B:
                propertyIndex = _effectiveValues[index].PropertyIndex;
                if (propertyIndex == targetIndex)
                {
                    return new Simple.EntryIndex(index);
                }
                if (propertyIndex <= targetIndex)
                {
                    index++;
                    if (index < effectiveValuesCount)
                    {
                        goto Label_004B;
                    }
                }
                return new Simple.EntryIndex(index, false);
            }
            return new Simple.EntryIndex(0, false);
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="entryIndex">Index of the entry.</param>
        /// <param name="dp">The dp.</param>
        private void RemoveEntry(uint entryIndex, BaseProperty dp)
        {
            if (CanModifyEffectiveValues == false)
            {
                throw new InvalidOperationException(TR.Get("LocalValueEnumerationInvalidated"));
            }
            uint effectiveValuesCount = EffectiveValuesCount;
            System.Array.Copy(_effectiveValues, (long)(entryIndex + 1), _effectiveValues, (long)entryIndex, (long)((effectiveValuesCount - entryIndex) - 1));
            effectiveValuesCount--;
            EffectiveValuesCount = effectiveValuesCount;
            _effectiveValues[effectiveValuesCount].Clear();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can modify effective values.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can modify effective values; otherwise, <c>false</c>.
        /// </value>
        private bool CanModifyEffectiveValues
        {
            get { return ((_packedData & 0x80000) != 0); }
            set
            {
                if (value == true)
                {
                    _packedData |= 0x80000;
                }
                else
                {
                    _packedData &= 0xfff7ffff;
                }
            }
        }

        /// <summary>
        /// Gets or sets the effective values count.
        /// </summary>
        /// <value>The effective values count.</value>
        internal uint EffectiveValuesCount
        {
            get { return (_packedData & 0x3ff); }
            private set { _packedData = (_packedData & 0xfffffc00) | (value & 0x3ff); }
        }

        /// <summary>
        /// Gets or sets the inheritable effective values count.
        /// </summary>
        /// <value>The inheritable effective values count.</value>
        internal uint InheritableEffectiveValuesCount
        {
            get { return ((_packedData >> 10) & 0x1ff); }
            set { _packedData = ((uint)((value & 0x1ff) << 10)) | (_packedData & 0xfff803ff); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in property initialization.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is in property initialization; otherwise, <c>false</c>.
        /// </value>
        private bool IsInPropertyInitialization
        {
            get { return ((_packedData & 0x800000) != 0); }
            set
            {
                if (value == true)
                {
                    _packedData |= 0x800000;
                }
                else
                {
                    _packedData &= 0xff7fffff;
                }
            }
        }


        /// <summary>
        /// Gets the initial size of the effective values.
        /// </summary>
        /// <value>The initial size of the effective values.</value>
        internal virtual int EffectiveValuesInitialSize
        {
            get { return 2; }
        }

        /// <summary>
        /// Gets the effective values.
        /// </summary>
        /// <value>The effective values.</value>
        internal Simple.EffectiveValueEntry[] EffectiveValues
        {
            get { return _effectiveValues; }
        }

        /// <summary>
        /// Unsets the effective value.
        /// </summary>
        /// <param name="entryIndex">Index of the entry.</param>
        /// <param name="dp">The dp.</param>
        /// <param name="metadata">The metadata.</param>
        internal void UnsetEffectiveValue(Simple.EntryIndex entryIndex, BaseProperty dp, PropertyMetadata metadata)
        {
            if (entryIndex.Found == true)
            {
                RemoveEntry(entryIndex.Index, dp);
                if ((metadata != null) && (metadata.IsInherited == true))
                {
                    InheritableEffectiveValuesCount--;
                }
            }
        }
    }
}