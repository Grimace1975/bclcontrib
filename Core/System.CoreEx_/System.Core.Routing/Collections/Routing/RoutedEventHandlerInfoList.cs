namespace System.Collections.Routing
{
    /// <summary>
    /// RoutedEventHandlerInfoList
    /// </summary>
    internal class RoutedEventHandlerInfoList
    {
        internal RoutedEventHandlerInfo[] Handlers;
        internal RoutedEventHandlerInfoList Next;

        /// <summary>
        /// Determines whether [contains] [the specified handlers].
        /// </summary>
        /// <param name="handlers">The handlers.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified handlers]; otherwise, <c>false</c>.
        /// </returns>
        internal bool Contains(RoutedEventHandlerInfoList handlers)
        {
            for (RoutedEventHandlerInfoList list = this; list != null; list = list.Next)
            {
                if (list == handlers)
                {
                    return true;
                }
            }
            return false;
        }
    }
}