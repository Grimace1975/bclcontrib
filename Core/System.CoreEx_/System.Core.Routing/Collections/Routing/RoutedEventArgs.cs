namespace System.Collections.Routing
{
    /// <summary>
    /// RoutedEventArgs
    /// </summary>
    public class RoutedEventArgs : System.EventArgs
    {
        [System.Security.SecurityCritical]
        private System.Collections.Specialized.BitVector32 _flags;
        private object _originalSource;
        private RoutedEvent _routedEvent;
        private object _source;
        private const int HandledIndex = 1;
        private const int InvokingHandlerIndex = 4;
        private const int UserInitiatedIndex = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutedEventArgs"/> class.
        /// </summary>
        public RoutedEventArgs()
        {
        }
        /// Initializes a new instance of the <see cref="RoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        public RoutedEventArgs(RoutedEvent routedEvent)
            : this(routedEvent, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="source">The source.</param>
        public RoutedEventArgs(RoutedEvent routedEvent, object source)
        {
            _routedEvent = routedEvent;
            _source = _originalSource = source;
        }

        /// <summary>
        /// Clears the user initiated.
        /// </summary>
        [System.Security.SecurityCritical, System.Security.SecurityTreatAsSafe]
        internal void ClearUserInitiated()
        {
            _flags[2] = false;
        }

        /// <summary>
        /// Invokes the event handler.
        /// </summary>
        /// <param name="genericHandler">The generic handler.</param>
        /// <param name="genericTarget">The generic target.</param>
        protected virtual void InvokeEventHandler(System.Delegate genericHandler, object genericTarget)
        {
            if (genericHandler == null)
            {
                throw new ArgumentNullException("genericHandler");
            }
            if (genericTarget == null)
            {
                throw new ArgumentNullException("genericTarget");
            }
            if (_routedEvent == null)
            {
                throw new InvalidOperationException(TR.Get("RoutedEventArgsMustHaveRoutedEvent"));
            }
            InvokingHandler = true;
            try
            {
                if (genericHandler is RoutedEventHandler)
                {
                    ((RoutedEventHandler)genericHandler)(genericTarget, this);
                }
                else
                {
                    genericHandler.DynamicInvoke(new object[] { genericTarget, this });
                }
            }
            finally
            {
                InvokingHandler = false;
            }
        }

        /// <summary>
        /// Invokes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="target">The target.</param>
        internal void InvokeHandler(System.Delegate handler, object target)
        {
            InvokingHandler = true;
            try
            {
                InvokeEventHandler(handler, target);
            }
            finally
            {
                InvokingHandler = false;
            }
        }

        /// <summary>
        /// Marks as user initiated.
        /// </summary>
        [System.Security.SecurityCritical]
        internal void MarkAsUserInitiated()
        {
            _flags[2] = true;
        }

        /// <summary>
        /// Called when [set source].
        /// </summary>
        /// <param name="source">The source.</param>
        protected virtual void OnSetSource(object source)
        {
        }

        /// <summary>
        /// Overrides the routed event.
        /// </summary>
        /// <param name="newRoutedEvent">The new routed event.</param>
        internal void OverrideRoutedEvent(RoutedEvent newRoutedEvent)
        {
            _routedEvent = newRoutedEvent;
        }

        /// <summary>
        /// Overrides the source.
        /// </summary>
        /// <param name="source">The source.</param>
        internal void OverrideSource(object source)
        {
            _source = source;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RoutedEventArgs"/> is handled.
        /// </summary>
        /// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
        public bool Handled
        {
            [System.Security.SecurityCritical]
            get { return _flags[1]; }
            [System.Security.SecurityCritical]
            set
            {
                if (_routedEvent == null)
                {
                    throw new InvalidOperationException(TR.Get("RoutedEventArgsMustHaveRoutedEvent"));
                }
                //if (TraceRoutedEvent.IsEnabled)
                //{
                //    TraceRoutedEvent.TraceActivityItem(TraceRoutedEvent.HandleEvent, new object[] { value, RoutedEvent.OwnerType.Name, RoutedEvent.Name, this });
                //}
                _flags[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [invoking handler].
        /// </summary>
        /// <value><c>true</c> if [invoking handler]; otherwise, <c>false</c>.</value>
        private bool InvokingHandler
        {
            [System.Security.SecurityCritical, System.Security.SecurityTreatAsSafe]
            get { return _flags[4]; }
            [System.Security.SecurityTreatAsSafe, System.Security.SecurityCritical]
            set { _flags[4] = value; }
        }

        /// <summary>
        /// Gets the original source.
        /// </summary>
        /// <value>The original source.</value>
        public object OriginalSource
        {
            get { return _originalSource; }
        }

        /// <summary>
        /// Gets or sets the routed event.
        /// </summary>
        /// <value>The routed event.</value>
        public RoutedEvent RoutedEvent
        {
            get { return _routedEvent; }
            set
            {
                if ((UserInitiated == true) && (InvokingHandler == true))
                {
                    throw new InvalidOperationException(TR.Get("RoutedEventCannotChangeWhileRouting"));
                }
                _routedEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public object Source
        {
            get { return _source; }
            set
            {
                if ((UserInitiated == true) && (InvokingHandler == true))
                {
                    throw new InvalidOperationException(TR.Get("RoutedEventCannotChangeWhileRouting"));
                }
                if (_routedEvent == null)
                {
                    throw new InvalidOperationException(TR.Get("RoutedEventArgsMustHaveRoutedEvent"));
                }
                object source = value;
                if ((_source == null) && (_originalSource == null))
                {
                    _source = _originalSource = source;
                    OnSetSource(source);
                }
                else if (_source != source)
                {
                    _source = source;
                    OnSetSource(source);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether [user initiated].
        /// </summary>
        /// <value><c>true</c> if [user initiated]; otherwise, <c>false</c>.</value>
        internal bool UserInitiated
        {
            [System.Security.SecurityTreatAsSafe, System.Security.SecurityCritical]
            get { return _flags[2]; }
        }
    }
}