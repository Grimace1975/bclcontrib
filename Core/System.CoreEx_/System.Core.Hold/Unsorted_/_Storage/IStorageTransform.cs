//namespace System.Patterns.Storage
//{
//    /// <summary>
//    /// Provides a simplified generic contract for classes that provide data conversion functionality.
//    /// </summary>
//    /// <remarks>
//    /// Classes that currently implement IStorageTransform include:
//    /// <list type="bullet">
//    /// 		<item><see cref="Instinct.DataType.BitDataType"/></item>
//    /// 		<item><see cref="Instinct.DataType.DateDataType"/></item>
//    /// 		<item><see cref="Instinct.DataType.MoneyDataType"/></item>
//    /// 		<item><see cref="Instinct.DataType.RealDataType"/></item>
//    /// 		<item><see cref="Instinct.DataType.SequenceDataType"/></item>
//    /// 		<item><see cref="Instinct.DataType.TimeDataType"/></item>
//    /// 	</list>
//    /// </remarks>
//    [CodeVersion(CodeVersionKind.Instinct, "1.0")]
//    public interface IStorageTransform
//    {
//        /// <summary>
//        /// When implemented, used to transform data being imported into storage.
//        /// </summary>
//        /// <param name="value">The value to be transformed</param>
//        /// <returns>
//        /// Specified <c>value</c> transformed by implementation of method.
//        /// </returns>
//        string TransformIn(string value);
//        /// <summary>
//        /// When implemented, used to transform data being imported into storage.
//        /// </summary>
//        /// <param name="value">The value to be transformed</param>
//        /// <returns>
//        /// Specified <c>value</c> transformed by implementation of method.
//        /// </returns>
//        string TransformIn(object value);
//        /// <summary>
//        /// When implemented, used to transform data being exported out of storage.
//        /// </summary>
//        /// <param name="value">The value to be transformed</param>
//        /// <returns>
//        /// Specified <c>value</c> transformed by implementation of method.
//        /// </returns>
//        object TransformOut(string value);
//    }
//}
