using System.Collections.Frugal;
namespace System.Collections.Routing
{
    /// <summary>
    /// GlobalEventManager
    /// </summary>
    internal static class GlobalEventManager
    {
        private static int _countRoutedEvents = 0;
        private static Object.BaseObjectType _dependencyObjectType = Object.BaseObjectType.FromSystemTypeInternal(typeof(Object.BaseObject));
        private static DTypeMap _dTypedClassListeners = new DTypeMap(100);
        private static DTypeMap _dTypedRoutedEventList = new DTypeMap(10);
        private static System.Collections.ArrayList _globalIndexToEventMap = new System.Collections.ArrayList(100);
        private static System.Collections.Hashtable _ownerTypedRoutedEventList = new System.Collections.Hashtable(10);
        internal static object Synchronized = new object();

        /// <summary>
        /// Adds the owner.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="ownerType">Type of the owner.</param>
        internal static void AddOwner(RoutedEvent routedEvent, System.Type ownerType)
        {
            if ((ownerType == typeof(Object.BaseObject)) || (ownerType.IsSubclassOf(typeof(Object.BaseObject)) == true))
            {
                FrugalObjectList<RoutedEvent> list2;
                Object.BaseObjectType type = Object.BaseObjectType.FromSystemTypeInternal(ownerType);
                object obj3 = _dTypedRoutedEventList[type];
                if (obj3 == null)
                {
                    list2 = new FrugalObjectList<RoutedEvent>(1);
                    _dTypedRoutedEventList[type] = list2;
                }
                else
                {
                    list2 = (FrugalObjectList<RoutedEvent>)obj3;
                }
                if (list2.Contains(routedEvent) == false)
                {
                    list2.Add(routedEvent);
                }
            }
            else
            {
                FrugalObjectList<RoutedEvent> list;
                object obj2 = _ownerTypedRoutedEventList[ownerType];
                if (obj2 == null)
                {
                    list = new FrugalObjectList<RoutedEvent>(1);
                    _ownerTypedRoutedEventList[ownerType] = list;
                }
                else
                {
                    list = (FrugalObjectList<RoutedEvent>)obj2;
                }
                if (list.Contains(routedEvent) == false)
                {
                    list.Add(routedEvent);
                }
            }
        }

        /// <summary>
        /// Events the index of from global.
        /// </summary>
        /// <param name="globalIndex">Index of the global.</param>
        /// <returns></returns>
        internal static object EventFromGlobalIndex(int globalIndex)
        {
            return _globalIndexToEventMap[globalIndex];
        }

        /// <summary>
        /// Gets the D typed class listeners.
        /// </summary>
        /// <param name="dType">Type of the d.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <returns></returns>
        internal static RoutedEventHandlerInfoList GetDTypedClassListeners(Object.BaseObjectType dType, RoutedEvent routedEvent)
        {
            int num;
            ClassHandlersStore store;
            return GetDTypedClassListeners(dType, routedEvent, out store, out num);
        }

        /// <summary>
        /// Gets the D typed class listeners.
        /// </summary>
        /// <param name="dType">Type of the d.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="classListenersLists">The class listeners lists.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal static RoutedEventHandlerInfoList GetDTypedClassListeners(Object.BaseObjectType dType, RoutedEvent routedEvent, out ClassHandlersStore classListenersLists, out int index)
        {
            classListenersLists = (ClassHandlersStore)_dTypedClassListeners[dType];
            if (classListenersLists != null)
            {
                index = classListenersLists.GetHandlersIndex(routedEvent);
                if (index != -1)
                {
                    return classListenersLists.GetExistingHandlers(index);
                }
            }
            lock (Synchronized)
            {
                return GetUpdatedDTypedClassListeners(dType, routedEvent, out classListenersLists, out index);
            }
        }

