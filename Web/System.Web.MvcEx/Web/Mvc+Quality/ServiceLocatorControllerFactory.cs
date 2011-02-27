#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Quality;
using System.Web.Routing;
namespace System.Web.Mvc
{
    /// <summary>
    /// ServiceLocatorControllerFactory
    /// </summary>
    public class ServiceLocatorControllerFactory : DefaultControllerFactory
    {
        private static readonly object s_lock = new object();
        private static IActionInvoker s_actionInvoker;

        public ServiceLocatorControllerFactory()
            : this(ServiceLocatorManager.Current) { }
        public ServiceLocatorControllerFactory(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
                throw new ArgumentNullException("serviceLocator");
            ServiceLocator = serviceLocator;
        }

        protected virtual IActionInvoker GetActionInvoker()
        {
            if (s_actionInvoker == null)
                lock (s_lock)
                    if (s_actionInvoker == null)
                        try
                        {
                            s_actionInvoker = ServiceLocator.Resolve<IActionInvoker>();
                        }
                        catch (ServiceResolutionException) { s_actionInvoker = new ServiceLocatorActionInvoker(ServiceLocator); }
            return s_actionInvoker;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (!ServiceLocatorManager.GetWantsToSkipLocator(controllerType))
            {
                var controller = ServiceLocator.Resolve<IController>(controllerType);
                var controllerAsController = (controller as Controller);
                if (controllerAsController != null)
                    controllerAsController.ActionInvoker = GetActionInvoker();
                return controller;
            }
            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            if (!ServiceLocatorManager.GetWantsToSkipLocator<IController>(controller))
            {
                var disposable = (controller as IDisposable);
                if (disposable != null)
                    disposable.Dispose();
                ServiceLocator.Release(controller);
            }
            base.ReleaseController(controller);
        }

        public IServiceLocator ServiceLocator { get; private set; }
    }
}