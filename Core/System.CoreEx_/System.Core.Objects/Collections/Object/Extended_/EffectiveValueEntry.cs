//namespace System.Collections
//{
//    /// <summary>
//    /// EffectiveValueEntry
//    /// </summary>
//    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//    internal struct EffectiveValueEntry
//    {
//        private object _value;
//        private short _propertyIndex;
//        private FullValueSource _source;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="EffectiveValueEntry"/> struct.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        internal EffectiveValueEntry(BaseProperty dp)
//        {
//            _propertyIndex = (short)dp.GlobalIndex;
//            _value = null;
//            _source = (FullValueSource)0;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="EffectiveValueEntry"/> struct.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <param name="valueSource">The value source.</param>
//        internal EffectiveValueEntry(BaseProperty dp, BaseValueSourceInternal valueSource)
//        {
//            _propertyIndex = (short)dp.GlobalIndex;
//            _value = BaseProperty.UnsetValue;
//            _source = (FullValueSource)valueSource;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="EffectiveValueEntry"/> struct.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <param name="fullValueSource">The full value source.</param>
//        internal EffectiveValueEntry(BaseProperty dp, FullValueSource fullValueSource)
//        {
//            _propertyIndex = (short)dp.GlobalIndex;
//            _value = BaseProperty.UnsetValue;
//            _source = fullValueSource;
//        }


//        /// <summary>
//        /// Creates the default value entry.
//        /// </summary>
//        /// <param name="dp">The dp.</param>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        internal static EffectiveValueEntry CreateDefaultValueEntry(BaseProperty dp, object value)
//        {
//            EffectiveValueEntry entry = new EffectiveValueEntry(dp, BaseValueSourceInternal.Default);
//            entry.Value = value;
//            return entry;
//        }

//        /// <summary>
//        /// Sets the expression value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <param name="baseValue">The base value.</param>
//        internal void SetExpressionValue(object value, object baseValue)
//        {
//            EnsureModifiedValue().ExpressionValue = value;
//            IsExpression = true;
//        }

//        /// <summary>
//        /// Sets the animated value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <param name="baseValue">The base value.</param>
//        internal void SetAnimatedValue(object value, object baseValue)
//        {
//            EnsureModifiedValue().AnimatedValue = value;
//            IsAnimated = true;
//        }

//        /// <summary>
//        /// Sets the coerced value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <param name="baseValue">The base value.</param>
//        /// <param name="skipBaseValueChecks">if set to <c>true</c> [skip base value checks].</param>
//        internal void SetCoercedValue(object value, object baseValue, bool skipBaseValueChecks)
//        {
//            EnsureModifiedValue().CoercedValue = value;
//            IsCoerced = true;
//        }

//        /// <summary>
//        /// Resets the expression value.
//        /// </summary>
//        internal void ResetExpressionValue()
//        {
//            if (IsExpression == true)
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                modifiedValue.ExpressionValue = null;
//                IsExpression = false;
//                if (HasModifiers == false)
//                {
//                    Value = modifiedValue.BaseValue;
//                }
//            }
//        }

//        /// <summary>
//        /// Resets the animated value.
//        /// </summary>
//        internal void ResetAnimatedValue()
//        {
//            if (IsAnimated == true)
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                modifiedValue.AnimatedValue = null;
//                IsAnimated = false;
//                if (HasModifiers == false)
//                {
//                    Value = modifiedValue.BaseValue;
//                }
//            }
//        }

//        /// <summary>
//        /// Resets the coerced value.
//        /// </summary>
//        internal void ResetCoercedValue()
//        {
//            if (IsCoerced == true)
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                modifiedValue.CoercedValue = null;
//                IsCoerced = false;
//                if (HasModifiers == false)
//                {
//                    Value = modifiedValue.BaseValue;
//                }
//            }
//        }

//        /// <summary>
//        /// Resets the value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <param name="hasExpressionMarker">if set to <c>true</c> [has expression marker].</param>
//        internal void ResetValue(object value, bool hasExpressionMarker)
//        {
//            _source = (FullValueSource)((short)(_source & FullValueSource.ValueSourceMask));
//            _value = value;
//            if (hasExpressionMarker == true)
//            {
//                HasExpressionMarker = true;
//            }
//        }

//        /// <summary>
//        /// Restores the expression marker.
//        /// </summary>
//        internal void RestoreExpressionMarker()
//        {
//            if (HasModifiers == true)
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                modifiedValue.ExpressionValue = modifiedValue.BaseValue;
//                modifiedValue.BaseValue = BaseObject.ExpressionInAlternativeStore;
//                _source = (FullValueSource)((short)(_source | FullValueSource.HasExpressionMarker | FullValueSource.IsExpression));
//            }
//            else
//            {
//                object obj2 = Value;
//                Value = BaseObject.ExpressionInAlternativeStore;
//                SetExpressionValue(obj2, BaseObject.ExpressionInAlternativeStore);
//                _source = (FullValueSource)((short)(_source | FullValueSource.HasExpressionMarker));
//            }
//        }

