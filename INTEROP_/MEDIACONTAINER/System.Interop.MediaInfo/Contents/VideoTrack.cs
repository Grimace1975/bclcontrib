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
    /// VideoTrack
    /// </summary>
    public class VideoTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoTrack"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="trackIndex">Index of the track.</param>
        public VideoTrack(MediaVisitor visitor, uint trackIndex)
        {
            if (trackIndex >= visitor.VideoTracks)
                throw new ArgumentOutOfRangeException("trackIndex");
            var handle = visitor._handle;
            Count = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Count");
            StreamCount = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "StreamCount");
            StreamKind = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "StreamKind");
            StreamKindID = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "StreamKindID");
            Inform = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Inform");
            ID = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "ID");
            UniqueID = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "UniqueID");
            Title = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Title");
            Codec = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Codec");
            Codec_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Codec/String");
            Codec_Info = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Codec/Info");
            Codec_Url = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Codec/Url");
            CodecID = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "CodecID");
            CodecID_Info = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "CodecID/Info");
            BitRate = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "BitRate");
            BitRate_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "BitRate/String");
            BitRateMode = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "BitRate_Mode");
            EncodedLibrary = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Encoded_Library");
            EncodedLibrarySettings = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Encoded_Library_Settings");
            Width = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Width");
            Height = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Height");
            AspectRatio = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "AspectRatio");
            AspectRatio_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "AspectRatio/String");
            FrameRate = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "FrameRate");
            FrameRate_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "FrameRate/String");
            FrameCount = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "FrameCount");
            Resolution = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Resolution");
            Bits_PixelFrame = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Bits/(Pixel*Frame)");
            Delay = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Delay");
            Duration = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Duration");
            Duration_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Duration/String");
            Duration_String1 = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Duration/String1");
            Duration_String2 = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Duration/String2");
            Duration_String3 = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Duration/String3");
            Language = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Language");
            Language_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Language/String");
            LanguageMore = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Language_More");
            Format = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format");
            Format_Info = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format/Info");
            FormatProfile = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Profile");
            FormatSettings = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings");
            FormatSettingsBVOP = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_BVOP");
            FormatSettingsBVOP_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_BVOP/String");
            FormatSettingsCABAC = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_CABAC");
            FormatSettingsCABAC_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_CABAC/String");
            FormatSettingsGMC = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_GMC");
            FormatSettingsGMC_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_GMAC/String");
            FormatSettingsMatrix = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_Matrix");
            FormatSettingsMatrixData = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_Matrix_Data");
            FormatSettingsMatrix_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_Matrix/String");
            FormatSettingsPulldown = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_Pulldown");
            FormatSettingsQPel = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_QPel");
            FormatSettingsQPel_String = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_QPel/String");
            FormatSettingsRefFrames = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_RefFrames");
            FormatSettingsRefFramesString = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Settings_RefFrames/String");
            Format_Url = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format/Url");
            FormatVersion = MediaInfo.Get(handle, _StreamKind.Video, trackIndex, "Format_Version");
        }

        public string AspectRatio { get; set; }
        public string AspectRatio_String { get; set; }
        public string BitRate { get; set; }
        public string BitRateMode { get; set; }
        public string BitRate_String { get; set; }
        public string Bits_PixelFrame { get; set; }
        public string Codec { get; set; }
        public string CodecID { get; set; }
        public string CodecID_Info { get; set; }
        public string Codec_Info { get; set; }
        public string Codec_String { get; set; }
        public string Codec_Url { get; set; }
        public string Count { get; set; }
        public string Delay { get; set; }
        public string Duration { get; set; }
        public string Duration_String { get; set; }
        public string Duration_String1 { get; set; }
        public string Duration_String2 { get; set; }
        public string Duration_String3 { get; set; }
        public string EncodedLibrary { get; set; }
        public string EncodedLibrarySettings { get; set; }
        public string Format { get; set; }
        public string Format_Info { get; set; }
        public string FormatProfile { get; set; }
        public string FormatSettings { get; set; }
        public string FormatSettingsBVOP { get; set; }
        public string FormatSettingsBVOP_String { get; set; }
        public string FormatSettingsCABAC { get; set; }
        public string FormatSettingsCABAC_String { get; set; }
        public string FormatSettingsGMC { get; set; }
        public string FormatSettingsGMC_String { get; set; }
        public string FormatSettingsMatrix { get; set; }
        public string FormatSettingsMatrixData { get; set; }
        public string FormatSettingsMatrix_String { get; set; }
        public string FormatSettingsPulldown { get; set; }
        public string FormatSettingsQPel { get; set; }
        public string FormatSettingsQPel_String { get; set; }
        public string FormatSettingsRefFrames { get; set; }
        public string FormatSettingsRefFramesString { get; set; }
        public string Format_Url { get; set; }
        public string FormatVersion { get; set; }
        public string FrameCount { get; set; }
        public string FrameRate { get; set; }
        public string FrameRate_String { get; set; }
        public string Height { get; set; }
        public string ID { get; set; }
        public string Inform { get; set; }
        public string Language { get; set; }
        public string LanguageMore { get; set; }
        public string Language_String { get; set; }
        public string Resolution { get; set; }
        public string StreamCount { get; set; }
        public string StreamKind { get; set; }
        public string StreamKindID { get; set; }
        public string Title { get; set; }
        public string UniqueID { get; set; }
        public string Width { get; set; }
    }
}
