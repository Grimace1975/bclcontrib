namespace System.Collections.Object.Simple
{
    /// <summary>
    /// ModifiedValue
    /// </summary>
    internal class ModifiedValue
    {
        private object _animatedValue;
        private object _baseValue;
        private object _coercedValue;
        private object _expressionValue;

        /// <summary>
        /// Gets or sets the animated value.
        /// </summary>
        /// <value>The animated value.</value>
        internal object AnimatedValue
        {
            get { return _animatedValue; }
            set { _animatedValue = value; }
        }

        /// <summary>
        /// Gets or sets the base value.
        /// </summary>
        /// <value>The base value.</value>
        internal object BaseValue
        {
            get { return _baseValue; }
            set { _baseValue = value; }
        }

        /// <summary>
        /// Gets or sets the coerced value.
        /// </summary>
        /// <value>The coerced value.</value>
        internal object CoercedValue
        {
            get { return _coercedValue; }
            set { _coercedValue = value; }
        }

        /// <summary>
        /// Gets or sets the expression value.
        /// </summary>
        /// <value>The expression value.</value>
        internal object ExpressionValue
        {
            get { return _expressionValue; }
            set { _expressionValue = value; }
        }
    }
}