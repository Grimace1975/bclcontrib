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
    /// GeneralTrack
    /// </summary>
    public class GeneralTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralTrack"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="trackIndex">Index of the track.</param>
        public GeneralTrack(MediaVisitor visitor, uint trackIndex)
        {
            if (trackIndex >= visitor.GeneralTracks)
            {
                throw new ArgumentOutOfRangeException("trackIndex");
            }
            var handle = visitor._handle;
            Count = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Count");
            StreamCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "StreamCount");
            StreamKind = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "StreamKind");
            StreamKindID = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "StreamKindID");
            Inform = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Inform");
            ID = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ID");
            UniqueID = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "UniqueID");
            GeneralCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "GeneralCount");
            VideoCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "VideoCount");
            AudioCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "AudioCount");
            TextCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "TextCount");
            ChaptersCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ChaptersCount");
            ImageCount = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ImageCount");
            CompleteName = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "CompleteName");
            FolderName = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FolderName");
            FileName = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileName");
            FileExtension = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileExtension");
            FileSize = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize");
            FileSize_String = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize/String");
            FileSize_String1 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize/String1");
            FileSize_String2 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize/String2");
            FileSize_String3 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize/String3");
            FileSize_String4 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "FileSize/String4");
            Format = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Format");
            Format_String = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Format/String");
            Format_Info = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Format/Info");
            Format_Url = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Format/Url");
            Format_Extensions = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Format/Extensions");
            OveralBitRate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "OveralBitRate");
            OveralBitRate_String = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "OveralBitRate/String");
            PlayTime = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "PlayTime");
            PlayTime_String = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "PlayTime/String");
            PlayTime_String1 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "PlayTime/String1");
            PlayTime_String2 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "PlayTime/String2");
            PlayTime_String3 = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "PlayTime/String3");
            Title = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Title");
            Title_More = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Title/More");
            Domain = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Domain");
            Collection = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Collection");
            Collection_TotalParts = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Collection/Total_Parts");
            Season = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Season");
            Movie = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Movie");
            Movie_More = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Movie/More");
            Album = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Album");
            Album_TotalParts = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Album/Total_Parts");
            Album_Sort = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Album/Sort");
            Comic = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Comic");
            Comic_TotalParts = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Comic/Total_Parts");
            Part = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Part");
            Part_TotalParts = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Part/Total_Parts");
            Part_Position = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Part/Position");
            Track = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Track");
            Track_Position = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Track/Position");
            Track_More = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Track/More");
            Track_Sort = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Track/Sort");
            Chapter = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Chapter");
            SubTrack = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "SubTrack");
            Original_Album = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Album");
            Original_Movie = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Movie");
            Original_Part = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Part");
            Original_Track = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Track");
            Author = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Author");
            Artist = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Artist");
            Performer_Sort = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Performer/Sort");
            Original_Performer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Performer");
            Accompaniment = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Accompaniment");
            MusicianInstrument = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Musician_Instrument");
            Composer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Composer");
            Composer_Nationality = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Composer/Nationality");
            Arranger = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Arranger");
            Lyricist = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Lyricist");
            Original_Lyricist = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Lyricist");
            Conductor = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Conductor");
            Actor = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Actor");
            ActorCharacter = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Actor_Character");
            WrittenBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "WrittenBy");
            ScreenplayBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ScreenplayBy");
            Director = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Director");
            AssistantDirector = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "AssistantDirector");
            DirectorOfPhotography = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "DirectorOfPhotography");
            ArtDirector = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ArtDirector");
            EditedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "EditedBy");
            Producer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Producer");
            CoProducer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "CoProducer");
            ExecutiveProducer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ExecutiveProducer");
            ProductionDesigner = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ProductionDesigner");
            CostumeDesigner = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "CostumeDesigner");
            Choregrapher = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Choregrapher");
            SoundEngineer = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "SoundEngineer");
            MasteredBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "MasteredBy");
            RemixedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "RemixedBy");
            ProductionStudio = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ProductionStudio");
            Publisher = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Publisher");
            Publisher_URL = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Publisher/URL");
            DistributedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "DistributedBy");
            EncodedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "EncodedBy");
            ThanksTo = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ThanksTo");
            Technician = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Technician");
            CommissionedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "CommissionedBy");
            EncodedOriginal_DistributedBy = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Original/DistributedBy");
            RadioStation = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "RadioStation");
            RadioStation_Owner = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "RadioStation/Owner");
            RadioStation_URL = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "RadioStation/URL");
            ContentType = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ContentType");
            Subject = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Subject");
            Synopsys = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Synopsys");
            Summary = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Summary");
            Description = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Description");
            Keywords = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Keywords");
            Period = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Period");
            LawRating = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "LawRating");
            IRCA = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "IRCA");
            Language = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Language");
            Medium = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Medium");
            Product = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Product");
            Country = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Country");
            WrittenDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Written_Date");
            RecordedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Recorded_Date");
            ReleasedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Released_Date");
            MasteredDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Mastered_Date");
            EncodedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Date");
            TaggedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Tagged_Date");
            Original_ReleasedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Released_Date");
            Original_RecordedDate = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Original/Recorded_Date");
            WrittenLocation = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Written_Location");
            RecordedLocation = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Recorded_Location");
            ArchivalLocation = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Archival_Location");
            Genre = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Genre");
            Mood = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Mood");
            Comment = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Comment");
            Rating = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Rating ");
            EncodedApplication = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Application");
            EncodedLibrary = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Library");
            EncodedLibrarySettings = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Library_Settings");
            EncodedOriginal = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Original");
            EncodedOriginal_Url = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Encoded_Original/Url");
            Copyright = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Copyright");
            ProducerCopyright = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Producer_Copyright");
            TermsOfUse = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "TermsOfUse");
            Copyright_Url = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Copyright/Url");
            ISRC = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ISRC");
            MSDI = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "MSDI");
            ISBN = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "ISBN");
            BarCode = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "BarCode");
            LCCN = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "LCCN");
            CatalogNumber = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "CatalogNumber");
            LabelCode = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "LabelCode");
            Cover = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Cover");
            CoverDatas = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Cover_Datas");
            BPM = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "BPM");
            VideoCodecList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Video_Codec_List");
            VideoLanguageList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Video_Language_List");
            AudioCodecList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Audio_Codec_List");
            AudioLanguageList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Audio_Language_List");
            TextCodecList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Text_Codec_List");
            TextLanguageList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Text_Language_List");
            ChaptersCodecList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Chapters_Codec_List");
            ChaptersLanguageList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Chapters_Language_List");
            ImageCodecList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Image_Codec_List");
            ImageLanguageList = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Image_Language_List");
            Other = MediaInfo.Get(handle, _StreamKind.General, trackIndex, "Other");
        }

        public string Accompaniment { get; set; }
        public string Actor { get; set; }
        public string ActorCharacter { get; set; }
        public string Album { get; set; }
        public string Album_Sort { get; set; }
        public string Album_TotalParts { get; set; }
        public string ArchivalLocation { get; set; }
        public string Arranger { get; set; }
        public string ArtDirector { get; set; }
        public string Artist { get; set; }
        public string AssistantDirector { get; set; }
        public string AudioCodecList { get; set; }
        public string AudioCount { get; set; }
        public string AudioLanguageList { get; set; }
        public string Author { get; set; }
        public string BarCode { get; set; }
        public string BPM { get; set; }
        public string CatalogNumber { get; set; }
        public string Chapter { get; set; }
        public string ChaptersCodecList { get; set; }
        public string ChaptersCount { get; set; }
        public string ChaptersLanguageList { get; set; }
        public string Choregrapher { get; set; }
        public string Collection { get; set; }
        public string Collection_TotalParts { get; set; }
        public string Comic { get; set; }
        public string Comic_TotalParts { get; set; }
        public string Comment { get; set; }
        public string CommissionedBy { get; set; }
        public string CompleteName { get; set; }
        public string Composer { get; set; }
        public string Composer_Nationality { get; set; }
        public string Conductor { get; set; }
        public string ContentType { get; set; }
        public string CoProducer { get; set; }
        public string Copyright { get; set; }
        public string Copyright_Url { get; set; }
        public string CostumeDesigner { get; set; }
        public string Count { get; set; }
        public string Country { get; set; }
        public string Cover { get; set; }
        public string CoverDatas { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string DirectorOfPhotography { get; set; }
        public string DistributedBy { get; set; }
        public string Domain { get; set; }
        public string EditedBy { get; set; }
        public string EncodedApplication { get; set; }
        public string EncodedBy { get; set; }
        public string EncodedDate { get; set; }
        public string EncodedLibrary { get; set; }
        public string EncodedLibrarySettings { get; set; }
        public string EncodedOriginal { get; set; }
        public string EncodedOriginal_DistributedBy { get; set; }
        public string EncodedOriginal_Url { get; set; }
        public string ExecutiveProducer { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileSize_String { get; set; }
        public string FileSize_String1 { get; set; }
        public string FileSize_String2 { get; set; }
        public string FileSize_String3 { get; set; }
        public string FileSize_String4 { get; set; }
        public string FolderName { get; set; }
        public string Format { get; set; }
        public string Format_Extensions { get; set; }
        public string Format_Info { get; set; }
        public string Format_String { get; set; }
        public string Format_Url { get; set; }
        public string GeneralCount { get; set; }
        public string Genre { get; set; }
        public string ID { get; set; }
        public string ImageCodecList { get; set; }
        public string ImageCount { get; set; }
        public string ImageLanguageList { get; set; }
        public string Inform { get; set; }
        public string IRCA { get; set; }
        public string ISBN { get; set; }
        public string ISRC { get; set; }
        public string Keywords { get; set; }
        public string LabelCode { get; set; }
        public string Language { get; set; }
        public string LawRating { get; set; }
        public string LCCN { get; set; }
        public string Lyricist { get; set; }
        public string MasteredBy { get; set; }
        public string MasteredDate { get; set; }
        public string Medium { get; set; }
        public string Mood { get; set; }
        public string Movie { get; set; }
        public string Movie_More { get; set; }
        public string MSDI { get; set; }
        public string MusicianInstrument { get; set; }
        public string Original_Album { get; set; }
        public string Original_Lyricist { get; set; }
        public string Original_Movie { get; set; }
        public string Original_Part { get; set; }
        public string Original_Performer { get; set; }
        public string Original_RecordedDate { get; set; }
        public string Original_ReleasedDate { get; set; }
        public string Original_Track { get; set; }
        public string Other { get; set; }
        public string OveralBitRate { get; set; }
        public string OveralBitRate_String { get; set; }
        public string Part { get; set; }
        public string Part_Position { get; set; }
        public string Part_TotalParts { get; set; }
        public string Performer_Sort { get; set; }
        public string Period { get; set; }
        public string PlayTime { get; set; }
        public string PlayTime_String { get; set; }
        public string PlayTime_String1 { get; set; }
        public string PlayTime_String2 { get; set; }
        public string PlayTime_String3 { get; set; }
        public string Producer { get; set; }
        public string ProducerCopyright { get; set; }
        public string Product { get; set; }
        public string ProductionDesigner { get; set; }
        public string ProductionStudio { get; set; }
        public string Publisher { get; set; }
        public string Publisher_URL { get; set; }
        public string RadioStation { get; set; }
        public string RadioStation_Owner { get; set; }
        public string RadioStation_URL { get; set; }
        public string Rating { get; set; }
        public string RecordedDate { get; set; }
        public string RecordedLocation { get; set; }
        public string ReleasedDate { get; set; }
        public string RemixedBy { get; set; }
        public string ScreenplayBy { get; set; }
        public string Season { get; set; }
        public string SoundEngineer { get; set; }
        public string StreamCount { get; set; }
        public string StreamKind { get; set; }
        public string StreamKindID { get; set; }
        public string Subject { get; set; }
        public string SubTrack { get; set; }
        public string Summary { get; set; }
        public string Synopsys { get; set; }
        public string TaggedDate { get; set; }
        public string Technician { get; set; }
        public string TermsOfUse { get; set; }
        public string TextCodecList { get; set; }
        public string TextCount { get; set; }
        public string TextLanguageList { get; set; }
        public string ThanksTo { get; set; }
        public string Title { get; set; }
        public string Title_More { get; set; }
        public string Track { get; set; }
        public string Track_More { get; set; }
        public string Track_Position { get; set; }
        public string Track_Sort { get; set; }
        public string UniqueID { get; set; }
        public string VideoCodecList { get; set; }
        public string VideoCount { get; set; }
        public string VideoLanguageList { get; set; }
        public string WrittenBy { get; set; }
        public string WrittenDate { get; set; }
        public string WrittenLocation { get; set; }
    }
}
