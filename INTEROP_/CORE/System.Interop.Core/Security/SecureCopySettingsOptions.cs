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
namespace System.Interop.Core.Security
{
    [Flags]
    public enum SecureCopySettingsOptions
    {
        EnableCompression = 0x0001,
        DisableAgent = 0x0002, 
        EnableAgent = 0x0004,
        DisableInteractivePrompts = 0x0008,
        Unsafe = 0x0010,
        ForceSftp = 0x0020, 
        ForceScp = 0x0040,
        Recursively = 0x0080,
        PreserveFileAttributes = 0x0100,
        ForceSshProtocol1 = 0x0200,
        ForceSshProtocol2 = 0x0400,
        ForceIPv4 = 0x0800,
        ForceIPv6 = 0x1000,
    }
}