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
namespace System.Patterns.Versioning
{
    /// <summary>
    /// VersionManager
    /// </summary>
    public class VersionManager : VersionManagerBase //| VersionContext
    {
        private VersionType _versionType = VersionType.Publish;

        /// <summary>
        /// Gets or sets the version target.
        /// </summary>
        /// <value>The version target.</value>
        public override string VersionTarget { get; set; }

        /// <summary>
        /// Gets or sets the type of the version.
        /// </summary>
        /// <value>The type of the version.</value>
        public override VersionType VersionType
        {
            get { return _versionType; }
            set
            {
                if ((_versionType != value) && (HasAccess(value)))
                {
                    _versionType = value;
                    OnVersionChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the version id.
        /// </summary>
        /// <value>The version id.</value>
        public override decimal VersionId { get; set; }

        /// <summary>
        /// Determines whether [is has access] [the specified application unit].
        /// </summary>
        /// <param name="applicationUnit">The application unit.</param>
        /// <returns>
        /// 	<c>true</c> if [is has access] [the specified application unit]; otherwise, <c>false</c>.
        /// </returns>
        public override bool HasAccess(VersionType versionType)
        {
            //string lastApplicationUnit = Kernel.ApplicationUnit;
            //bool isChangeContext = (lastApplicationUnit != applicationUnit);
            ////- CHANGECONTEXT -//
            //#region CHANGECONTEXT
            //if (isChangeContext) {
            //   HttpEnvironment.Startup(applicationUnit, null);
            //   Kernel.Instance.OnStartup(null);
            //   Kernel.ApplicationType = ApplicationType.WebApplication;
            //}
            //SecurityState securityState = SecurityState.Instance;
            //bool isHasAccess = securityState.IsSecurityAccountLoggedIn;
            //if (isChangeContext) {
            //   HttpEnvironment.Shutdown();
            //   Kernel.ContextSwitch(lastApplicationUnit);
            //}
            //#endregion CHANGECONTEXT
            //return isHasAccess;
            return true;
        }

        public override event EventHandler VersionChanged;

        /// <summary>
        /// Called when [version changed].
        /// </summary>
        /// <param name="versionType">Type of the version.</param>
        protected virtual void OnVersionChanged()
        {
            EventHandler versionChanged = VersionChanged;
            if (versionChanged != null)
                versionChanged(this, EventArgs.Empty);
        }
    }
}