//        /// <summary>
//        /// Gets or sets the index of the property.
//        /// </summary>
//        /// <value>The index of the property.</value>
//        public int PropertyIndex
//        {
//            get { return _propertyIndex; }
//            set { _propertyIndex = (short)value; }
//        }

//        /// <summary>
//        /// Gets or sets the value.
//        /// </summary>
//        /// <value>The value.</value>
//        internal object Value
//        {
//            get { return _value; }
//            set { _value = value; }
//        }

//        /// <summary>
//        /// Gets or sets the base value source internal.
//        /// </summary>
//        /// <value>The base value source internal.</value>
//        internal BaseValueSourceInternal BaseValueSourceInternal
//        {
//            get { return (BaseValueSourceInternal)((short)(_source & FullValueSource.ValueSourceMask)); }
//            set { _source = ((FullValueSource)((short)(_source & ~FullValueSource.ValueSourceMask))) | ((FullValueSource)value); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is deferred reference.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is deferred reference; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsDeferredReference
//        {
//            get {  return ReadPrivateFlag(FullValueSource.IsDeferredReference); }
//            set { WritePrivateFlag(FullValueSource.IsDeferredReference, value); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is expression.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is expression; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsExpression
//        {
//            get { return ReadPrivateFlag(FullValueSource.IsExpression); }
//            private set { WritePrivateFlag(FullValueSource.IsExpression, value); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is animated.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is animated; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsAnimated
//        {
//            get { return ReadPrivateFlag(FullValueSource.IsAnimated); }
//            private set { WritePrivateFlag(FullValueSource.IsAnimated, value); }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is coerced.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is coerced; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsCoerced
//        {
//            get { return ReadPrivateFlag(FullValueSource.IsCoerced); }
//            private set { WritePrivateFlag(FullValueSource.IsCoerced, value); }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance has modifiers.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance has modifiers; otherwise, <c>false</c>.
//        /// </value>
//        internal bool HasModifiers
//        {
//            get { return (((short)(_source & FullValueSource.ModifiersMask)) != 0); }
//        }

//        /// <summary>
//        /// Gets the full value source.
//        /// </summary>
//        /// <value>The full value source.</value>
//        internal FullValueSource FullValueSource {
//            get { return _source; }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance has expression marker.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance has expression marker; otherwise, <c>false</c>.
//        /// </value>
//        internal bool HasExpressionMarker
//        {
//            get { return ReadPrivateFlag(FullValueSource.HasExpressionMarker); }
//            set { WritePrivateFlag(FullValueSource.HasExpressionMarker, value); }
//        }

//        /// <summary>
//        /// Gets the flattened entry.
//        /// </summary>
//        /// <param name="requests">The requests.</param>
//        /// <returns></returns>
//        internal EffectiveValueEntry GetFlattenedEntry(RequestFlags requests)
//        {
//            if (((short)(_source & (FullValueSource.IsCoerced | FullValueSource.IsAnimated | FullValueSource.HasExpressionMarker | FullValueSource.IsExpression))) == 0)
//            {
//                return this;
//            }
//            if (HasModifiers == false)
//            {
//                EffectiveValueEntry entry = new EffectiveValueEntry();
//                entry.BaseValueSourceInternal = BaseValueSourceInternal;
//                entry.PropertyIndex = PropertyIndex;
//                return entry;
//            }
//            EffectiveValueEntry entry2 = new EffectiveValueEntry();
//            entry2.BaseValueSourceInternal = BaseValueSourceInternal;
//            entry2.PropertyIndex = PropertyIndex;
//            entry2.IsDeferredReference = IsDeferredReference;
//            ModifiedValue modifiedValue = ModifiedValue;
//            if (IsCoerced == true)
//            {
//                if ((requests & RequestFlags.CoercionBaseValue) == RequestFlags.FullyResolved)
//                {
//                    entry2.Value = modifiedValue.CoercedValue;
//                    return entry2;
//                }
//                if ((IsAnimated  == true) && ((requests & RequestFlags.AnimationBaseValue) == RequestFlags.FullyResolved))
//                {
//                    entry2.Value = modifiedValue.AnimatedValue;
//                    return entry2;
//                }
//                if (IsExpression == true)
//                {
//                    entry2.Value = modifiedValue.ExpressionValue;
//                    return entry2;
//                }
//                entry2.Value = modifiedValue.BaseValue;
//                return entry2;
//            }
//            if (IsAnimated == true)
//            {
//                if ((requests & RequestFlags.AnimationBaseValue) == RequestFlags.FullyResolved)
//                {
//                    entry2.Value = modifiedValue.AnimatedValue;
//                    return entry2;
//                }
//                if (IsExpression == true)
//                {
//                    entry2.Value = modifiedValue.ExpressionValue;
//                    return entry2;
//                }
//                entry2.Value = modifiedValue.BaseValue;
//                return entry2;
//            }
//            object expressionValue = modifiedValue.ExpressionValue;
//            entry2.Value = expressionValue;
//            if ((HasExpressionMarker == false) && (entry2.Value is DeferredReference))
//            {
//                entry2.IsDeferredReference = true;
//            }
//            return entry2;
//        }

