//namespace Instinct.Collections.RoutedCommand
//{
//    public sealed class CommandManager
//    {
//        // Fields
//        private static HybridDictionary _classCommandBindings = new HybridDictionary();
//        private static HybridDictionary _classInputBindings = new HybridDictionary();
//        [ThreadStatic]
//        private static CommandManager _commandManager;
//        private List<WeakReference> _requerySuggestedHandlers;
//        private DispatcherOperation _requerySuggestedOperation;
//        public static readonly RoutedEvent CanExecuteEvent = EventManager.RegisterRoutedEvent("CanExecute", RoutingStrategy.Bubble, typeof(CanExecuteRoutedEventHandler), typeof(CommandManager));
//        public static readonly RoutedEvent ExecutedEvent = EventManager.RegisterRoutedEvent("Executed", RoutingStrategy.Bubble, typeof(ExecutedRoutedEventHandler), typeof(CommandManager));
//        public static readonly RoutedEvent PreviewCanExecuteEvent = EventManager.RegisterRoutedEvent("PreviewCanExecute", RoutingStrategy.Tunnel, typeof(CanExecuteRoutedEventHandler), typeof(CommandManager));
//        public static readonly RoutedEvent PreviewExecutedEvent = EventManager.RegisterRoutedEvent("PreviewExecuted", RoutingStrategy.Tunnel, typeof(ExecutedRoutedEventHandler), typeof(CommandManager));

//        // Events
//        public static event EventHandler RequerySuggested
//        {
//            add
//            {
//                AddWeakReferenceHandler(ref Current._requerySuggestedHandlers, value);
//            }
//            remove
//            {
//                RemoveWeakReferenceHandler(Current._requerySuggestedHandlers, value);
//            }
//        }

//        // Methods
//        private CommandManager()
//        {
//        }

//        public static void AddCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.AddHandler(CanExecuteEvent, handler);
//        }

//        public static void AddExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.AddHandler(ExecutedEvent, handler);
//        }

//        public static void AddPreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.AddHandler(PreviewCanExecuteEvent, handler);
//        }

//        public static void AddPreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.AddHandler(PreviewExecutedEvent, handler);
//        }

//        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
//        {
//            if (handlers == null)
//            {
//                handlers = new List<WeakReference>();
//            }
//            handlers.Add(new WeakReference(handler));
//        }

//        internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
//        {
//            if (handlers != null)
//            {
//                EventHandler[] handlerArray = new EventHandler[handlers.Count];
//                int index = 0;
//                for (int i = handlers.Count - 1; i >= 0; i--)
//                {
//                    WeakReference reference = handlers[i];
//                    EventHandler target = reference.Target as EventHandler;
//                    if (target == null)
//                    {
//                        handlers.RemoveAt(i);
//                    }
//                    else
//                    {
//                        handlerArray[index] = target;
//                        index++;
//                    }
//                }
//                for (int j = 0; j < index; j++)
//                {
//                    EventHandler handler2 = handlerArray[j];
//                    handler2(null, EventArgs.Empty);
//                }
//            }
//        }

//        private static bool CanExecuteCommandBinding(object sender, CanExecuteRoutedEventArgs e, CommandBinding commandBinding)
//        {
//            commandBinding.OnCanExecute(sender, e);
//            if (!e.CanExecute)
//            {
//                return e.Handled;
//            }
//            return true;
//        }

//        private static bool ContainsElement(DependencyObject scope, DependencyObject child)
//        {
//            if (child != null)
//            {
//                for (DependencyObject obj2 = FocusManager.GetFocusScope(child); obj2 != null; obj2 = GetParentScope(obj2))
//                {
//                    if (obj2 == scope)
//                    {
//                        return true;
//                    }
//                }
//            }
//            return false;
//        }

//        [SecurityTreatAsSafe, SecurityCritical]
//        private static bool ExecuteCommand(RoutedCommand routedCommand, object parameter, IInputElement target, InputEventArgs inputEventArgs)
//        {
//            return routedCommand.ExecuteCore(parameter, target, inputEventArgs.UserInitiated);
//        }

//        [SecurityTreatAsSafe, SecurityCritical]
//        private static bool ExecuteCommandBinding(object sender, ExecutedRoutedEventArgs e, CommandBinding commandBinding)
//        {
//            ISecureCommand command = e.Command as ISecureCommand;
//            bool flag = (e.UserInitiated && (command != null)) && (command.UserInitiatedPermission != null);
//            if (flag)
//            {
//                command.UserInitiatedPermission.Assert();
//            }
//            try
//            {
//                commandBinding.OnExecuted(sender, e);
//            }
//            finally
//            {
//                if (flag)
//                {
//                    CodeAccessPermission.RevertAssert();
//                }
//            }
//            return e.Handled;
//        }

