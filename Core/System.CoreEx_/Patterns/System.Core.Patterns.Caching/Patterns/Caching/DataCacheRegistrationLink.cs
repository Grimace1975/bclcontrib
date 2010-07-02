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
    /// DataCacheRegistrationLink
    /// </summary>
    public class DataCacheRegistrationLink : DataCacheRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCacheRegistrationLink"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="foreignType">Type of the foreign.</param>
        /// <param name="foreignKey">The foreign key.</param>
        public DataCacheRegistrationLink(string id, Type foreignType, string foreignKey)
            : base(id)
        {
            ForeignType = foreignType;
            ForeignId = foreignKey;
        }

        /// <summary>
        /// Gets or sets the type of the foreign.
        /// </summary>
        /// <value>The type of the foreign.</value>
        public Type ForeignType { get; set; }

        /// <summary>
        /// Gets or sets the foreign key.
        /// </summary>
        /// <value>The foreign key.</value>
        public string ForeignId { get; set; }
    }
}
