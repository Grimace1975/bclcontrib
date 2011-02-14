namespace System.Collections.Routing
{
    //[ValueSerializer("System.Windows.Markup.RoutedEventValueSerializer, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"), System.ComponentModel.TypeConverter("System.Windows.Markup.RoutedEventConverter, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
    /// <summary>
    /// RoutedEvent
    /// </summary>
    public sealed class RoutedEvent
    {
        private int _globalIndex;
        private System.Type _handlerType;
        private string _name;
        private System.Type _ownerType;
        private RoutingStrategy _routingStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutedEvent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="routingStrategy">The routing strategy.</param>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="ownerType">Type of the owner.</param>
        internal RoutedEvent(string name, RoutingStrategy routingStrategy, System.Type handlerType, System.Type ownerType)
        {
            _name = name;
            _routingStrategy = routingStrategy;
            _handlerType = handlerType;
            _ownerType = ownerType;
            _globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
        }

        /// <summary>
        /// Adds the owner.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public RoutedEvent AddOwner(System.Type ownerType)
        {
            GlobalEventManager.AddOwner(this, ownerType);
            return this;
        }

        /// <summary>
        /// Determines whether [is legal handler] [the specified handler].
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        /// 	<c>true</c> if [is legal handler] [the specified handler]; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsLegalHandler(System.Delegate handler)
        {
            System.Type type = handler.GetType();
            if (type != HandlerType)
            {
                return (type == typeof(RoutedEventHandler));
            }
            return true;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}", new object[] { _ownerType.Name, _name });
        }

        /// <summary>
        /// Gets the index of the global.
        /// </summary>
        /// <value>The index of the global.</value>
        internal int GlobalIndex
        {
            get { return _globalIndex; }
        }

        /// <summary>
        /// Gets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        public System.Type HandlerType
        {
            get { return _handlerType; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the type of the owner.
        /// </summary>
        /// <value>The type of the owner.</value>
        public System.Type OwnerType
        {
            get { return _ownerType; }
        }

        /// <summary>
        /// Gets the routing strategy.
        /// </summary>
        /// <value>The routing strategy.</value>
        public RoutingStrategy RoutingStrategy
        {
            get { return _routingStrategy; }
        }
    }
}