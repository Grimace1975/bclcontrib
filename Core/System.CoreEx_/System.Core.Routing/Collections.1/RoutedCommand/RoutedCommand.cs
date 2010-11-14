//namespace Instinct.Collections.RoutedCommand
//{
//    /// <summary>
//    /// RoutedCommand
//    /// </summary>
//    public class RoutedCommand : ICommand
//    {
//        private byte _commandId;
//        private string _name;
//        private System.Type _ownerType;
//        private System.Collections.Specialized.BitVector32 _privateVector = new System.Collections.Specialized.BitVector32();

//        #region Class Types
//        /// <summary>
//        /// PrivateFlags
//        /// </summary>
//        private enum PrivateVectorIndex : byte
//        {
//            /// <summary>
//            /// IsBlockedByRM
//            /// </summary>
//            IsBlockedByRM = 1
//        }
//        #endregion Class Types

//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedCommand"/> class.
//        /// </summary>
//        public RoutedCommand()
//        {
//            _name = string.Empty;
//            _ownerType = null;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedCommand"/> class.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="ownerType">Type of the owner.</param>
//        public RoutedCommand(string name, System.Type ownerType)
//        {
//            if ((name == null) || (name.Length == 0))
//            {
//                throw new ArgumentNullException("name");
//            }
//            if (ownerType == null)
//            {
//                throw new ArgumentNullException("ownerType");
//            }
//            _name = name;
//            _ownerType = ownerType;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedCommand"/> class.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="ownerType">Type of the owner.</param>
//        /// <param name="commandId">The command id.</param>
//        internal RoutedCommand(string name, System.Type ownerType, byte commandId)
//            : this(name, ownerType)
//        {
//            _commandId = commandId;
//        }

//        /// <summary>
//        /// Occurs when [can execute changed].
//        /// </summary>
//        public event System.EventHandler CanExecuteChanged
//        {
//            add { CommandManager.RequerySuggested += value; }
//            remove { CommandManager.RequerySuggested -= value; }
//        }

//        /// <summary>
//        /// Determines whether this instance can execute the specified parameter.
//        /// </summary>
//        /// <param name="parameter">The parameter.</param>
//        /// <param name="target">The target.</param>
//        /// <returns>
//        /// 	<c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
//        /// </returns>
//        public bool CanExecute(object parameter, IInputElement target)
//        {
//            bool flag;
//            return CriticalCanExecute(parameter, target, false, out flag);
//        }

//        private bool CanExecuteImpl(object parameter, IInputElement target, bool trusted, out bool continueRouting)
//        {
//            if ((target != null) && !this.IsBlockedByRM)
//            {
//                CanExecuteRoutedEventArgs args = new CanExecuteRoutedEventArgs(this, parameter);
//                args.RoutedEvent = CommandManager.PreviewCanExecuteEvent;
//                this.CriticalCanExecuteWrapper(parameter, target, trusted, args);
//                if (!args.Handled)
//                {
//                    args.RoutedEvent = CommandManager.CanExecuteEvent;
//                    this.CriticalCanExecuteWrapper(parameter, target, trusted, args);
//                }
//                continueRouting = args.ContinueRouting;
//                return args.CanExecute;
//            }
//            continueRouting = false;
//            return false;
//        }

//        internal bool CriticalCanExecute(object parameter, IInputElement target, bool trusted, out bool continueRouting)
//        {
//            if ((target != null) && !InputElement.IsValid(target))
//            {
//                throw new InvalidOperationException(TR.Get("Invalid_IInputElement", new object[] { target.GetType() }));
//            }
//            if (target == null)
//            {
//                target = FilterInputElement(Keyboard.FocusedElement);
//            }
//            return CanExecuteImpl(parameter, target, trusted, out continueRouting);
//        }

