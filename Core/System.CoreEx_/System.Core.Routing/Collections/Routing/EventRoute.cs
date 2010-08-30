using System.ComponentModel;
using System.Collections.Frugal;
namespace System.Collections.Routing
{
    /// <summary>
    /// EventRoute
    /// </summary>
    public class EventRoute
    {
        private System.Collections.Generic.Stack<BranchNode> _branchNodeStack;
        private RoutedEvent _routedEvent;
        private FrugalStructList<RouteItem> _routeItemList = new FrugalStructList<RouteItem>(0x10);
        private FrugalStructList<SourceItem> _sourceItemList = new FrugalStructList<SourceItem>(0x10);

        #region BranchNode
        /// <summary>
        /// BranchNode
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct BranchNode
        {
            public object Node;
            public object Source;
        }
        #endregion BranchNode

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRoute"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        public EventRoute(RoutedEvent routedEvent)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }
            _routedEvent = routedEvent;
        }

        /// <summary>
        /// Adds the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="handledEventsToo">if set to <c>true</c> [handled events too].</param>
        public void Add(object target, System.Delegate handler, bool handledEventsToo)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            RouteItem item = new RouteItem(target, new RoutedEventHandlerInfo(handler, handledEventsToo));
            _routeItemList.Add(item);
        }

        /// <summary>
        /// Adds the source.
        /// </summary>
        /// <param name="source">The source.</param>
        internal void AddSource(object source)
        {
            int startIndex = _routeItemList.Count;
            _sourceItemList.Add(new SourceItem(startIndex, source));
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            _routedEvent = null;
            _routeItemList.Clear();
            if (_branchNodeStack != null)
            {
                _branchNodeStack.Clear();
            }
            _sourceItemList.Clear();
        }

        /// <summary>
        /// Gets the bubble source.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <returns></returns>
        private object GetBubbleSource(int index, out int endIndex)
        {
            if (_sourceItemList.Count == 0)
            {
                endIndex = _routeItemList.Count;
                return null;
            }
            SourceItem item7 = _sourceItemList[0];
            if (index < item7.StartIndex)
            {
                SourceItem item6 = _sourceItemList[0];
                endIndex = item6.StartIndex;
                return null;
            }
            for (int sourceItemIndex = 0; sourceItemIndex < (_sourceItemList.Count - 1); sourceItemIndex++)
            {
                SourceItem item5 = _sourceItemList[sourceItemIndex];
                if (index >= item5.StartIndex)
                {
                    SourceItem item4 = _sourceItemList[sourceItemIndex + 1];
                    if (index < item4.StartIndex)
                    {
                        SourceItem item3 = _sourceItemList[sourceItemIndex + 1];
                        endIndex = item3.StartIndex;
                        SourceItem item2 = _sourceItemList[sourceItemIndex];
                        return item2.Source;
                    }
                }
            }
            endIndex = _routeItemList.Count;
            SourceItem item = _sourceItemList[_sourceItemList.Count - 1];
            return item.Source;
        }

        /// <summary>
        /// Gets the tunnel source.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        private object GetTunnelSource(int index, out int startIndex)
        {
            if (_sourceItemList.Count == 0)
            {
                startIndex = 0;
                return null;
            }
            SourceItem item7 = _sourceItemList[0];
            if (index < item7.StartIndex)
            {
                startIndex = 0;
                return null;
            }
            for (int sourceItemIndex = 0; sourceItemIndex < (_sourceItemList.Count - 1); sourceItemIndex++)
            {
                SourceItem item6 = _sourceItemList[sourceItemIndex];
                if (index >= item6.StartIndex)
                {
                    SourceItem item5 = _sourceItemList[sourceItemIndex + 1];
                    if (index < item5.StartIndex)
                    {
                        SourceItem item4 = _sourceItemList[sourceItemIndex];
                        startIndex = item4.StartIndex;
                        SourceItem item3 = _sourceItemList[sourceItemIndex];
                        return item3.Source;
                    }
                }
            }
            SourceItem item2 = _sourceItemList[_sourceItemList.Count - 1];
            startIndex = item2.StartIndex;
            SourceItem item = _sourceItemList[_sourceItemList.Count - 1];
            return item.Source;
        }

        /// <summary>
        /// Invokes the handlers.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="RoutedEvent_.RoutedEventArgs"/> instance containing the event data.</param>
        internal void InvokeHandlers(object source, RoutedEventArgs args)
        {
            InvokeHandlersImpl(source, args, false);
        }

        /// <summary>
        /// Invokes the handlers impl.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="RoutedEvent_.RoutedEventArgs"/> instance containing the event data.</param>
        /// <param name="reRaised">if set to <c>true</c> [re raised].</param>
        private void InvokeHandlersImpl(object source, RoutedEventArgs args, bool reRaised)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            if (args.Source == null)
            {
                throw new ArgumentException(TR.Get("SourceNotSet"));
            }
            if (args.RoutedEvent != _routedEvent)
            {
                throw new ArgumentException(TR.Get("Mismatched_RoutedEvent"));
            }
            if ((args.RoutedEvent.RoutingStrategy == RoutingStrategy.Bubble) || (args.RoutedEvent.RoutingStrategy == RoutingStrategy.Direct))
            {
                int endIndex = 0;
                for (int routeItemIndex = 0; routeItemIndex < _routeItemList.Count; routeItemIndex++)
                {
                    if (routeItemIndex >= endIndex)
                    {
                        object bubbleSource = GetBubbleSource(routeItemIndex, out endIndex);
                        if (reRaised == false)
                        {
                            args.Source = (bubbleSource == null ? source : bubbleSource);
                        }
                    }
                    //if (TraceRoutedEvent.IsEnabled)
                    //{
                    //    RouteItem item8 = _routeItemList[i];
                    //    TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.InvokeHandlers, item8.Target, args, args.Handled);
                    //}
                    _routeItemList[routeItemIndex].InvokeHandler(args);
                    //if (TraceRoutedEvent.IsEnabled)
                    //{
                    //    RouteItem item6 = _routeItemList[i];
                    //    TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.InvokeHandlers, item6.Target, args, args.Handled);
                    //}
                }
            }
            else
            {
                int num3;
                int count = _routeItemList.Count;
                for (int routeItemIndex = _routeItemList.Count - 1; routeItemIndex >= 0; routeItemIndex = num3)
                {
                    RouteItem item5 = _routeItemList[routeItemIndex];
                    object target = item5.Target;
                    num3 = routeItemIndex;
                    while (num3 >= 0)
                    {
                        RouteItem item4 = _routeItemList[num3];
                        if (item4.Target != target)
                        {
                            break;
                        }
                        num3--;
                    }
                    for (int routeItemIndex2 = num3 + 1; routeItemIndex2 <= routeItemIndex; routeItemIndex2++)
                    {
                        if (routeItemIndex2 < count)
                        {
                            object tunnelSource = GetTunnelSource(routeItemIndex2, out count);
                            args.Source = (tunnelSource == null ? source : tunnelSource);
                        }
                        //if (TraceRoutedEvent.IsEnabled)
                        //{
                        //    RouteItem item3 = _routeItemList[k];
                        //    TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.InvokeHandlers, item3.Target, args, args.Handled);
                        //}
                        _routeItemList[routeItemIndex2].InvokeHandler(args);
                        //if (TraceRoutedEvent.IsEnabled)
                        //{
                        //    RouteItem item = _routeItemList[k];
                        //    TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.InvokeHandlers, item.Target, args, args.Handled);
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// Peeks the branch node.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public object PeekBranchNode()
        {
            if (BranchNodeStack.Count == 0)
            {
                return null;
            }
            return BranchNodeStack.Peek().Node;
        }

        /// <summary>
        /// Peeks the branch source.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public object PeekBranchSource()
        {
            if (BranchNodeStack.Count == 0)
            {
                return null;
            }
            return BranchNodeStack.Peek().Source;
        }

        /// <summary>
        /// Pops the branch node.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public object PopBranchNode()
        {
            if (BranchNodeStack.Count == 0)
            {
                return null;
            }
            return BranchNodeStack.Pop().Node;
        }

        /// <summary>
        /// Pushes the branch node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="source">The source.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void PushBranchNode(object node, object source)
        {
            BranchNode item = new BranchNode
            {
                Node = node,
                Source = source
            };
            BranchNodeStack.Push(item);
        }

        /// <summary>
        /// Res the invoke handlers.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="RoutedEvent_.RoutedEventArgs"/> instance containing the event data.</param>
        internal void ReInvokeHandlers(object source, RoutedEventArgs args)
        {
            InvokeHandlersImpl(source, args, true);
        }

        /// <summary>
        /// Gets the branch node stack.
        /// </summary>
        /// <value>The branch node stack.</value>
        private System.Collections.Generic.Stack<BranchNode> BranchNodeStack
        {
            get
            {
                if (_branchNodeStack == null)
                {
                    _branchNodeStack = new System.Collections.Generic.Stack<BranchNode>(1);
                }
                return _branchNodeStack;
            }
        }

        /// <summary>
        /// Gets or sets the routed event.
        /// </summary>
        /// <value>The routed event.</value>
        internal RoutedEvent RoutedEvent
        {
            get { return _routedEvent; }
            set { _routedEvent = value; }
        }
    }
}