//        private static void FindCommandBinding(object sender, RoutedEventArgs e, ICommand command, bool execute)
//        {
//            CommandBindingCollection commandBindings = null;
//            DependencyObject o = sender as DependencyObject;
//            if (InputElement.IsUIElement(o))
//            {
//                commandBindings = ((UIElement)o).CommandBindingsInternal;
//            }
//            else if (InputElement.IsContentElement(o))
//            {
//                commandBindings = ((ContentElement)o).CommandBindingsInternal;
//            }
//            else if (InputElement.IsUIElement3D(o))
//            {
//                commandBindings = ((UIElement3D)o).CommandBindingsInternal;
//            }
//            if (commandBindings != null)
//            {
//                FindCommandBinding(commandBindings, sender, e, command, execute);
//            }
//            lock (_classCommandBindings.SyncRoot)
//            {
//                for (Type type = sender.GetType(); type != null; type = type.BaseType)
//                {
//                    CommandBindingCollection bindings2 = _classCommandBindings[type] as CommandBindingCollection;
//                    if (bindings2 != null)
//                    {
//                        FindCommandBinding(bindings2, sender, e, command, execute);
//                    }
//                }
//            }
//        }

//        private static void FindCommandBinding(CommandBindingCollection commandBindings, object sender, RoutedEventArgs e, ICommand command, bool execute)
//        {
//            CommandBinding binding;
//            int index = 0;
//            do
//            {
//                binding = commandBindings.FindMatch(command, ref index);
//            }
//            while (((binding != null) && (!execute || !ExecuteCommandBinding(sender, (ExecutedRoutedEventArgs)e, binding))) && (execute || !CanExecuteCommandBinding(sender, (CanExecuteRoutedEventArgs)e, binding)));
//        }

//        private static DependencyObject GetParentScope(DependencyObject childScope)
//        {
//            DependencyObject uIParent = null;
//            UIElement element = childScope as UIElement;
//            ContentElement element2 = (element == null) ? (childScope as ContentElement) : null;
//            UIElement3D elementd = ((element == null) && (element2 == null)) ? (childScope as UIElement3D) : null;
//            if (element != null)
//            {
//                uIParent = element.GetUIParent(true);
//            }
//            else if (element2 != null)
//            {
//                uIParent = element2.GetUIParent(true);
//            }
//            else if (elementd != null)
//            {
//                uIParent = elementd.GetUIParent(true);
//            }
//            if (uIParent != null)
//            {
//                return FocusManager.GetFocusScope(uIParent);
//            }
//            return null;
//        }

//        private static IInputElement GetParentScopeFocusedElement(DependencyObject childScope)
//        {
//            DependencyObject parentScope = GetParentScope(childScope);
//            if (parentScope != null)
//            {
//                IInputElement focusedElement = FocusManager.GetFocusedElement(parentScope);
//                if ((focusedElement != null) && !ContainsElement(childScope, focusedElement as DependencyObject))
//                {
//                    return focusedElement;
//                }
//            }
//            return null;
//        }

//        public static void InvalidateRequerySuggested()
//        {
//            Current.RaiseRequerySuggested();
//        }

//        internal static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
//        {
//            if (((sender != null) && (e != null)) && (e.Command != null))
//            {
//                FindCommandBinding(sender, e, e.Command, false);
//                if (!e.Handled && (e.RoutedEvent == CanExecuteEvent))
//                {
//                    DependencyObject obj2 = sender as DependencyObject;
//                    if ((obj2 != null) && FocusManager.GetIsFocusScope(obj2))
//                    {
//                        IInputElement parentScopeFocusedElement = GetParentScopeFocusedElement(obj2);
//                        if (parentScopeFocusedElement != null)
//                        {
//                            TransferEvent(parentScopeFocusedElement, e);
//                        }
//                    }
//                }
//            }
//        }

//        internal static void OnCommandDevice(object sender, CommandDeviceEventArgs e)
//        {
//            if (((sender != null) && (e != null)) && (e.Command != null))
//            {
//                CanExecuteRoutedEventArgs args2 = new CanExecuteRoutedEventArgs(e.Command, null);
//                args2.RoutedEvent = CanExecuteEvent;
//                args2.Source = sender;
//                OnCanExecute(sender, args2);
//                if (args2.CanExecute)
//                {
//                    ExecutedRoutedEventArgs args = new ExecutedRoutedEventArgs(e.Command, null);
//                    args.RoutedEvent = ExecutedEvent;
//                    args.Source = sender;
//                    OnExecuted(sender, args);
//                    if (args.Handled)
//                    {
//                        e.Handled = true;
//                    }
//                }
//            }
//        }

