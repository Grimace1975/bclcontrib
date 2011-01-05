namespace System.Collections.Routing
{
    /// <summary>
    /// EventManager
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Gets the name of the routed event from.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        internal static RoutedEvent GetRoutedEventFromName(string name, System.Type ownerType)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            return GlobalEventManager.GetRoutedEventFromName(name, ownerType, true);
        }

        /// <summary>
        /// Gets the routed events.
        /// </summary>
        /// <returns></returns>
        public static RoutedEvent[] GetRoutedEvents()
        {
            return GlobalEventManager.GetRoutedEvents();
        }

        /// <summary>
        /// Gets the routed events for owner.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public static RoutedEvent[] GetRoutedEventsForOwner(System.Type ownerType)
        {
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            return GlobalEventManager.GetRoutedEventsForOwner(ownerType);
        }

        /// <summary>
        /// Registers the class handler.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        public static void RegisterClassHandler(System.Type classType, RoutedEvent routedEvent, System.Delegate handler)
        {
            RegisterClassHandler(classType, routedEvent, handler, false);
        }

        /// <summary>
        /// Registers the class handler.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        public static void RegisterClassHandler(System.Type classType, RoutedEvent routedEvent, System.Delegate handler, bool handledEventsToo)
        {
            if (classType == null)
            {
                throw new ArgumentNullException("classType");
            }
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            if (typeof(EventElement).IsAssignableFrom(classType) == false)
            {
                throw new ArgumentException(TR.Get("ClassTypeIllegal"));
            }
            if (routedEvent.IsLegalHandler(handler) == false)
            {
                throw new ArgumentException(TR.Get("HandlerTypeIllegal"));
            }
            GlobalEventManager.RegisterClassHandler(classType, routedEvent, handler, handledEventsToo);
        }

        /// <summary>
        /// Registers the routed event.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="routingStrategy">The routing strategy.</param>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public static RoutedEvent RegisterRoutedEvent(string name, RoutingStrategy routingStrategy, System.Type handlerType, System.Type ownerType)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (((routingStrategy != RoutingStrategy.Tunnel) && (routingStrategy != RoutingStrategy.Bubble)) && (routingStrategy != RoutingStrategy.Direct))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("routingStrategy", (int)routingStrategy, typeof(RoutingStrategy));
            }
            if (handlerType == null)
            {
                throw new ArgumentNullException("handlerType");
            }
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            if (GlobalEventManager.GetRoutedEventFromName(name, ownerType, false) != null)
            {
                throw new ArgumentException(TR.Get("DuplicateEventName", new object[] { name, ownerType }));
            }
            return GlobalEventManager.RegisterRoutedEvent(name, routingStrategy, handlerType, ownerType);
        }
    }
}