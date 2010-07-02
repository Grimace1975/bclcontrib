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
using System.Interop.MediaInfo.Native_;
using System.IO;
namespace System.Interop.MediaInfo
{
    /// <summary>
    /// MediaVisitor
    /// </summary>
    public class MediaVisitor : IDisposable
    {
        private string _path;
        internal IntPtr _handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaVisitor"/> class.
        /// </summary>
        public MediaVisitor()
        {
        }

        /// <summary>
        /// Opens the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Open(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found :" + path, path);
            _path = path;
            if (_handle != null)
                MediaInfo.Close(_handle);
            _handle = MediaInfo.New();
            MediaInfo.Open(_handle, path);
            AudioTracks = MediaInfo.GetCount(_handle, StreamKind.Audio, -1);
            VideoTracks = MediaInfo.GetCount(_handle, StreamKind.Video, -1);
            GeneralTracks = MediaInfo.GetCount(_handle, StreamKind.General, -1);
            TextTracks = MediaInfo.GetCount(_handle, StreamKind.Text, -1);
            ChaptersTracks = MediaInfo.GetCount(_handle, StreamKind.Chapters, -1);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MediaVisitor"/> is reclaimed by garbage collection.
        /// </summary>
        ~MediaVisitor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //}
            MediaInfo.Close(_handle);
            MediaInfo.Delete(_handle);
        }

        /// <summary>
        /// Gets or sets the audio count.
        /// </summary>
        /// <value>The audio count.</value>
        public int AudioTracks { get; private set; }
        /// <summary>
        /// Gets or sets the video count.
        /// </summary>
        /// <value>The video count.</value>
        public int VideoTracks { get; private set; }
        /// <summary>
        /// Gets or sets the general count.
        /// </summary>
        /// <value>The general count.</value>
        public int GeneralTracks { get; private set; }
        /// <summary>
        /// Gets or sets the text count.
        /// </summary>
        /// <value>The text count.</value>
        public int TextTracks { get; private set; }
        /// <summary>
        /// Gets or sets the chapters count.
        /// </summary>
        /// <value>The chapters count.</value>
        public int ChaptersTracks { get; private set; }
    }
}