//        internal static void OnExecuted(object sender, ExecutedRoutedEventArgs e)
//        {
//            if (((sender != null) && (e != null)) && (e.Command != null))
//            {
//                FindCommandBinding(sender, e, e.Command, true);
//                if (!e.Handled && (e.RoutedEvent == ExecutedEvent))
//                {
//                    DependencyObject obj2 = sender as DependencyObject;
//                    if ((obj2 != null) && FocusManager.GetIsFocusScope(obj2))
//                    {
//                        IInputElement parentScopeFocusedElement = GetParentScopeFocusedElement(obj2);
//                        if (parentScopeFocusedElement != null)
//                        {
//                            TransferEvent(parentScopeFocusedElement, e);
//                        }
//                    }
//                }
//            }
//        }

//        private void RaiseRequerySuggested()
//        {
//            if (this._requerySuggestedOperation == null)
//            {
//                Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
//                if (((currentDispatcher != null) && !currentDispatcher.HasShutdownStarted) && !currentDispatcher.HasShutdownFinished)
//                {
//                    this._requerySuggestedOperation = currentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(this.RaiseRequerySuggested), null);
//                }
//            }
//        }

//        private object RaiseRequerySuggested(object obj)
//        {
//            this._requerySuggestedOperation = null;
//            CallWeakReferenceHandlers(this._requerySuggestedHandlers);
//            return null;
//        }

//        public static void RegisterClassCommandBinding(Type type, CommandBinding commandBinding)
//        {
//            if (type == null)
//            {
//                throw new ArgumentNullException("type");
//            }
//            if (commandBinding == null)
//            {
//                throw new ArgumentNullException("commandBinding");
//            }
//            lock (_classCommandBindings.SyncRoot)
//            {
//                CommandBindingCollection bindings = _classCommandBindings[type] as CommandBindingCollection;
//                if (bindings == null)
//                {
//                    bindings = new CommandBindingCollection();
//                    _classCommandBindings[type] = bindings;
//                }
//                bindings.Add(commandBinding);
//            }
//        }

//        public static void RegisterClassInputBinding(Type type, InputBinding inputBinding)
//        {
//            if (type == null)
//            {
//                throw new ArgumentNullException("type");
//            }
//            if (inputBinding == null)
//            {
//                throw new ArgumentNullException("inputBinding");
//            }
//            lock (_classInputBindings.SyncRoot)
//            {
//                InputBindingCollection bindings = _classInputBindings[type] as InputBindingCollection;
//                if (bindings == null)
//                {
//                    bindings = new InputBindingCollection();
//                    _classInputBindings[type] = bindings;
//                }
//                bindings.Add(inputBinding);
//            }
//        }

//        public static void RemoveCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.RemoveHandler(CanExecuteEvent, handler);
//        }

//        public static void RemoveExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.RemoveHandler(ExecutedEvent, handler);
//        }

//        public static void RemovePreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.RemoveHandler(PreviewCanExecuteEvent, handler);
//        }

//        public static void RemovePreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
//        {
//            if (element == null)
//            {
//                throw new ArgumentNullException("element");
//            }
//            if (handler == null)
//            {
//                throw new ArgumentNullException("handler");
//            }
//            element.RemoveHandler(PreviewExecutedEvent, handler);
//        }

//        internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
//        {
//            if (handlers != null)
//            {
//                for (int i = handlers.Count - 1; i >= 0; i--)
//                {
//                    WeakReference reference = handlers[i];
//                    EventHandler target = reference.Target as EventHandler;
//                    if ((target == null) || (target == handler))
//                    {
//                        handlers.RemoveAt(i);
//                    }
//                }
//            }
//        }

//        private static void TransferEvent(IInputElement newSource, CanExecuteRoutedEventArgs e)
//        {
//            RoutedCommand command = e.Command as RoutedCommand;
//            if (command != null)
//            {
//                try
//                {
//                    e.CanExecute = command.CanExecute(e.Parameter, newSource);
//                }
//                finally
//                {
//                    e.Handled = true;
//                }
//            }
//        }

