using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Mvc
{
    /// <summary>
    /// DataAnnotationsModelMetadataProviderEx
    /// </summary>
    public class DataAnnotationsModelMetadataProviderEx : DataAnnotationsModelMetadataProvider
    {
        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The metadata for the property.</returns>
        // hard coded creation of metadata so must take over entire method
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            #region clone of base method + CreateMetadata
            var attributeList = new List<Attribute>(attributes);
            var displayColumnAttribute = attributeList.OfType<DisplayColumnAttribute>().FirstOrDefault();
            var result = CreateMetadata(containerType, modelAccessor, modelType, propertyName, displayColumnAttribute);
            //
            var hiddenInputAttribute = attributeList.OfType<HiddenInputAttribute>().FirstOrDefault();
            if (hiddenInputAttribute != null)
            {
                result.TemplateHint = "HiddenInput";
                result.HideSurroundingHtml = !hiddenInputAttribute.DisplayValue;
            }
            //
            var uiHintAttributes = attributeList.OfType<UIHintAttribute>();
            var uiHintAttribute = (uiHintAttributes.FirstOrDefault(a => String.Equals(a.PresentationLayer, "MVC", StringComparison.OrdinalIgnoreCase)) ?? uiHintAttributes.FirstOrDefault(a => String.IsNullOrEmpty(a.PresentationLayer)));
            if (uiHintAttribute != null)
                result.TemplateHint = uiHintAttribute.UIHint;
            //
            var dataTypeAttribute = attributeList.OfType<DataTypeAttribute>().FirstOrDefault();
            if (dataTypeAttribute != null)
                result.DataTypeName = dataTypeAttribute.GetDataTypeName();
            //
            var readOnlyAttribute = attributeList.OfType<ReadOnlyAttribute>().FirstOrDefault();
            if (readOnlyAttribute != null)
                result.IsReadOnly = readOnlyAttribute.IsReadOnly;
            //
            var displayFormatAttribute = attributeList.OfType<DisplayFormatAttribute>().FirstOrDefault();
            if ((displayFormatAttribute == null) && (dataTypeAttribute != null))
                displayFormatAttribute = dataTypeAttribute.DisplayFormat;
            if (displayFormatAttribute != null)
            {
                result.NullDisplayText = displayFormatAttribute.NullDisplayText;
                result.DisplayFormatString = displayFormatAttribute.DataFormatString;
                result.ConvertEmptyStringToNull = displayFormatAttribute.ConvertEmptyStringToNull;
                if (displayFormatAttribute.ApplyFormatInEditMode)
                    result.EditFormatString = displayFormatAttribute.DataFormatString;
            }
            //
            var scaffoldColumnAttribute = attributeList.OfType<ScaffoldColumnAttribute>().FirstOrDefault();
            if (scaffoldColumnAttribute != null)
                result.ShowForDisplay = result.ShowForEdit = scaffoldColumnAttribute.Scaffold;
            //
            var displayNameAttribute = attributeList.OfType<DisplayNameAttribute>().FirstOrDefault();
            if (displayNameAttribute != null)
                result.DisplayName = displayNameAttribute.DisplayName;
            //
            var requiredAttribute = attributeList.OfType<RequiredAttribute>().FirstOrDefault();
            if (requiredAttribute != null)
                result.IsRequired = true;
            #endregion

            result.ViewModifiers = attributeList.OfType<ViewModifierAttributeBase>().Select(c => c.GetViewModifier()).ToList();
            //
            return result;
        }

        protected virtual DataAnnotationsModelMetadataEx CreateMetadata(Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute)
        {
            return new DataAnnotationsModelMetadataEx(this, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute);
        }
    }
}
