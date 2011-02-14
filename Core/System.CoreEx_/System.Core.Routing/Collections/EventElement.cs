using System.Collections.Routing;

namespace System.Collections
{
    /// <summary>
    /// EventElement
    /// </summary>
    public class EventElement : Object.BaseObject
    {
        private CoreFlags _flags;
        //+ eventhandlerstore
        internal static readonly Object.UncommonField<EventHandlersStore> EventHandlersStoreField = new Object.UncommonField<EventHandlersStore>();

        /// <summary>
        /// CoreFlags
        /// </summary>
        [System.Flags]
        internal enum CoreFlags : uint
        {
            //AreTransformsClean = 0x800000,
            //ArrangeDirty = 8,
            //ArrangeInProgress = 0x20,
            //ClipToBoundsCache = 2,
            ExistsEventHandlersStore = 0x2000000,
            //HasAutomationPeer = 0x100000,
            //IsCollapsed = 0x200,
            //IsKeyboardFocusWithinCache = 0x400,
            //IsKeyboardFocusWithinChanged = 0x800,
            //IsMouseCaptureWithinCache = 0x4000,
            //IsMouseCaptureWithinChanged = 0x8000,
            //IsMouseOverCache = 0x1000,
            //IsMouseOverChanged = 0x2000,
            //IsOpacitySuppressed = 0x1000000,
            //IsStylusCaptureWithinCache = 0x40000,
            //IsStylusCaptureWithinChanged = 0x80000,
            //IsStylusOverCache = 0x10000,
            //IsStylusOverChanged = 0x20000,
            //IsVisibleCache = 0x400000,
            //MeasureDirty = 4,
            //MeasureDuringArrange = 0x100,
            //MeasureInProgress = 0x10,
            //NeverArranged = 0x80,
            //NeverMeasured = 0x40,
            //RenderingInvalidated = 0x200000,
            //SnapsToDevicePixelsCache = 1
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventElement"/> class.
        /// </summary>
        /// <param name="t">The t.</param>
        public EventElement(object t)
        {
        }

        /// <summary>
        /// Reads the flag.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        internal bool ReadFlag(CoreFlags field)
        {
            return ((_flags & field) != ((CoreFlags)0));
        }

        /// <summary>
        /// Writes the flag.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        internal void WriteFlag(CoreFlags field, bool value)
        {
            if (value == true)
            {
                _flags |= field;
            }
            else
            {
                _flags &= ~field;
            }
        }

        #region EventHandlerStore
        /// <summary>
        /// Gets the event handlers store.
        /// </summary>
        /// <value>The event handlers store.</value>
        internal EventHandlersStore EventHandlersStore
        {
            get
            {
                if (ReadFlag(CoreFlags.ExistsEventHandlersStore) == false)
                {
                    return null;
                }
                return EventHandlersStoreField.GetValue(this);
            }
        }

        //[FriendAccessAllowed]
        /// <summary>
        /// Ensures the event handlers store.
        /// </summary>
        internal void EnsureEventHandlersStore()
        {
            if (EventHandlersStore == null)
            {
                EventHandlersStoreField.SetValue(this, new EventHandlersStore());
                WriteFlag(CoreFlags.ExistsEventHandlersStore, true);
            }
        }
        #endregion EventHandlerStore

        #region Add/Remove Handler
        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        public void AddHandler(RoutedEvent routedEvent, System.Delegate handler)
        {
            AddHandler(routedEvent, handler, false);
        }
        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        internal static void AddHandler(object d, RoutedEvent routedEvent, System.Delegate handler)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            EventElement element2 = (d as EventElement);
            if (element2 != null)
            {
                element2.AddHandler(routedEvent, handler);
            }
            //else
            //{
            //    ContentElement element = d as ContentElement;
            //    if (element != null)
            //    {
            //        element.AddHandler(routedEvent, handler);
            //    }
            //    else
            //    {
            //        UIElement3D elementd = d as UIElement3D;
            //        if (elementd == null)
            //        {
            //            throw new ArgumentException(TR.Get("Invalid_IInputElement", new object[] { d.GetType() }));
            //        }
            //        elementd.AddHandler(routedEvent, handler);
            //    }
            //}
        }
        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        public void AddHandler(RoutedEvent routedEvent, System.Delegate handler, bool handledEventsToo)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            if (routedEvent.IsLegalHandler(handler) == false)
            {
                throw new ArgumentException(TR.Get("HandlerTypeIllegal"));
            }
            EnsureEventHandlersStore();
            EventHandlersStore.AddRoutedEventHandler(routedEvent, handler, handledEventsToo);
            OnAddHandler(routedEvent, handler);
        }


        /// <summary>
        /// Called when [add handler].
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        internal virtual void OnAddHandler(RoutedEvent routedEvent, System.Delegate handler)
        {
        }

        /// <summary>
        /// Called when [remove handler].
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        internal virtual void OnRemoveHandler(RoutedEvent routedEvent, System.Delegate handler)
        {
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        public void RemoveHandler(RoutedEvent routedEvent, System.Delegate handler)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            if (routedEvent.IsLegalHandler(handler) == false)
            {
                throw new ArgumentException(TR.Get("HandlerTypeIllegal"));
            }
            EventHandlersStore eventHandlersStore = EventHandlersStore;
            if (eventHandlersStore != null)
            {
                eventHandlersStore.RemoveRoutedEventHandler(routedEvent, handler);
                OnRemoveHandler(routedEvent, handler);
                if (eventHandlersStore.Count == 0)
                {
                    EventHandlersStoreField.ClearValue(this);
                    WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
                }
            }
        }

