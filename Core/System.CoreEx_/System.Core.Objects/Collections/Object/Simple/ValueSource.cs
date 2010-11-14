namespace System.Collections.Object.Simple
{
    /// <summary>
    /// ValueSource
    /// </summary>
    public class ValueSource
    {
        /// <summary>
        /// BaseValueSourceInternal
        /// </summary>
        internal enum Base : short
        {
            /// <summary>
            /// Default
            /// </summary>
            Default = 1,
            /// <summary>
            /// ImplicitReference
            /// </summary>
            ImplicitReference = 8,
            /// <summary>
            /// Inherited
            /// </summary>
            Inherited = 2,
            /// <summary>
            /// Local
            /// </summary>
            Local = 11,
            /// <summary>
            /// ParentTemplate
            /// </summary>
            ParentTemplate = 9,
            /// <summary>
            /// ParentTemplateTrigger
            /// </summary>
            ParentTemplateTrigger = 10,
            /// <summary>
            /// Style
            /// </summary>
            Style = 5,
            /// <summary>
            /// StyleTrigger
            /// </summary>
            StyleTrigger = 7,
            /// <summary>
            /// TemplateTrigger
            /// </summary>
            TemplateTrigger = 6,
            /// <summary>
            /// ThemeStyle
            /// </summary>
            ThemeStyle = 3,
            /// <summary>
            /// ThemeStyleTrigger
            /// </summary>
            ThemeStyleTrigger = 4,
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0
        }

        /// <summary>
        /// FullValueSource
        /// </summary>
        internal enum Full : short
        {
            /// <summary>
            /// HasExpressionMarker
            /// </summary>
            HasExpressionMarker = 0x100,
            /// <summary>
            /// IsAnimated
            /// </summary>
            IsAnimated = 0x20,
            /// <summary>
            /// IsCoerced
            /// </summary>
            IsCoerced = 0x40,
            /// <summary>
            /// IsDeferredReference
            /// </summary>
            IsDeferredReference = 0x80,
            /// <summary>
            /// IsExpression
            /// </summary>
            IsExpression = 0x10,
            /// <summary>
            /// ModifiersMask
            /// </summary>
            ModifiersMask = 0x70,
            /// <summary>
            /// ValueSourceMask
            /// </summary>
            ValueSourceMask = 15
        }
    }
}