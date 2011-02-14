#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
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
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var attributeList = new List<Attribute>(attributes);
            // field modifiers
            var labelViewModifiers = attributeList.OfType<FieldViewModifierAttributeBase>().Select(c => c.GetLabelViewModifier()).Where(x => x != null).ToList();
            if (labelViewModifiers.Count > 0)
                metadata.SetMany<ILabelViewModifier>(labelViewModifiers);
            var inputViewModifiers = attributeList.OfType<FieldViewModifierAttributeBase>().Select(c => c.GetInputViewModifier()).Where(x => x != null).ToList();
            if (inputViewModifiers.Count > 0)
                metadata.SetMany<IInputViewModifier>(inputViewModifiers);
            // metadata modifiers
            var arguments = new ModelMetadataModifierArguments
            {
                Attributes = attributes,
                ContainerType = containerType,
                ModelAccessor = modelAccessor,
                ModelType = modelType,
                PropertyName = propertyName,
            };
            foreach (var modifier in attributeList.OfType<ModelMetadataModifierAttributeBase>().Select(x => x.Modifier))
                modifier(metadata, arguments);
            return metadata;
        }
    }
}