        //[FriendAccessAllowed]
        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        internal static void RemoveHandler(object d, RoutedEvent routedEvent, System.Delegate handler)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            EventElement element2 = (d as EventElement);
            if (element2 != null)
            {
                element2.RemoveHandler(routedEvent, handler);
            }
            //else
            //{
            //    ContentElement element = d as ContentElement;
            //    if (element != null)
            //    {
            //        element.RemoveHandler(routedEvent, handler);
            //    }
            //    else
            //    {
            //        UIElement3D elementd = d as UIElement3D;
            //        if (elementd == null)
            //        {
            //            throw new ArgumentException(TR.Get("Invalid_IInputElement", new object[] { d.GetType() }));
            //        }
            //        elementd.RemoveHandler(routedEvent, handler);
            //    }
            //}
        }
        #endregion Add/Remove Handler

        #region RaiseEvent And Route
        /// <summary>
        /// Builds the route helper.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="route">The route.</param>
        /// <param name="args">The <see cref="Instinct.Collections.Routing.RoutedEventArgs"/> instance containing the event data.</param>
        internal static void BuildRoute(object e, EventRoute route, RoutedEventArgs args)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            if (args.Source == null)
            {
                throw new ArgumentException(TR.Get("SourceNotSet"));
            }
            if (args.RoutedEvent != route.RoutedEvent)
            {
                throw new ArgumentException(TR.Get("Mismatched_RoutedEvent"));
            }
            //if (args.RoutedEvent.RoutingStrategy == RoutingStrategy.Direct)
            //{
            //    UIElement element4 = e as UIElement;
            //    ContentElement element3 = null;
            //    UIElement3D elementd2 = null;
            //    if (element4 == null)
            //    {
            //        element3 = e as ContentElement;
            //        if (element3 == null)
            //        {
            //            elementd2 = e as UIElement3D;
            //        }
            //    }
            //    if (element4 != null)
            //    {
            //        element4.AddToEventRoute(route, args);
            //    }
            //    else if (element3 != null)
            //    {
            //        element3.AddToEventRoute(route, args);
            //    }
            //    else if (elementd2 != null)
            //    {
            //        elementd2.AddToEventRoute(route, args);
            //    }
            //}
            //else
            //{
            //    int num = 0;
            //    while (e != null)
            //    {
            //        UIElement element2 = e as UIElement;
            //        ContentElement element = null;
            //        UIElement3D elementd = null;
            //        if (element2 == null)
            //        {
            //            element = e as ContentElement;
            //            if (element == null)
            //            {
            //                elementd = e as UIElement3D;
            //            }
            //        }
            //        if (num++ > 0x1000)
            //        {
            //            throw new InvalidOperationException(TR.Get("TreeLoop"));
            //        }
            //        object source = null;
            //        if (element2 != null)
            //        {
            //            source = element2.AdjustEventSource(args);
            //        }
            //        else if (element != null)
            //        {
            //            source = element.AdjustEventSource(args);
            //        }
            //        else if (elementd != null)
            //        {
            //            source = elementd.AdjustEventSource(args);
            //        }
            //        if (source != null)
            //        {
            //            route.AddSource(source);
            //        }
            //        bool continuePastVisualTree = false;
            //        if (element2 != null)
            //        {
            //            continuePastVisualTree = element2.BuildRouteCore(route, args);
            //            element2.AddToEventRoute(route, args);
            //            e = element2.GetUIParent(continuePastVisualTree);
            //        }
            //        else if (element != null)
            //        {
            //            continuePastVisualTree = element.BuildRouteCore(route, args);
            //            element.AddToEventRoute(route, args);
            //            e = element.GetUIParent(continuePastVisualTree);
            //        }
            //        else if (elementd != null)
            //        {
            //            continuePastVisualTree = elementd.BuildRouteCore(route, args);
            //            elementd.AddToEventRoute(route, args);
            //            e = elementd.GetUIParent(continuePastVisualTree);
            //        }
            //        if (e == args.Source)
            //        {
            //            route.AddSource(e);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public static void RaiseEvent(object sender, RoutedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.ClearUserInitiated();
            RaiseEventImpl(sender, e);
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Instinct.Collections.Routing.RoutedEventArgs"/> instance containing the event data.</param>
        /// <param name="trusted">if set to <c>true</c> [trusted].</param>
        [System.Security.SecurityCritical]
        internal static void RaiseEvent(object sender, RoutedEventArgs args, bool trusted)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            if (trusted == true)
            {
                args.MarkAsUserInitiated();
            }
            else
            {
                args.ClearUserInitiated();
            }
            try
            {
                RaiseEventImpl(sender, args);
            }
            finally
            {
                args.ClearUserInitiated();
            }
        }

        /// <summary>
        /// Raises the event impl.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        internal static void RaiseEventImpl(object sender, RoutedEventArgs args)
        {
            EventRoute route = EventRouteFactory.FetchObject(args.RoutedEvent);
            //if (TraceRoutedEvent.IsEnabled)
            //{
            //    TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.RaiseEvent, new object[] { args.RoutedEvent, sender, args, args.Handled });
            //}
            try
            {
                args.Source = sender;
                BuildRoute(sender, route, args);
                route.InvokeHandlers(sender, args);
                args.Source = args.OriginalSource;
            }
            finally
            {
                //if (TraceRoutedEvent.IsEnabled)
                //{
                //    TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.RaiseEvent, new object[] { args.RoutedEvent, sender, args, args.Handled });
                //}
            }
            EventRouteFactory.RecycleObject(route);
        }
        #endregion RaiseEvent And Route
    }
}