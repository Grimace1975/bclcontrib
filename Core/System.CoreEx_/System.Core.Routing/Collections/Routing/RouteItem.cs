namespace System.Collections.Routing
{
    /// <summary>
    /// RouteItem
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct RouteItem
    {
        private object _target;
        private RoutedEventHandlerInfo _routedEventHandlerInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteItem"/> struct.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="routedEventHandlerInfo">The routed event handler info.</param>
        internal RouteItem(object target, RoutedEventHandlerInfo routedEventHandlerInfo)
        {
            _target = target;
            _routedEventHandlerInfo = routedEventHandlerInfo;
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        internal object Target
        {
            get { return _target; }
        }

        /// <summary>
        /// Invokes the handler.
        /// </summary>
        /// <param name="routedEventArgs">The <see cref="RoutedEvent_.RoutedEventArgs"/> instance containing the event data.</param>
        internal void InvokeHandler(RoutedEventArgs routedEventArgs)
        {
            _routedEventHandlerInfo.InvokeHandler(_target, routedEventArgs);
        }

        /// <summary>
        /// Equalses the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            return Equals((RouteItem)o);
        }

        /// <summary>
        /// Equalses the specified route item.
        /// </summary>
        /// <param name="routeItem">The route item.</param>
        /// <returns></returns>
        public bool Equals(RouteItem routeItem)
        {
            return ((routeItem._target == _target) && (routeItem._routedEventHandlerInfo == _routedEventHandlerInfo));
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
        /// <param name="routeItem1">The route item1.</param>
        /// <param name="routeItem2">The route item2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RouteItem routeItem1, RouteItem routeItem2)
        {
            return routeItem1.Equals(routeItem2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="routeItem1">The route item1.</param>
        /// <param name="routeItem2">The route item2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RouteItem routeItem1, RouteItem routeItem2)
        {
            return !routeItem1.Equals(routeItem2);
        }
    }
}