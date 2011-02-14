namespace System.Collections.Object.Simple
{
    /// <summary>
    /// DeferredReference
    /// </summary>
    internal abstract class DeferredReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredReference"/> class.
        /// </summary>
        protected DeferredReference() { }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="valueSource">The value source.</param>
        /// <returns></returns>
        internal abstract object GetValue(ValueSource.Base valueSource);

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <returns></returns>
        internal abstract Type GetValueType();
    }
}