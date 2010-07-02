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
namespace System.Patterns.Caching
{
    /// <summary>
    /// CacheDependency class
    /// </summary>
    public class CacheDependency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheDependency"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public CacheDependency()
        {
            StartDate = DateTime.MaxValue;
        }

        /// <summary>
        /// Gets or sets the cache key array.
        /// </summary>
        /// <value>The cache key array.</value>
        public string[] CacheKeys { get; set; }

        ///// <summary>
        ///// Gets or sets the dependency.
        ///// </summary>
        ///// <value>The dependency.</value>
        //public CacheDependency Dependency {get;set;}
        //{
        //    get { return m_dependency; }
        //    set { m_dependency = value; }
        //}

        /// <summary>
        /// Gets or sets the file path array.
        /// </summary>
        /// <value>The file path array.</value>
        public string[] FilePaths { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }
    }
}