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
using System.Web.UI.HtmlControls;
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// FacebookSharedContent control.
    /// http://www.facebook.com/share_partners.php
    /// </summary>
    public class FacebookSharedContent : HtmlContainerControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookSharedContent"/> class.
        /// </summary>
        public FacebookSharedContent()
            : base() { }

        /// <summary>
        /// Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl"/> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter"/> object.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> that receives the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl"/> content.</param>
        protected override void Render(HtmlTextWriter w)
        {
            // common
            RenderRelLink(w, "facebook_image_src", "image_src", ImageSource);
            // audio
            RenderRelLink(w, "facebook_audio_src", "audio_src", AudioSource);
            RenderMeta(w, "facebook_audio_type", "audio_type", AudioType);
            RenderMeta(w, "facebook_audio_title", "audio_title", AudioTitle);
            RenderMeta(w, "facebook_audio_artist", "audio_artist", AudioArtist);
            RenderMeta(w, "facebook_audio_album", "audio_album", AudioAlbum);
            // video
            RenderRelLink(w, "facebook_video_src", "video_src", VideoSource);
            RenderMeta(w, "facebook_video_height", "video_height", VideoHeight);
            RenderMeta(w, "facebook_video_width", "video_width", VideoWidth);
            RenderMeta(w, "facebook_video_type", "video_type", VideoType);
        }

        /// <summary>
        /// Renders the rel link.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="id">The id.</param>
        /// <param name="rel">The rel.</param>
        /// <param name="href">The href.</param>
        private void RenderRelLink(HtmlTextWriter w, string id, string rel, string href)
        {
            if (string.IsNullOrEmpty(href))
                return;
            var htmlLink = new HtmlLink { ID = id, Href = href };
            htmlLink.Attributes["rel"] = rel;
            htmlLink.RenderControl(w);
        }

        /// <summary>
        /// Renders the meta.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        private void RenderMeta(HtmlTextWriter w, string id, string name, string content)
        {
            if (string.IsNullOrEmpty(content))
                return;
            var htmlMeta = new HtmlMeta { ID = id, Name = name, Content = content };
            htmlMeta.RenderControl(w);
        }

        /// <summary>
        /// Gets or sets the audio album.
        /// </summary>
        /// <value>The audio album.</value>
        public string AudioAlbum { get; set; }

        /// <summary>
        /// Gets or sets the audio artist.
        /// </summary>
        /// <value>The audio artist.</value>
        public string AudioArtist { get; set; }

        /// <summary>
        /// Gets or sets the audio source.
        /// </summary>
        /// <value>The audio source.</value>
        public string AudioSource { get; set; }

        /// <summary>
        /// Gets or sets the audio title.
        /// </summary>
        /// <value>The audio title.</value>
        public string AudioTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the audio.
        /// </summary>
        /// <value>The type of the audio.</value>
        public string AudioType { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>The image source.</value>
        public string ImageSource { get; set; }

        /// <summary>
        /// Gets or sets the height of the video.
        /// </summary>
        /// <value>The height of the video.</value>
        public string VideoHeight { get; set; }

        /// <summary>
        /// Gets or sets the video source.
        /// </summary>
        /// <value>The video source.</value>
        public string VideoSource { get; set; }

        /// <summary>
        /// Gets or sets the type of the video.
        /// </summary>
        /// <value>The type of the video.</value>
        public string VideoType { get; set; }

        /// <summary>
        /// Gets or sets the width of the video.
        /// </summary>
        /// <value>The width of the video.</value>
        public string VideoWidth { get; set; }
    }
}