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
using System.Patterns.ReleaseManagement;
namespace System
{
    /// <summary>
    /// EnvironmentEx
    /// </summary>
    public static partial class EnvironmentEx
    {
        private static DeploymentEnvironment s_deploymentEnvironment = DeploymentEnvironment.Production;
        private static DevelopmentStage s_developmentStage = DevelopmentStage.Release;

        static EnvironmentEx()
        {
            ApplicationId = "NONE";
        }

        [ThreadStatic]
        private static MockBase s_mock;

        public static MockBase Mock
        {
            get { return s_mock; }
            set { s_mock = value; }
        }

        public static string ApplicationId { get; private set; }

        public static DeploymentEnvironment DeploymentEnvironment
        {
            get { return (s_mock == null ? s_deploymentEnvironment : s_mock.DeploymentEnvironment); }
            set
            {
                if (s_mock == null)
                    s_deploymentEnvironment = value;
                else
                    throw new InvalidOperationException("Mocked");
            }
        }

        public static DevelopmentStage DevelopmentStage
        {
            get { return (s_mock == null ? s_developmentStage : s_mock.DevelopmentStage); }
            set
            {
                if (s_mock == null)
                    s_developmentStage = value;
                else
                    throw new InvalidOperationException("Mocked");
            }
        }
    }
}
