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
    /// Enumeration value used in conjunction with the CacheCommand type for identifying a relative priority for an item being cached.
    /// </summary>
    public enum CacheItemPriority
    {
        /// <summary>
        /// Cached item has an above normal relative priority value.
        /// </summary>
        AboveNormal = 4,
        /// <summary>
        /// Cached item has an below normal relative priority value.
        /// </summary>
        BelowNormal = 2,
        /// <summary>
        /// Cached item has a default priority value.
        /// </summary>
        Default = 3,
        /// <summary>
        /// Cached item has a high priority value.
        /// </summary>
        High = 5,
        /// <summary>
        /// Cached item has a low priority value.
        /// </summary>
        Low = 1,
        /// <summary>
        /// Cached item has a normal priority value.
        /// </summary>
        Normal = 3,
        /// <summary>
        /// Cached item has a priority value indicating it can not be removed from cache.
        /// </summary>
        NotRemovable = 6
    }
}