        /// <summary>
        /// Gets the index of the next available global.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static int GetNextAvailableGlobalIndex(object value)
        {
            lock (Synchronized)
            {
                if (_globalIndexToEventMap.Count >= 0x7fffffff)
                {
                    throw new InvalidOperationException(TR.Get("TooManyRoutedEvents"));
                }
                return _globalIndexToEventMap.Add(value);
            }
        }

        /// <summary>
        /// Gets the name of the routed event from.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <param name="includeSupers">if set to <c>true</c> [include supers].</param>
        /// <returns></returns>
        internal static RoutedEvent GetRoutedEventFromName(string name, System.Type ownerType, bool includeSupers)
        {
            if ((ownerType != typeof(Object.BaseObject)) && (ownerType.IsSubclassOf(typeof(Object.BaseObject)) == false))
            {
                while (ownerType != null)
                {
                    FrugalObjectList<RoutedEvent> list = (FrugalObjectList<RoutedEvent>)_ownerTypedRoutedEventList[ownerType];
                    if (list != null)
                    {
                        for (int index = 0; index < list.Count; index++)
                        {
                            RoutedEvent event2 = list[index];
                            if (event2.Name.Equals(name) == true)
                            {
                                return event2;
                            }
                        }
                    }
                    ownerType = (includeSupers ? ownerType.BaseType : null);
                }
            }
            else
            {
                for (Object.BaseObjectType type = Object.BaseObjectType.FromSystemTypeInternal(ownerType); type != null; type = includeSupers ? type.BaseType : null)
                {
                    FrugalObjectList<RoutedEvent> list2 = (FrugalObjectList<RoutedEvent>)_dTypedRoutedEventList[type];
                    if (list2 != null)
                    {
                        for (int index = 0; index < list2.Count; index++)
                        {
                            RoutedEvent event3 = list2[index];
                            if (event3.Name.Equals(name) == true)
                            {
                                return event3;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the routed events.
        /// </summary>
        /// <returns></returns>
        internal static RoutedEvent[] GetRoutedEvents()
        {
            RoutedEvent[] eventArray;
            lock (Synchronized)
            {
                eventArray = new RoutedEvent[_countRoutedEvents];
                ItemStructList<Object.BaseObjectType> activeDTypes = _dTypedRoutedEventList.ActiveDTypes;
                int num4 = 0;
                for (int activeListIndex = 0; activeListIndex < activeDTypes.Count; activeListIndex++)
                {
                    FrugalObjectList<RoutedEvent> list2 = (FrugalObjectList<RoutedEvent>)_dTypedRoutedEventList[activeDTypes.List[activeListIndex]];
                    for (int listIndex = 0; listIndex < list2.Count; listIndex++)
                    {
                        RoutedEvent event3 = list2[listIndex];
                        if (System.Array.IndexOf<RoutedEvent>(eventArray, event3) < 0)
                        {
                            eventArray[num4++] = event3;
                        }
                    }
                }
                System.Collections.IDictionaryEnumerator enumerator = _ownerTypedRoutedEventList.GetEnumerator();
                while (enumerator.MoveNext() == true)
                {
                    FrugalObjectList<RoutedEvent> list = (FrugalObjectList<RoutedEvent>)enumerator.Value;
                    for (int listIndex = 0; listIndex < list.Count; listIndex++)
                    {
                        RoutedEvent event2 = list[listIndex];
                        if (System.Array.IndexOf<RoutedEvent>(eventArray, event2) < 0)
                        {
                            eventArray[num4++] = event2;
                        }
                    }
                }
            }
            return eventArray;
        }

        /// <summary>
        /// Gets the routed events for owner.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        internal static RoutedEvent[] GetRoutedEventsForOwner(System.Type ownerType)
        {
            if ((ownerType == typeof(Object.BaseObject)) || (ownerType.IsSubclassOf(typeof(Object.BaseObject)) == true))
            {
                Object.BaseObjectType type = Object.BaseObjectType.FromSystemTypeInternal(ownerType);
                FrugalObjectList<RoutedEvent> list2 = (FrugalObjectList<RoutedEvent>)_dTypedRoutedEventList[type];
                if (list2 != null)
                {
                    return list2.ToArray();
                }
            }
            else
            {
                FrugalObjectList<RoutedEvent> list = (FrugalObjectList<RoutedEvent>)_ownerTypedRoutedEventList[ownerType];
                if (list != null)
                {
                    return list.ToArray();
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the updated D typed class listeners.
        /// </summary>
        /// <param name="dType">Type of the d.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="classListenersLists">The class listeners lists.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private static RoutedEventHandlerInfoList GetUpdatedDTypedClassListeners(Object.BaseObjectType dType, RoutedEvent routedEvent, out ClassHandlersStore classListenersLists, out int index)
        {
            classListenersLists = (ClassHandlersStore)_dTypedClassListeners[dType];
            if (classListenersLists != null)
            {
                index = classListenersLists.GetHandlersIndex(routedEvent);
                if (index != -1)
                {
                    return classListenersLists.GetExistingHandlers(index);
                }
            }
            Object.BaseObjectType baseType = dType;
            ClassHandlersStore store = null;
            RoutedEventHandlerInfoList handlers = null;
            int handlersIndex = -1;
            while ((handlersIndex == -1) && (baseType.Id != _dependencyObjectType.Id))
            {
                baseType = baseType.BaseType;
                store = (ClassHandlersStore)_dTypedClassListeners[baseType];
                if (store != null)
                {
                    handlersIndex = store.GetHandlersIndex(routedEvent);
                    if (handlersIndex != -1)
                    {
                        handlers = store.GetExistingHandlers(handlersIndex);
                    }
                }
            }
            if (classListenersLists == null)
            {
                //if ((dType.SystemType == typeof(UIElement)) || (dType.SystemType == typeof(ContentElement)))
                //{
                //    classListenersLists = new ClassHandlersStore(80);
                //}
                //else
                //{
                classListenersLists = new ClassHandlersStore(1);
                //}
                _dTypedClassListeners[dType] = classListenersLists;
            }
            index = classListenersLists.CreateHandlersLink(routedEvent, handlers);
            return handlers;
        }

        /// <summary>
        /// Registers the class handler.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        internal static void RegisterClassHandler(System.Type classType, RoutedEvent routedEvent, System.Delegate handler, bool handledEventsToo)
        {
            ClassHandlersStore store;
            int num2;
            Object.BaseObjectType dType = Object.BaseObjectType.FromSystemTypeInternal(classType);
            GetDTypedClassListeners(dType, routedEvent, out store, out num2);
            lock (Synchronized)
            {
                RoutedEventHandlerInfoList baseClassListeners = store.AddToExistingHandlers(num2, handler, handledEventsToo);
                ItemStructList<Object.BaseObjectType> activeDTypes = _dTypedClassListeners.ActiveDTypes;
                for (int activeListIndex = 0; activeListIndex < activeDTypes.Count; activeListIndex++)
                {
                    if (activeDTypes.List[activeListIndex].IsSubclassOf(dType))
                    {
                        ((ClassHandlersStore)_dTypedClassListeners[activeDTypes.List[activeListIndex]]).UpdateSubClassHandlers(routedEvent, baseClassListeners);
                    }
                }
            }
        }

        /// <summary>
        /// Registers the routed event.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="routingStrategy">The routing strategy.</param>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        internal static RoutedEvent RegisterRoutedEvent(string name, RoutingStrategy routingStrategy, System.Type handlerType, System.Type ownerType)
        {
            lock (Synchronized)
            {
                RoutedEvent routedEvent = new RoutedEvent(name, routingStrategy, handlerType, ownerType);
                _countRoutedEvents++;
                AddOwner(routedEvent, ownerType);
                return routedEvent;
            }
        }
    }
}