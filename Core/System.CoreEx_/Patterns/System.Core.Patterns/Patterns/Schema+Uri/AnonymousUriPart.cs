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
namespace System.Patterns.Schema
{
	/// <summary>
	/// AnonymousUriPart
	/// </summary>
	public class AnonymousUriPart : UriPartBase
	{
        public delegate bool UriPartPredicate(string id, ref UriPart part);

        private UriPartPredicate _predicate;
        //private TryFunc<string, string> _predicate;

        public AnonymousUriPart(UriPartPredicate predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("predicate");
			_predicate = predicate;
		}

		public override string CreateUriPart(ref UriPart part, string newValue)
		{
			string value = (newValue == null ? part.Value : (newValue != "\xff" ? newValue : part.Value));
            return (string.IsNullOrEmpty(value) ? string.Empty : value + "/");
		}

		public override void ParseUriPart(ref UriPart part, IUriPartScanner scanner)
		{
			if (scanner == null)
				throw new ArgumentNullException("scanner");
			string normalizedPath = scanner.NormalizedPath;
			int normalizedPathIndex;
            var newPart = new UriPart();
			if ((normalizedPath.Length > 1) && ((normalizedPathIndex = normalizedPath.IndexOf("/", 1)) > -1))
			{
				string id = normalizedPath.Substring(1, normalizedPathIndex - 1);
                if (_predicate(id, ref newPart))
				{
                    part = newPart;
					scanner.IncreasePath(normalizedPathIndex);
				}
			}
		}
	}
}