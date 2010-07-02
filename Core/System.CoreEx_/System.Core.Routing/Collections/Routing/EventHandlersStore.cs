using System.Collections.Frugal;
namespace System.Collections.Routing
{
    /// <summary>
    /// EventHandlersStore
    /// </summary>
    internal class EventHandlersStore
    {
        private FrugalMap _entries;
        private static FrugalMapIterationCallback _iterationCallback = new FrugalMapIterationCallback(EventHandlersStore.OnEventHandlersIterationCallback);

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlersStore"/> class.
        /// </summary>
        public EventHandlersStore()
        {
            _entries = new FrugalMap();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlersStore"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public EventHandlersStore(EventHandlersStore source)
        {
            _entries = source._entries;
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        public void Add(EventPrivateKey key, System.Delegate handler)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            System.Delegate a = this[key];
            if (a == null)
            {
                _entries[key.GlobalIndex] = handler;
            }
            else
            {
                _entries[key.GlobalIndex] = System.Delegate.Combine(a, handler);
            }
        }

        /// <summary>
        /// Adds the routed event handler.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        public void AddRoutedEventHandler(RoutedEvent routedEvent, System.Delegate handler, bool handledEventsToo)
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
            RoutedEventHandlerInfo info = new RoutedEventHandlerInfo(handler, handledEventsToo);
            FrugalObjectList<RoutedEventHandlerInfo> list = this[routedEvent];
            if (list == null)
            {
                _entries[routedEvent.GlobalIndex] = list = new FrugalObjectList<RoutedEventHandlerInfo>(1);
            }
            list.Add(info);
        }

        /// <summary>
        /// Determines whether [contains] [the specified routed event].
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified routed event]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(RoutedEvent routedEvent)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            FrugalObjectList<RoutedEventHandlerInfo> list = this[routedEvent];
            return ((list != null) && (list.Count != 0));
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public System.Delegate Get(EventPrivateKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this[key];
        }

        /// <summary>
        /// Gets the routed event handlers.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <returns></returns>
        public RoutedEventHandlerInfo[] GetRoutedEventHandlers(RoutedEvent routedEvent)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            FrugalObjectList<RoutedEventHandlerInfo> list = this[routedEvent];
            if (list != null)
            {
                return list.ToArray();
            }
            return null;
        }

        /// <summary>
        /// Called when [event handlers iteration callback].
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void OnEventHandlersIterationCallback(System.Collections.ArrayList list, int key, object value)
        {
            RoutedEvent event2 = GlobalEventManager.EventFromGlobalIndex(key) as RoutedEvent;
            if ((event2 != null) && (((FrugalObjectList<RoutedEventHandlerInfo>)value).Count > 0))
            {
                list.Add(event2);
            }
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        public void Remove(EventPrivateKey key, System.Delegate handler)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            System.Delegate source = this[key];
            if (source != null)
            {
                source = System.Delegate.Remove(source, handler);
                if (source == null)
                {
                    _entries[key.GlobalIndex] = Object.BaseProperty.UnsetValue;
                }
                else
                {
                    _entries[key.GlobalIndex] = source;
                }
            }
        }

        public void RemoveRoutedEventHandler(RoutedEvent routedEvent, System.Delegate handler)
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
            FrugalObjectList<RoutedEventHandlerInfo> list = this[routedEvent];
            if ((list != null) && (list.Count > 0))
            {
                if (list.Count == 1)
                {
                    RoutedEventHandlerInfo info2 = list[0];
                    if (info2.Handler == handler)
                    {
                        _entries[routedEvent.GlobalIndex] = Object.BaseProperty.UnsetValue;
                        return;
                    }
                }
                for (int index = 0; index < list.Count; index++)
                {
                    RoutedEventHandlerInfo info = list[index];
                    if (info.Handler == handler)
                    {
                        list.RemoveAt(index);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        internal int Count
        {
            get { return _entries.Count; }
        }

        /// <summary>
        /// Gets the <see cref="Instinct.Collections.Frugal.FrugalObjectList&lt;Instinct.Collections.Routing.RoutedEventHandlerInfo&gt;"/> with the specified key.
        /// </summary>
        /// <value></value>
        internal FrugalObjectList<RoutedEventHandlerInfo> this[RoutedEvent key]
        {
            get
            {
                object obj2 = _entries[key.GlobalIndex];
                if (obj2 == Object.BaseProperty.UnsetValue)
                {
                    return null;
                }
                return (FrugalObjectList<RoutedEventHandlerInfo>)obj2;
            }
        }

        /// <summary>
        /// Gets the <see cref="Delegate"/> with the specified key.
        /// </summary>
        /// <value></value>
        internal System.Delegate this[EventPrivateKey key]
        {
            get
            {
                object obj2 = _entries[key.GlobalIndex];
                if (obj2 == Object.BaseProperty.UnsetValue)
                {
                    return null;
                }
                return (System.Delegate)obj2;
            }
        }
    }
}