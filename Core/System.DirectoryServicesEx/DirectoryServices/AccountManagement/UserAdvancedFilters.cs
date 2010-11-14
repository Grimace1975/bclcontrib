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
namespace System.DirectoryServices.AccountManagement
{
    public class UserAdvancedFilters : AdvancedFilters
    {
        public UserAdvancedFilters(Principal p)
            : base(p) { }

        public void LogonCount(int value, MatchType mt)
        {
            AdvancedFilterSet("LogonCount", value, typeof(int), mt);
        }

        public void Created(DateTime? created, MatchType matchType)
        {
            const string WhenCreatedDateFormat = "yyyyMMddHHmmss.0Z";
            if (!created.HasValue)
                return;
            AdvancedFilterSet("whenCreated", created.Value.ToUniversalTime().ToString(WhenCreatedDateFormat), typeof(string), matchType);
        }

        public void MemberOf(string distinguishedName, MatchType matchType)
        {
            if (distinguishedName == null)
                throw new ArgumentNullException("distinguishedName");
            AdvancedFilterSet("memberOf", distinguishedName, typeof(string), matchType);
        }
    }
}
