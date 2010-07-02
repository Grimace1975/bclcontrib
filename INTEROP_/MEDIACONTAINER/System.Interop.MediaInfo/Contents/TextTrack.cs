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
    /// TextTrack
    /// </summary>
    public class TextTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextTrack"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="trackIndex">Index of the track.</param>
        public TextTrack(MediaVisitor visitor, uint trackIndex)
        {
            if (trackIndex >= visitor.TextTracks)
            {
                throw new ArgumentOutOfRangeException("trackIndex");
            }
            var handle = visitor._handle;
            Count = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Count");
            StreamCount = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "StreamCount");
            StreamKind = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "StreamKind");
            StreamKindID = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "StreamKindID");
            Inform = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Inform");
            ID = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "ID");
            UniqueID = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "UniqueID");
            Title = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Title");
            Codec = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Codec");
            Codec_String = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Codec/String");
            Codec_Url = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Codec/Url");
            Delay = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Delay");
            Video0Delay = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Video0_Delay");
            PlayTime = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "PlayTime");
            PlayTime_String = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "PlayTime/String");
            PlayTime_String1 = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "PlayTime/String1");
            PlayTime_String2 = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "PlayTime/String2");
            PlayTime_String3 = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "PlayTime/String3");
            Language = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Language");
            Language_String = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Language/String");
            LanguageMore = MediaInfo.Get(handle, _StreamKind.Text, trackIndex, "Language_More");
        }

        public string Codec { get; set; }
        public string Codec_String { get; set; }
        public string Codec_Url { get; set; }
        public string Count { get; set; }
        public string Delay { get; set; }
        public string ID { get; set; }
        public string Inform { get; set; }
        public string Language { get; set; }
        public string LanguageMore { get; set; }
        public string Language_String { get; set; }
        public string PlayTime { get; set; }
        public string PlayTime_String { get; set; }
        public string PlayTime_String1 { get; set; }
        public string PlayTime_String2 { get; set; }
        public string PlayTime_String3 { get; set; }
        public string StreamCount { get; set; }
        public string StreamKind { get; set; }
        public string StreamKindID { get; set; }
        public string Title { get; set; }
        public string UniqueID { get; set; }
        public string Video0Delay { get; set; }
    }
}
