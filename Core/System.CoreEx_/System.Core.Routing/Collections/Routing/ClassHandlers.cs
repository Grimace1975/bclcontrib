namespace System.Collections.Routing
{
    /// <summary>
    /// ClassHandlers
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct ClassHandlers
    {
        internal RoutedEvent RoutedEvent;
        internal RoutedEventHandlerInfoList Handlers;
        internal bool HasSelfHandlers;

        /// <summary>
        /// Equalses the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            return Equals((ClassHandlers)o);
        }

        /// <summary>
        /// Equalses the specified class handlers.
        /// </summary>
        /// <param name="classHandlers">The class handlers.</param>
        /// <returns></returns>
        public bool Equals(ClassHandlers classHandlers)
        {
            return ((classHandlers.RoutedEvent == RoutedEvent) && (classHandlers.Handlers == Handlers));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="classHandlers1">The class handlers1.</param>
        /// <param name="classHandlers2">The class handlers2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ClassHandlers classHandlers1, ClassHandlers classHandlers2)
        {
            return classHandlers1.Equals(classHandlers2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="classHandlers1">The class handlers1.</param>
        /// <param name="classHandlers2">The class handlers2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ClassHandlers classHandlers1, ClassHandlers classHandlers2)
        {
            return !classHandlers1.Equals(classHandlers2);
        }
    }
}