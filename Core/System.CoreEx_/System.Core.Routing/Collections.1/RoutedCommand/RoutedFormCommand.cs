//namespace Instinct.Collections.RoutedCommand
//{
//    /// <summary>
//    /// RoutedFormCommand
//    /// </summary>
//    public class RoutedFormCommand : RoutedCommand
//    {
//        private string _text;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedFormCommand"/> class.
//        /// </summary>
//        public RoutedFormCommand()
//        {
//            _text = string.Empty;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedFormCommand"/> class.
//        /// </summary>
//        /// <param name="text">The text.</param>
//        /// <param name="name">The name.</param>
//        /// <param name="ownerType">Type of the owner.</param>
//        public RoutedFormCommand(string text, string name, System.Type ownerType)
//            : base(text, name, ownerType)
//        {
//            if (text == null)
//            {
//                throw new ArgumentNullException("text");
//            }
//            _text = text;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="RoutedFormCommand"/> class.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="ownerType">Type of the owner.</param>
//        /// <param name="commandId">The command id.</param>
//        internal RoutedFormCommand(string name, System.Type ownerType, byte commandId)
//            : base(name, ownerType, commandId)
//        {
//        }

//        /// <summary>
//        /// Gets the text.
//        /// </summary>
//        /// <returns></returns>
//        private string GetText()
//        {
//            //if (base.OwnerType == typeof(ApplicationCommands))
//            //{
//            //    return ApplicationCommands.GetUIText(base.CommandId);
//            //}
//            //if (base.OwnerType == typeof(NavigationCommands))
//            //{
//            //    return NavigationCommands.GetUIText(base.CommandId);
//            //}
//            //if (base.OwnerType == typeof(MediaCommands))
//            //{
//            //    return MediaCommands.GetUIText(base.CommandId);
//            //}
//            //if (base.OwnerType == typeof(ComponentCommands))
//            //{
//            //    return ComponentCommands.GetUIText(base.CommandId);
//            //}
//            return null;
//        }

//        /// <summary>
//        /// Gets or sets the text.
//        /// </summary>
//        /// <value>The text.</value>
//        public string Text
//        {
//            get
//            {
//                if (_text == null)
//                {
//                    _text = GetText();
//                }
//                return _text;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                _text = value;
//            }
//        }
//    }
//}