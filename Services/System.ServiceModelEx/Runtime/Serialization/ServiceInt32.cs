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
namespace System.Runtime.Serialization
{
    /// <summary>
    /// ServiceInt32
    /// </summary>
    [DataContract(Namespace = "http://www.digitalev.com/Service/2008/04/")]
    public class ServiceInt32 : ServiceTypeBase<int?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInt32"/> class.
        /// </summary>
        public ServiceInt32()
            : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInt32"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ServiceInt32(int? value)
            : base(value) { }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static ServiceInt32 operator +(ServiceInt32 a, ServiceInt32 b)
        {
            return new ServiceInt32(a.Value + b.Value);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static ServiceInt32 operator -(ServiceInt32 a, ServiceInt32 b)
        {
            return new ServiceInt32(a.Value - b.Value);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(ServiceInt32 a, ServiceInt32 b)
        {
            return (a.Value < b.Value);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(ServiceInt32 a, ServiceInt32 b)
        {
            return (a.Value > b.Value);
        }
    }
}
