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
	/// CultureUriPart
    /// </summary>
    //+ [culture]ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.VisualStudio.v80.en/dv_aspnetcon/html/8ef3838e-9d05-4236-9dd0-ceecff9df80d.htm
    //+ [culture]ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.VisualStudio.v80.en/dv_aspnetcon/html/76091f86-f967-4687-a40f-de87bd8cc9a0.htm
	public class CultureUriPart : UriPartBase
    {
		public override string CreateUriPart(ref UriPart part, string newValue)
        {
			string value = (newValue ?? part.Value);
            return (string.IsNullOrEmpty(value) ? string.Empty : value + "/");
        }

		public override void ParseUriPart(ref UriPart part, IUriPartScanner scanner)
        {
			if (scanner == null)
				throw new ArgumentNullException("scanner");
            var cultureSchema = (CultureSchemaBase)null;
            if (cultureSchema == null)
                throw new NullReferenceException("cultureSchema");
            string normalizedPath = scanner.NormalizedPath;
            int normalizedPathIndex;
            if ((normalizedPath.Length > 1) && ((normalizedPathIndex = normalizedPath.IndexOf("/", 1)) > -1))
            {
                string id = normalizedPath.Substring(1, normalizedPathIndex - 1);
                foreach (var culture in cultureSchema.Cultures)
                    if (string.Compare(culture.Id, id, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        part.RequestValue = culture.Name;
                        part.RequestTag = culture;
                        scanner.IncreasePath(normalizedPathIndex);
                        return;
                    }
            }
        }
    }
}