//        [SecurityTreatAsSafe, SecurityCritical]
//        private static void TransferEvent(IInputElement newSource, ExecutedRoutedEventArgs e)
//        {
//            RoutedCommand command = e.Command as RoutedCommand;
//            if (command != null)
//            {
//                try
//                {
//                    command.ExecuteCore(e.Parameter, newSource, e.UserInitiated);
//                }
//                finally
//                {
//                    e.Handled = true;
//                }
//            }
//        }

//        [SecurityCritical]
//        internal static void TranslateInput(IInputElement targetElement, InputEventArgs inputEventArgs)
//        {
//            if ((targetElement == null) || (inputEventArgs == null))
//            {
//                return;
//            }
//            ICommand command = null;
//            IInputElement target = null;
//            object parameter = null;
//            DependencyObject o = targetElement as DependencyObject;
//            bool flag = InputElement.IsUIElement(o);
//            bool flag2 = !flag && InputElement.IsContentElement(o);
//            bool flag3 = (!flag && !flag2) && InputElement.IsUIElement3D(o);
//            InputBindingCollection inputBindingsInternal = null;
//            if (flag)
//            {
//                inputBindingsInternal = ((UIElement)targetElement).InputBindingsInternal;
//            }
//            else if (flag2)
//            {
//                inputBindingsInternal = ((ContentElement)targetElement).InputBindingsInternal;
//            }
//            else if (flag3)
//            {
//                inputBindingsInternal = ((UIElement3D)targetElement).InputBindingsInternal;
//            }
//            if (inputBindingsInternal != null)
//            {
//                InputBinding binding2 = inputBindingsInternal.FindMatch(targetElement, inputEventArgs);
//                if (binding2 != null)
//                {
//                    command = binding2.Command;
//                    target = binding2.CommandTarget;
//                    parameter = binding2.CommandParameter;
//                }
//            }
//            if (command == null)
//            {
//                lock (_classInputBindings.SyncRoot)
//                {
//                    for (Type type2 = targetElement.GetType(); type2 != null; type2 = type2.BaseType)
//                    {
//                        InputBindingCollection bindings4 = _classInputBindings[type2] as InputBindingCollection;
//                        if (bindings4 != null)
//                        {
//                            InputBinding binding = bindings4.FindMatch(targetElement, inputEventArgs);
//                            if (binding != null)
//                            {
//                                command = binding.Command;
//                                target = binding.CommandTarget;
//                                parameter = binding.CommandParameter;
//                                goto Label_011D;
//                            }
//                        }
//                    }
//                }
//            }
//        Label_011D:
//            if (command == null)
//            {
//                CommandBindingCollection commandBindingsInternal = null;
//                if (flag)
//                {
//                    commandBindingsInternal = ((UIElement)targetElement).CommandBindingsInternal;
//                }
//                else if (flag2)
//                {
//                    commandBindingsInternal = ((ContentElement)targetElement).CommandBindingsInternal;
//                }
//                else if (flag3)
//                {
//                    commandBindingsInternal = ((UIElement3D)targetElement).CommandBindingsInternal;
//                }
//                if (commandBindingsInternal != null)
//                {
//                    command = commandBindingsInternal.FindMatch(targetElement, inputEventArgs);
//                }
//            }
//            if (command == null)
//            {
//                lock (_classCommandBindings.SyncRoot)
//                {
//                    for (Type type = targetElement.GetType(); type != null; type = type.BaseType)
//                    {
//                        CommandBindingCollection bindings3 = _classCommandBindings[type] as CommandBindingCollection;
//                        if (bindings3 != null)
//                        {
//                            command = bindings3.FindMatch(targetElement, inputEventArgs);
//                            if (command != null)
//                            {
//                                goto Label_01BC;
//                            }
//                        }
//                    }
//                }
//            }
//        Label_01BC:
//            if ((command != null) && (command != ApplicationCommands.NotACommand))
//            {
//                if (target == null)
//                {
//                    target = targetElement;
//                }
//                bool continueRouting = false;
//                RoutedCommand routedCommand = command as RoutedCommand;
//                if (routedCommand != null)
//                {
//                    if (routedCommand.CriticalCanExecute(parameter, target, inputEventArgs.UserInitiated, out continueRouting))
//                    {
//                        ExecuteCommand(routedCommand, parameter, target, inputEventArgs);
//                    }
//                }
//                else if (command.CanExecute(parameter))
//                {
//                    command.Execute(parameter);
//                }
//                inputEventArgs.Handled = !continueRouting;
//            }
//        }

//        // Properties
//        private static CommandManager Current
//        {
//            get
//            {
//                if (_commandManager == null)
//                {
//                    _commandManager = new CommandManager();
//                }
//                return _commandManager;
//            }
//        }
//    }

//}