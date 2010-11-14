namespace System.Collections.Routing
{
    /// <summary>
    /// RoutedEventHandlerInfo
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RoutedEventHandlerInfo
    {
        private System.Delegate _handler;
        private bool _handledEventsToo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutedEventHandlerInfo"/> struct.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        internal RoutedEventHandlerInfo(System.Delegate handler, bool handledEventsToo)
        {
            _handler = handler;
            _handledEventsToo = handledEventsToo;
        }

        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <value>The handler.</value>
        public System.Delegate Handler
        {
            get { return _handler; }
        }

        /// <summary>
        /// Gets a value indicating whether [invoke handled events too].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [invoke handled events too]; otherwise, <c>false</c>.
        /// </value>
        public bool InvokeHandledEventsToo
        {
            get { return _handledEventsToo; }
        }

        /// <summary>
        /// Invokes the handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEvent_.RoutedEventArgs"/> instance containing the event data.</param>
        internal void InvokeHandler(object target, RoutedEventArgs routedEventArgs)
        {
            if ((routedEventArgs.Handled == false) || (_handledEventsToo == true))
            {
                if (_handler is RoutedEventHandler)
                {
                    ((RoutedEventHandler)_handler)(target, routedEventArgs);
                }
                else
                {
                    routedEventArgs.InvokeHandler(_handler, target);
                }
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj is RoutedEventHandlerInfo)) && Equals((RoutedEventHandlerInfo)obj));
        }

        /// <summary>
        /// Equalses the specified handler info.
        /// </summary>
        /// <param name="handlerInfo">The handler info.</param>
        /// <returns></returns>
        public bool Equals(RoutedEventHandlerInfo handlerInfo)
        {
            return ((_handler == handlerInfo._handler) && (_handledEventsToo == handlerInfo._handledEventsToo));
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
        /// <param name="handlerInfo1">The handler info1.</param>
        /// <param name="handlerInfo2">The handler info2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
        {
            return handlerInfo1.Equals(handlerInfo2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="handlerInfo1">The handler info1.</param>
        /// <param name="handlerInfo2">The handler info2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
        {
            return !handlerInfo1.Equals(handlerInfo2);
        }
    }
}