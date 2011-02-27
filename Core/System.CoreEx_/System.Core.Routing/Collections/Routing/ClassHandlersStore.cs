using System.Collections.Frugal;
namespace System.Collections.Routing
{
    /// <summary>
    /// ClassHandlersStore
    /// </summary>
    internal class ClassHandlersStore
    {
        private ItemStructList<ClassHandlers> _eventHandlersList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassHandlersStore"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        internal ClassHandlersStore(int size)
        {
            _eventHandlersList = new ItemStructList<ClassHandlers>(size);
        }

        /// <summary>
        /// Adds to existing handlers.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        /// <returns></returns>
        internal RoutedEventHandlerInfoList AddToExistingHandlers(int index, System.Delegate handler, bool handledEventsToo)
        {
            RoutedEventHandlerInfo info = new RoutedEventHandlerInfo(handler, handledEventsToo);
            RoutedEventHandlerInfoList handlers = _eventHandlersList.List[index].Handlers;
            if ((handlers == null) || (_eventHandlersList.List[index].HasSelfHandlers == false))
            {
                handlers = new RoutedEventHandlerInfoList
                {
                    Handlers = new RoutedEventHandlerInfo[1]
                };
                handlers.Handlers[0] = info;
                handlers.Next = _eventHandlersList.List[index].Handlers;
                _eventHandlersList.List[index].Handlers = handlers;
                _eventHandlersList.List[index].HasSelfHandlers = true;
                return handlers;
            }
            int length = handlers.Handlers.Length;
            RoutedEventHandlerInfo[] destinationArray = new RoutedEventHandlerInfo[length + 1];
            System.Array.Copy(handlers.Handlers, 0, destinationArray, 0, length);
            destinationArray[length] = info;
            handlers.Handlers = destinationArray;
            return handlers;
        }

        /// <summary>
        /// Creates the handlers link.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="handlers">The handlers.</param>
        /// <returns></returns>
        internal int CreateHandlersLink(RoutedEvent routedEvent, RoutedEventHandlerInfoList handlers)
        {
            ClassHandlers item = new ClassHandlers
            {
                RoutedEvent = routedEvent,
                Handlers = handlers,
                HasSelfHandlers = false
            };
            _eventHandlersList.Add(item);
            return (_eventHandlersList.Count - 1);
        }

        /// <summary>
        /// Gets the existing handlers.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal RoutedEventHandlerInfoList GetExistingHandlers(int index)
        {
            return _eventHandlersList.List[index].Handlers;
        }

        /// <summary>
        /// Gets the index of the handlers.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <returns></returns>
        internal int GetHandlersIndex(RoutedEvent routedEvent)
        {
            for (int eventHandlersIndex = 0; eventHandlersIndex < _eventHandlersList.Count; eventHandlersIndex++)
            {
                if (_eventHandlersList.List[eventHandlersIndex].RoutedEvent == routedEvent)
                {
                    return eventHandlersIndex;
                }
            }
            return -1;
        }

        /// <summary>
        /// Updates the sub class handlers.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="baseClassListeners">The base class listeners.</param>
        internal void UpdateSubClassHandlers(RoutedEvent routedEvent, RoutedEventHandlerInfoList baseClassListeners)
        {
            int handlersIndex = GetHandlersIndex(routedEvent);
            if (handlersIndex != -1)
            {
                bool hasSelfHandlers = _eventHandlersList.List[handlersIndex].HasSelfHandlers;
                RoutedEventHandlerInfoList handlers = (hasSelfHandlers ? _eventHandlersList.List[handlersIndex].Handlers.Next : _eventHandlersList.List[handlersIndex].Handlers);
                bool flag = false;
                if (handlers != null)
                {
                    if ((baseClassListeners.Next != null) && (baseClassListeners.Next.Contains(handlers) == true))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag == true)
                {
                    if (hasSelfHandlers == true)
                    {
                        _eventHandlersList.List[handlersIndex].Handlers.Next = baseClassListeners;
                    }
                    else
                    {
                        _eventHandlersList.List[handlersIndex].Handlers = baseClassListeners;
                    }
                }
            }
        }
    }
}