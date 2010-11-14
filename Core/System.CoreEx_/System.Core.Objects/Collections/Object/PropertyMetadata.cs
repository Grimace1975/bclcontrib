using System.Collections.Frugal;
namespace System.Collections.Object
{
    /// <summary>
    /// PropertyMetadata
    /// </summary>
    public class PropertyMetadata
    {
        private object _defaultValue;
        internal MetadataFlags _flags;
        private Simple.PropertyChangedCallback _propertyChangedCallback;

        #region Class Types
        internal enum MetadataFlags : uint
        {
            DefaultValueModifiedID = 1,
            FW_AffectsArrangeID = 0x80,
            FW_AffectsMeasureID = 0x40,
            FW_AffectsParentArrangeID = 0x200,
            FW_AffectsParentMeasureID = 0x100,
            FW_AffectsRenderID = 0x400,
            FW_BindsTwoWayByDefaultID = 0x2000,
            FW_DefaultUpdateSourceTriggerEnumBit1 = 0x40000000,
            FW_DefaultUpdateSourceTriggerEnumBit2 = 0x80000000,
            FW_DefaultUpdateSourceTriggerModifiedID = 0x4000000,
            FW_InheritsModifiedID = 0x100000,
            FW_IsNotDataBindableID = 0x1000,
            FW_OverridesInheritanceBehaviorID = 0x800,
            FW_OverridesInheritanceBehaviorModifiedID = 0x200000,
            FW_ReadOnlyID = 0x8000000,
            FW_ShouldBeJournaledID = 0x4000,
            FW_ShouldBeJournaledModifiedID = 0x1000000,
            FW_SubPropertiesDoNotAffectRenderID = 0x8000,
            FW_SubPropertiesDoNotAffectRenderModifiedID = 0x10000,
            FW_UpdatesSourceOnLostFocusByDefaultID = 0x2000000,
            Inherited = 0x10,
            SealedID = 2,
            UI_IsAnimationProhibitedID = 0x20
        }
        #endregion Class Types

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        public PropertyMetadata()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        public PropertyMetadata(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        /// <param name="propertyChangedCallback">The property changed callback.</param>
        public PropertyMetadata(Simple.PropertyChangedCallback propertyChangedCallback)
        {
            PropertyChangedCallback = propertyChangedCallback;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="propertyChangedCallback">The property changed callback.</param>
        public PropertyMetadata(object defaultValue, Simple.PropertyChangedCallback propertyChangedCallback)
        {
            DefaultValue = defaultValue;
            PropertyChangedCallback = propertyChangedCallback;
        }

        /// <summary>
        /// Copies the specified dp.
        /// </summary>
        /// <param name="dp">The dp.</param>
        /// <returns></returns>
        internal PropertyMetadata Copy(BaseProperty dp)
        {
            PropertyMetadata metadata = CreateInstance();
            metadata.InvokeMerge(this, dp);
            return metadata;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        internal virtual PropertyMetadata CreateInstance()
        {
            return new PropertyMetadata();
        }

        /// <summary>
        /// Invokes the merge.
        /// </summary>
        /// <param name="baseMetadata">The base metadata.</param>
        /// <param name="dp">The dp.</param>
        internal void InvokeMerge(PropertyMetadata baseMetadata, BaseProperty dp)
        {
            Merge(baseMetadata, dp);
        }

        /// <summary>
        /// Determines whether the specified id is modified.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id is modified; otherwise, <c>false</c>.
        /// </returns>
        private bool IsModified(MetadataFlags id)
        {
            return ((id & _flags) != ((MetadataFlags)0));
        }

        /// <summary>
        /// Merges the specified base metadata.
        /// </summary>
        /// <param name="baseMetadata">The base metadata.</param>
        /// <param name="dp">The dp.</param>
        protected virtual void Merge(PropertyMetadata baseMetadata, BaseProperty dp)
        {
            if (baseMetadata == null)
            {
                throw new ArgumentNullException("baseMetadata");
            }
            if (Sealed == true)
            {
                throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
            }
            if (IsModified(MetadataFlags.DefaultValueModifiedID) == false)
            {
                _defaultValue = baseMetadata.DefaultValue;
            }
            if (baseMetadata.PropertyChangedCallback != null)
            {
                System.Delegate[] invocationList = baseMetadata.PropertyChangedCallback.GetInvocationList();
                if (invocationList.Length > 0)
                {
                    Simple.PropertyChangedCallback callback = (Simple.PropertyChangedCallback)invocationList[0];
                    for (int invocationIndex = 1; invocationIndex < invocationList.Length; invocationIndex++)
                    {
                        callback = (Simple.PropertyChangedCallback)System.Delegate.Combine(callback, (Simple.PropertyChangedCallback)invocationList[invocationIndex]);
                    }
                    callback = (Simple.PropertyChangedCallback)System.Delegate.Combine(callback, _propertyChangedCallback);
                    _propertyChangedCallback = callback;
                }
            }
        }

        /// <summary>
        /// Called when [apply].
        /// </summary>
        /// <param name="dp">The dp.</param>
        /// <param name="targetType">Type of the target.</param>
        protected virtual void OnApply(BaseProperty dp, System.Type targetType)
        {
        }

        /// <summary>
        /// Reads the flag.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal bool ReadFlag(MetadataFlags id)
        {
            return ((id & _flags) != ((MetadataFlags)0));
        }

        /// <summary>
        /// Seals the specified dp.
        /// </summary>
        /// <param name="dp">The dp.</param>
        /// <param name="targetType">Type of the target.</param>
        internal void Seal(BaseProperty dp, System.Type targetType)
        {
            OnApply(dp, targetType);
            Sealed = true;
        }

        /// <summary>
        /// Sets the modified.
        /// </summary>
        /// <param name="id">The id.</param>
        private void SetModified(MetadataFlags id)
        {
            _flags |= id;
        }

        /// <summary>
        /// Writes the flag.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        internal void WriteFlag(MetadataFlags id, bool value)
        {
            if (value == true)
            {
                _flags |= id;
            }
            else
            {
                _flags &= ~id;
            }
        }


        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public object DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (Sealed == true)
                {
                    throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
                }
                if (value == BaseProperty.UnsetValue)
                {
                    throw new ArgumentException(TR.Get("DefaultValueMayNotBeUnset"));
                }
                _defaultValue = value;
                SetModified(MetadataFlags.DefaultValueModifiedID);
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is default value modified.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default value modified; otherwise, <c>false</c>.
        /// </value>
        internal bool IsDefaultValueModified
        {
            get { return IsModified(MetadataFlags.DefaultValueModifiedID); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inherited.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is inherited; otherwise, <c>false</c>.
        /// </value>
        internal bool IsInherited
        {
            get { return ((MetadataFlags.Inherited & _flags) != ((MetadataFlags)0)); }
            set
            {
                if (value == true)
                {
                    _flags |= MetadataFlags.Inherited;
                }
                else
                {
                    _flags &= ~MetadataFlags.Inherited;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is sealed.
        /// </summary>
        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
        protected bool IsSealed
        {
            get { return Sealed; }
        }

        /// <summary>
        /// Gets or sets the property changed callback.
        /// </summary>
        /// <value>The property changed callback.</value>
        public Simple.PropertyChangedCallback PropertyChangedCallback
        {
            get { return _propertyChangedCallback; }
            set
            {
                if (Sealed == true)
                {
                    throw new InvalidOperationException(TR.Get("TypeMetadataCannotChangeAfterUse"));
                }
                _propertyChangedCallback = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PropertyMetadata"/> is sealed.
        /// </summary>
        /// <value><c>true</c> if sealed; otherwise, <c>false</c>.</value>
        internal bool Sealed
        {
            get { return ReadFlag(MetadataFlags.SealedID); }
            set { WriteFlag(MetadataFlags.SealedID, value); }
        }
    }
}