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
    /// AudioTrack
    /// </summary>
    public class AudioTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioTrack"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="trackIndex">Index of the track.</param>
        public AudioTrack(MediaVisitor visitor, uint trackIndex)
        {
            if (trackIndex >= visitor.AudioTracks)
            {
                throw new ArgumentOutOfRangeException("trackIndex");
            }
            var handle = visitor._handle;
            Count = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Count");
            StreamCount = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamCount");
            StreamKind = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamKind");
            StreamKind_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamKind/String");
            StreamKindID = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamKindID");
            StreamKindPos = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamKindPos");
            Inform = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Inform");
            ID = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ID");
            ID_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ID/String");
            UniqueID = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "UniqueID");
            MenuID = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "MenuID");
            MenuID_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "MenuID/String");
            Format = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format");
            Format_Info = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format/Info");
            Format_Url = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format/Url");
            FormatVersion = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Version");
            FormatProfile = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Profile");
            FormatSettings = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings");
            FormatSettingsSBR = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_SBR");
            FormatSettingsSBR_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_SBR/String");
            FormatSettingsPS = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_PS");
            FormatSettingsPS_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_PS/String");
            FormatSettingsFloor = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_Floor");
            FormatSettingsFirm = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_Firm");
            FormatSettingsEndianness = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_Endianness");
            FormatSettingsSign = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_Sign");
            FormatSettingsLaw = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_Law");
            FormatSettingsITU = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Format_Settings_ITU");
            MuxingMode = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "MuxingMode");
            CodecID = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CodecID");
            CodecID_Info = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CodecID/Info");
            CodecID_Url = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CodecID/Url");
            CodecID_Hint = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CodecID/Hint");
            CodecIDDescription = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CodecID_Description");
            Duration = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Duration");
            Duration_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Duration/String");
            Duration_String1 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Duration/String1");
            Duration_String2 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Duration/String2");
            Duration_String3 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Duration/String3");
            BitRateMode = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Mode");
            BitRateMode_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Mode/String");
            BitRate = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate");
            BitRate_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate/String");
            BitRateMinimum = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Minimum");
            BitRateMinimum_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Minimum/String");
            BitRateNominal = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Nominal");
            BitRateNominal_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Nominal/String");
            BitRateMaximum = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Maximum");
            BitRateMaximum_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "BitRate_Maximum/String");
            Channels = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Channel(s)");
            Channels_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Channel(s)/String");
            ChannelMode = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ChannelMode");
            ChannelPositions = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ChannelPositions");
            ChannelPositions_String2 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ChannelPositions/String2");
            SamplingRate = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "SamplingRate");
            SamplingRate_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "SamplingRate/String");
            SamplingCount = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "SamplingCount");
            Resolution = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Resolution");
            Resolution_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Resolution/String");
            CompressionRatio = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "CompressionRatio");
            Delay = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Delay");
            Delay_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Delay/String");
            Delay_String1 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Delay/String1");
            Delay_String2 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Delay/String2");
            Delay_String3 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Delay/String3");
            VideoDelay = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Video_Delay");
            VideoDelay_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Video_Delay/String");
            VideoDelay_String1 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Video_Delay/String1");
            VideoDelay_String2 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Video_Delay/String2");
            VideoDelay_String3 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Video_Delay/String3");
            ReplayGainGain = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ReplayGain_Gain");
            ReplayGainGain_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ReplayGain_Gain/String");
            ReplayGainPeak = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "ReplayGain_Peak");
            StreamSize = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize");
            StreamSize_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String");
            StreamSize_String1 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String1");
            StreamSize_String2 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String2");
            StreamSize_String3 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String3");
            StreamSize_String4 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String4");
            StreamSize_String5 = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize/String5");
            StreamSizeProportion = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "StreamSize_Proportion");
            Alignment = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Alignment");
            Alignment_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Alignment/String");
            InterleaveVideoFrames = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Interleave_VideoFrames");
            InterleaveDuration = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Interleave_Duration");
            InterleaveDuration_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Interleave_Duration/String");
            InterleavePreload = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Interleave_Preload");
            InterleavePreload_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Interleave_Preload/String");
            Title = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Title");
            EncodedLibrary = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library");
            EncodedLibrary_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library/String");
            EncodedLibrary_Name = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library/Name");
            EncodedLibrary_Version = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library/Version");
            EncodedLibrary_Date = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library/Date");
            EncodedLibrarySettings = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Library_Settings");
            Language = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Language");
            Language_String = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Language/String");
            LanguageMore = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Language_More");
            EncodedDate = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encoded_Date");
            TaggedDate = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Tagged_Date");
            Encryption = MediaInfo.Get(handle, _StreamKind.Audio, trackIndex, "Encryption");
        }

        public string Alignment { get; set; }
        public string Alignment_String { get; set; }
        public string BitRate { get; set; }
        public string BitRateMaximum { get; set; }
        public string BitRateMaximum_String { get; set; }
        public string BitRateMinimum { get; set; }
        public string BitRateMinimum_String { get; set; }
        public string BitRateMode { get; set; }
        public string BitRateMode_String { get; set; }
        public string BitRateNominal { get; set; }
        public string BitRateNominal_String { get; set; }
        public string BitRate_String { get; set; }
        public string ChannelMode { get; set; }
        public string ChannelPositions { get; set; }
        public string ChannelPositions_String2 { get; set; }
        public string Channels { get; set; }
        public string Channels_String { get; set; }
        public string CodecID { get; set; }
        public string CodecIDDescription { get; set; }
        public string CodecID_Hint { get; set; }
        public string CodecID_Info { get; set; }
        public string CodecID_Url { get; set; }
        public string CompressionRatio { get; set; }
        public string Count { get; set; }
        public string Delay { get; set; }
        public string Delay_String { get; set; }
        public string Delay_String1 { get; set; }
        public string Delay_String2 { get; set; }
        public string Delay_String3 { get; set; }
        public string Duration { get; set; }
        public string Duration_String { get; set; }
        public string Duration_String1 { get; set; }
        public string Duration_String2 { get; set; }
        public string Duration_String3 { get; set; }
        public string EncodedDate { get; set; }
        public string EncodedLibrary { get; set; }
        public string EncodedLibrary_Date { get; set; }
        public string EncodedLibrary_Name { get; set; }
        public string EncodedLibrarySettings { get; set; }
        public string EncodedLibrary_String { get; set; }
        public string EncodedLibrary_Version { get; set; }
        public string Encryption { get; set; }
        public string Format { get; set; }
        public string Format_Info { get; set; }
        public string FormatProfile { get; set; }
        public string FormatSettings { get; set; }
        public string FormatSettingsEndianness { get; set; }
        public string FormatSettingsFirm { get; set; }
        public string FormatSettingsFloor { get; set; }
        public string FormatSettingsITU { get; set; }
        public string FormatSettingsLaw { get; set; }
        public string FormatSettingsPS { get; set; }
        public string FormatSettingsPS_String { get; set; }
        public string FormatSettingsSBR { get; set; }
        public string FormatSettingsSBR_String { get; set; }
        public string FormatSettingsSign { get; set; }
        public string Format_Url { get; set; }
        public string FormatVersion { get; set; }
        public string ID { get; set; }
        public string ID_String { get; set; }
        public string Inform { get; set; }
        public string InterleaveDuration { get; set; }
        public string InterleaveDuration_String { get; set; }
        public string InterleavePreload { get; set; }
        public string InterleavePreload_String { get; set; }
        public string InterleaveVideoFrames { get; set; }
        public string Language { get; set; }
        public string LanguageMore { get; set; }
        public string Language_String { get; set; }
        public string MenuID { get; set; }
        public string MenuID_String { get; set; }
        public string MuxingMode { get; set; }
        public string ReplayGainGain { get; set; }
        public string ReplayGainGain_String { get; set; }
        public string ReplayGainPeak { get; set; }
        public string Resolution { get; set; }
        public string Resolution_String { get; set; }
        public string SamplingCount { get; set; }
        public string SamplingRate { get; set; }
        public string SamplingRate_String { get; set; }
        public string StreamCount { get; set; }
        public string StreamKind { get; set; }
        public string StreamKindID { get; set; }
        public string StreamKindPos { get; set; }
        public string StreamKind_String { get; set; }
        public string StreamSize { get; set; }
        public string StreamSizeProportion { get; set; }
        public string StreamSize_String { get; set; }
        public string StreamSize_String1 { get; set; }
        public string StreamSize_String2 { get; set; }
        public string StreamSize_String3 { get; set; }
        public string StreamSize_String4 { get; set; }
        public string StreamSize_String5 { get; set; }
        public string TaggedDate { get; set; }
        public string Title { get; set; }
        public string UniqueID { get; set; }
        public string VideoDelay { get; set; }
        public string VideoDelay_String { get; set; }
        public string VideoDelay_String1 { get; set; }
        public string VideoDelay_String2 { get; set; }
        public string VideoDelay_String3 { get; set; }
    }
}
