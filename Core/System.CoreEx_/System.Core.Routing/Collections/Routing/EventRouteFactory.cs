namespace System.Collections.Routing
{
    /// <summary>
    /// EventRouteFactory
    /// </summary>
    internal static class EventRouteFactory
    {
        private static EventRoute[] _eventRouteStack;
        private static int _stackTop;
        private static object _synchronized = new object();

        /// <summary>
        /// Fetches the object.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <returns></returns>
        internal static EventRoute FetchObject(RoutedEvent routedEvent)
        {
            EventRoute route = Pop();
            if (route == null)
            {
                return new EventRoute(routedEvent);
            }
            route.RoutedEvent = routedEvent;
            return route;
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        /// <returns></returns>
        private static EventRoute Pop()
        {
            lock (_synchronized)
            {
                if (_stackTop > 0)
                {
                    EventRoute route2 = _eventRouteStack[--_stackTop];
                    _eventRouteStack[_stackTop] = null;
                    return route2;
                }
            }
            return null;
        }

        /// <summary>
        /// Pushes the specified event route.
        /// </summary>
        /// <param name="eventRoute">The event route.</param>
        private static void Push(EventRoute eventRoute)
        {
            lock (_synchronized)
            {
                if (_eventRouteStack == null)
                {
                    _eventRouteStack = new EventRoute[2];
                    _stackTop = 0;
                }
                if (_stackTop < 2)
                {
                    _eventRouteStack[_stackTop++] = eventRoute;
                }
            }
        }

        /// <summary>
        /// Recycles the object.
        /// </summary>
        /// <param name="eventRoute">The event route.</param>
        internal static void RecycleObject(EventRoute eventRoute)
        {
            eventRoute.Clear();
            Push(eventRoute);
        }
    }
}