//        private void CriticalCanExecuteWrapper(object parameter, IInputElement target, bool trusted, CanExecuteRoutedEventArgs args)
//        {
//            DependencyObject o = (DependencyObject)target;
//            if (InputElement.IsUIElement(o))
//            {
//                ((UIElement)o).RaiseEvent(args, trusted);
//            }
//            else if (InputElement.IsContentElement(o))
//            {
//                ((ContentElement)o).RaiseEvent(args, trusted);
//            }
//            else if (InputElement.IsUIElement3D(o))
//            {
//                ((UIElement3D)o).RaiseEvent(args, trusted);
//            }
//        }

//        public void Execute(object parameter, IInputElement target)
//        {
//            if ((target != null) && !InputElement.IsValid(target))
//            {
//                throw new InvalidOperationException(TR.Get("Invalid_IInputElement", new object[] { target.GetType() }));
//            }
//            if (target == null)
//            {
//                target = FilterInputElement(Keyboard.FocusedElement);
//            }
//            this.ExecuteImpl(parameter, target, false);
//        }

//        internal bool ExecuteCore(object parameter, IInputElement target, bool userInitiated)
//        {
//            if (target == null)
//            {
//                target = FilterInputElement(Keyboard.FocusedElement);
//            }
//            return this.ExecuteImpl(parameter, target, userInitiated);
//        }

//        private bool ExecuteImpl(object parameter, IInputElement target, bool userInitiated)
//        {
//            if ((target == null) || this.IsBlockedByRM)
//            {
//                return false;
//            }
//            UIElement element2 = target as UIElement;
//            ContentElement element = null;
//            UIElement3D elementd = null;
//            ExecutedRoutedEventArgs args = new ExecutedRoutedEventArgs(this, parameter);
//            args.RoutedEvent = CommandManager.PreviewExecutedEvent;
//            if (element2 != null)
//            {
//                element2.RaiseEvent(args, userInitiated);
//            }
//            else
//            {
//                element = target as ContentElement;
//                if (element != null)
//                {
//                    element.RaiseEvent(args, userInitiated);
//                }
//                else
//                {
//                    elementd = target as UIElement3D;
//                    if (elementd != null)
//                    {
//                        elementd.RaiseEvent(args, userInitiated);
//                    }
//                }
//            }
//            if (!args.Handled)
//            {
//                args.RoutedEvent = CommandManager.ExecutedEvent;
//                if (element2 != null)
//                {
//                    element2.RaiseEvent(args, userInitiated);
//                }
//                else if (element != null)
//                {
//                    element.RaiseEvent(args, userInitiated);
//                }
//                else if (elementd != null)
//                {
//                    elementd.RaiseEvent(args, userInitiated);
//                }
//            }
//            return args.Handled;
//        }

//        private static IInputElement FilterInputElement(IInputElement elem)
//        {
//            if ((elem != null) && InputElement.IsValid(elem))
//            {
//                return elem;
//            }
//            return null;
//        }


//        bool ICommand.CanExecute(object parameter)
//        {
//            bool flag;
//            return CanExecuteImpl(parameter, FilterInputElement(Keyboard.FocusedElement), false, out flag);
//        }

//        void ICommand.Execute(object parameter)
//        {
//            this.Execute(parameter, FilterInputElement(Keyboard.FocusedElement));
//        }


//        /// <summary>
//        /// Gets the command id.
//        /// </summary>
//        /// <value>The command id.</value>
//        internal byte CommandId
//        {
//            get { return _commandId; }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is blocked by RM.
//        /// </summary>
//        /// <value>
//        /// 	<c>true</c> if this instance is blocked by RM; otherwise, <c>false</c>.
//        /// </value>
//        internal bool IsBlockedByRM
//        {
//            get { return _privateVector[(int)PrivateVectorIndex.IsBlockedByRM]; }
//            set { _privateVector[(int)PrivateVectorIndex.IsBlockedByRM] = value; }
//        }

//        /// <summary>
//        /// Gets the name.
//        /// </summary>
//        /// <value>The name.</value>
//        public string Name
//        {
//            get { return _name; }
//        }

//        /// <summary>
//        /// Gets the type of the owner.
//        /// </summary>
//        /// <value>The type of the owner.</value>
//        public System.Type OwnerType
//        {
//            get { return _ownerType; }
//        }
//    }

//}