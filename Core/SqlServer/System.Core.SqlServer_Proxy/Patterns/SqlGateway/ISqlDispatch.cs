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
using System.Data.SqlTypes;
namespace System.Patterns.SqlGateway
{
    /// <summary>
    /// ISqlDispatch
    /// </summary>
    public interface ISqlDispatch
    {
        /// <summary>
        /// Gets a value indicating whether this instance has pipe.
        /// </summary>
        /// <value><c>true</c> if this instance has pipe; otherwise, <c>false</c>.</value>
        bool HasPipe { get; }
        /// <summary>
        /// Adds the instruction.
        /// </summary>
        /// <param name="name">The name.</param>
        void AddInstruction(string name);
        /// <summary>
        /// Adds the instruction2.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataXml">The data XML.</param>
        void AddInstruction2(string name, SqlXml tag);
        /// <summary>
        /// Adds the instruction3.
        /// </summary>
        /// <param name="xml">The XML.</param>
        void AddInstruction3(SqlXml xml);
        /// <summary>
        /// Adds the instruction if.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="condition">The condition.</param>
        void AddInstructionIf(string name, string condition);
        /// <summary>
        /// Adds the instruction if.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataXml">The data XML.</param>
        /// <param name="condition">The condition.</param>
        void AddInstructionIf2(string name, SqlXml tag, string condition);
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
        /// <summary>
        /// Gets the shard.
        /// </summary>
        /// <value>The shard.</value>
        int Shard { get; }
        /// <summary>
        /// Gets or sets the state key.
        /// </summary>
        /// <value>The state key.</value>
        SqlInt32 StateKey { get; set; }
        /// <summary>
        /// Gets or sets the state last modify by.
        /// </summary>
        /// <value>The state last modify by.</value>
        SqlString StateLastModifyBy { get; set; }
        /// <summary>
        /// Gets or sets the state XML.
        /// </summary>
        /// <value>The state XML.</value>
        SqlXml StateXml { get; set; }
        //+ Internal
        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        bool IsNull { get; }
        /// <summary>
        /// Internals the run.
        /// </summary>
        int InternalRun();
    }
}