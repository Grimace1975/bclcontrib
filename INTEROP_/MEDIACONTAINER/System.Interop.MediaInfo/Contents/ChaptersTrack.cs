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
namespace System.Interop.MediaInfo.Contents
{
    using _StreamKind = StreamKind;
    /// <summary>
    /// ChaptersTrack
    /// </summary>
    public class ChaptersTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChaptersTrack"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="trackIndex">Index of the track.</param>
        public ChaptersTrack(MediaVisitor visitor, uint trackIndex)
        {
            if (trackIndex >= visitor.ChaptersTracks)
            {
                throw new ArgumentOutOfRangeException("trackIndex");
            }
            var handle = visitor._handle;
            Count = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Count");
            StreamCount = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "StreamCount");
            StreamKind = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "StreamKind");
            StreamKindID = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "StreamKindID");
            Inform = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Inform");
            ID = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "ID");
            UniqueID = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "UniqueID");
            Title = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Title");
            Codec = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Codec");
            Codec_String = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Codec/String");
            Codec_Url = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Codec/Url");
            Total = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Total");
            Language = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Language");
            Language_String = MediaInfo.Get(handle, _StreamKind.Chapters, trackIndex, "Language/String");
        }

        public string Codec { get; set; }
        public string Codec_String { get; set; }
        public string Codec_Url { get; set; }
        public string Count { get; set; }
        public string ID { get; set; }
        public string Inform { get; set; }
        public string Language { get; set; }
        public string Language_String { get; set; }
        public string StreamCount { get; set; }
        public string StreamKind { get; set; }
        public string StreamKindID { get; set; }
        public string Title { get; set; }
        public string Total { get; set; }
        public string UniqueID { get; set; }
    }
}