//        /// <summary>
//        /// Sets the animation base value.
//        /// </summary>
//        /// <param name="animationBaseValue">The animation base value.</param>
//        internal void SetAnimationBaseValue(object animationBaseValue)
//        {
//            if (HasModifiers == false)
//            {
//                Value = animationBaseValue;
//            }
//            else
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                if (IsExpression == true)
//                {
//                    modifiedValue.ExpressionValue = animationBaseValue;
//                }
//                else
//                {
//                    modifiedValue.BaseValue = animationBaseValue;
//                }
//            }
//        }

//        /// <summary>
//        /// Sets the coersion base value.
//        /// </summary>
//        /// <param name="coersionBaseValue">The coersion base value.</param>
//        internal void SetCoersionBaseValue(object coersionBaseValue)
//        {
//            if (HasModifiers == false)
//            {
//                Value = coersionBaseValue;
//            }
//            else
//            {
//                ModifiedValue modifiedValue = ModifiedValue;
//                if (IsAnimated == true)
//                {
//                    modifiedValue.AnimatedValue = coersionBaseValue;
//                }
//                else if (IsExpression == true)
//                {
//                    modifiedValue.ExpressionValue = coersionBaseValue;
//                }
//                else
//                {
//                    modifiedValue.BaseValue = coersionBaseValue;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets or sets the local value.
//        /// </summary>
//        /// <value>The local value.</value>
//        internal object LocalValue
//        {
//            get
//            {
//                if (BaseValueSourceInternal != BaseValueSourceInternal.Local)
//                {
//                    return BaseProperty.UnsetValue;
//                }
//                if (HasModifiers == false)
//                {
//                    return Value;
//                }
//                return ModifiedValue.BaseValue;
//            }
//            set
//            {
//                if (HasModifiers == false)
//                {
//                    Value = value;
//                }
//                else
//                {
//                    ModifiedValue.BaseValue = value;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the modified value.
//        /// </summary>
//        /// <value>The modified value.</value>
//        internal ModifiedValue ModifiedValue
//        {
//            get
//            {
//                if (_value != null)
//                {
//                    return (_value as ModifiedValue);
//                }
//                return null;
//            }
//        }

//        /// <summary>
//        /// Ensures the modified value.
//        /// </summary>
//        /// <returns></returns>
//        private ModifiedValue EnsureModifiedValue()
//        {
//            ModifiedValue value2 = null;
//            if (_value == null)
//            {
//                _value = value2 = new ModifiedValue();
//                return value2;
//            }
//            value2 = _value as ModifiedValue;
//            if (value2 == null)
//            {
//                value2 = new ModifiedValue();
//                value2.BaseValue = _value;
//                _value = value2;
//            }
//            return value2;
//        }

//        /// <summary>
//        /// Clears this instance.
//        /// </summary>
//        internal void Clear()
//        {
//            _propertyIndex = -1;
//            _value = null;
//            _source = (FullValueSource)0;
//        }

//        /// <summary>
//        /// Writes the private flag.
//        /// </summary>
//        /// <param name="bit">The bit.</param>
//        /// <param name="value">if set to <c>true</c> [value].</param>
//        private void WritePrivateFlag(FullValueSource bit, bool value)
//        {
//            if (value == true)
//            {
//                _source = (FullValueSource)((short)(_source | bit));
//            }
//            else
//            {
//                _source = (FullValueSource)((short)(_source & ((short)~bit)));
//            }
//        }

//        /// <summary>
//        /// Reads the private flag.
//        /// </summary>
//        /// <param name="bit">The bit.</param>
//        /// <returns></returns>
//        private bool ReadPrivateFlag(FullValueSource bit)
//        {
//            return (((short)(_source & bit)) != 0);
//        }
//    }
//}