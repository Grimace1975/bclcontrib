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
// http://code.google.com/p/swfupload/
using System;
using System.Text;
namespace System.Web.UI.ClientShapes
{
    /// <summary>
    /// SwfUploadShape
    /// </summary>
    public class SwfUploadShape : ClientScriptItemBase
    {
        public SwfUploadShape()
            : this(null) { }
        public SwfUploadShape(object settings)
            : this(NewAttrib.Parse(settings)) { }
        public SwfUploadShape(NewAttrib settings)
        {
            var registrar = ClientScriptRegistrarSwfUploadShape.AssertRegistered();
            Settings = settings;
            // set defaults
            UploadUrl = DefaultValues.UploadUrl;
            FilePostName = DefaultValues.FilePostName;
            FileTypes = DefaultValues.FileTypes;
            FileTypesDescription = DefaultValues.FileTypesDescription;
            FileSizeLimit = DefaultValues.FileSizeLimit;
            FlashUrl = DefaultValues.FlashUrl;
            PreventSwfCaching = DefaultValues.PreventSwfCaching;
            ButtonPlaceholderId = DefaultValues.ButtonPlaceholderId;
            ButtonImageUrl = DefaultValues.ButtonImageUrl;
            ButtonWidth = DefaultValues.ButtonWidth;
            ButtonHeight = DefaultValues.ButtonHeight;
            ButtonText = DefaultValues.ButtonText;
            ButtonTextStyle = DefaultValues.ButtonTextStyle;
            ButtonAction = DefaultValues.ButtonAction;
            //
            FlashUrl = registrar.SwfUploadFlashUrl;
        }

        protected ClientScriptRegistrarSwfUploadShape Registrar { get; set; }

        public override void Render(StringBuilder b)
        {
            var options = MakeOptions();
            if (Settings != null)
                foreach (var setting in Settings.Values)
                {
                    var optionBuilder = (setting as IClientScriptItemOption);
                    if (optionBuilder != null)
                        options.AddRange(optionBuilder.MakeOption());
                }
            string optionsAsText = ClientScript.EncodeDictionary(options, true);
            b.AppendLine("new SWFUpload(" + (optionsAsText.Length > 2 ? optionsAsText : ClientScript.EmptyObject) + ");");
        }

        public NewAttrib Settings { get; set; }

        #region Options

        public enum Action
        {
            SelectFile,
            SelectFiles,
            StartUpload,
        }

        public enum Cursor
        {
            Arrow,
            Hand,
        }

        public enum WindowMode
        {
            Window,
            Transparent,
            Opaque,
        }

        public static class DefaultValues
        {
            // UPLOAD BACKEND SETTINGS
            public static readonly string UploadUrl = string.Empty;
            public static readonly bool PreserveRelativeUrls = false;
            public static readonly string FilePostName = "Filedata";
            public static readonly object PostParams = null;
            public static readonly bool UseQueryString = false;
            public static readonly bool RequeueOnError = false;
            public static readonly string[] HttpSuccess = null;
            public static readonly int AssumeSuccessTimeout = 0;
            // FILE SETTINGS
            public static readonly string FileTypes = "*.*";
            public static readonly string FileTypesDescription = "All Files";
            public static readonly string FileSizeLimit = "0"; //- default zero means "unlimited"
            public static readonly int FileUploadLimit = 0;
            public static readonly int FileQueueLimit = 0;
            // FLASH SETTINGS
            public static readonly string FlashUrl = "swfupload.swf";
            public static readonly bool PreventSwfCaching = true;
            public static readonly bool Debug = false;
            // BUTTON SETTINGS
            public static readonly string ButtonImageUrl = string.Empty;
            public static readonly int ButtonWidth = 1;
            public static readonly int ButtonHeight = 1;
            public static readonly string ButtonText = string.Empty;
            public static readonly string ButtonTextStyle = "color: #000000; font-size: 16pt;";
            public static readonly int ButtonTextTopPadding = 0;
            public static readonly int ButtonTextLeftPadding = 0;
            public static readonly Action ButtonAction = Action.SelectFiles;
            public static readonly bool ButtonDisabled = false;
            public static readonly string ButtonPlaceholderId = string.Empty;
            public static readonly string ButtonPlaceholder = null;
            public static readonly Cursor ButtonCursor = Cursor.Arrow;
            public static readonly WindowMode ButtonWindowMode = WindowMode.Window;
            // EVENT HANDLERS
            public static readonly string SwfUploadLoadedHandler = null;
            public static readonly string FileDialogStartHandler = null;
            public static readonly string FileQueuedHandler = null;
            public static readonly string FileQueueErrorHandler = null;
            public static readonly string FileDialogCompleteHandler = null;
            public static readonly string UploadStartHandler = null;
            public static readonly string UploadProgressHandler = null;
            public static readonly string UploadErrorHandler = null;
            public static readonly string UploadSuccessHandler = null;
            public static readonly string UploadCompleteHandler = null;
            public static readonly string DebugHandler = null;
            // CUSTOM SETTINGS
            public static readonly object CustomSettings = null;
        }

        protected virtual NewAttrib MakeOptions()
        {
            var options = new NewAttrib();
            // UPLOAD BACKEND SETTINGS
            if (UploadUrl != DefaultValues.UploadUrl)
                options["upload_url"] = ClientScript.EncodeText(UploadUrl);
            if (FilePostName != DefaultValues.FilePostName)
                options["file_post_name"] = ClientScript.EncodeText(FilePostName);
            if (PostParams != DefaultValues.PostParams)
                options["post_params"] = ClientScript.EncodeDictionary(NewAttrib.Parse(PostParams));
            if (UseQueryString != DefaultValues.UseQueryString)
                options["use_query_string"] = ClientScript.EncodeBool(UseQueryString);
            if (PreserveRelativeUrls != DefaultValues.PreserveRelativeUrls)
                options["preserve_relative_urls"] = ClientScript.EncodeBool(PreserveRelativeUrls);
            if (RequeueOnError != DefaultValues.RequeueOnError)
                options["requeue_on_error"] = ClientScript.EncodeBool(RequeueOnError);
            if (HttpSuccess != DefaultValues.HttpSuccess)
                options["http_success"] = ClientScript.EncodeArray(HttpSuccess);
            if (AssumeSuccessTimeout != DefaultValues.AssumeSuccessTimeout)
                options["assume_success_timeout"] = ClientScript.EncodeInt32(AssumeSuccessTimeout);
            // FILE SETTINGS
            if (FileTypes != DefaultValues.FileTypes)
                options["file_types"] = ClientScript.EncodeText(FileTypes);
            if (FileTypesDescription != DefaultValues.FileTypesDescription)
                options["file_types_description"] = ClientScript.EncodeText(FileTypesDescription);
            if (FileSizeLimit != DefaultValues.FileSizeLimit)
                options["file_size_limit"] = ClientScript.EncodeText(FileSizeLimit);
            if (FileUploadLimit != DefaultValues.FileUploadLimit)
                options["file_upload_limit"] = ClientScript.EncodeInt32(FileUploadLimit);
            if (FileQueueLimit != DefaultValues.FileQueueLimit)
                options["file_queue_limit"] = ClientScript.EncodeInt32(FileQueueLimit);
            // FLASH SETTINGS
            if (FlashUrl != DefaultValues.FlashUrl)
                options["flash_url"] = ClientScript.EncodeText(FlashUrl);
            if (FlashWidth != null)
                options["flash_width"] = ClientScript.EncodeText(FlashWidth);
            if (FlashHeight != null)
                options["flash_height"] = ClientScript.EncodeText(FlashHeight);
            if (FlashColor != null)
                options["flash_color"] = ClientScript.EncodeText(FlashColor);
            if (PreventSwfCaching != DefaultValues.PreventSwfCaching)
                options["prevent_swf_caching"] = ClientScript.EncodeBool(PreventSwfCaching);
            if (Debug != DefaultValues.Debug)
                options["debug"] = ClientScript.EncodeBool(Debug);
            // BUTTON SETTINGS
            if (ButtonPlaceholderId != DefaultValues.ButtonPlaceholderId)
                options["button_placeholder_id"] = ClientScript.EncodeText(ButtonPlaceholderId);
            if (ButtonPlaceholder != DefaultValues.ButtonPlaceholder)
                options["button_placeholder"] = ClientScript.EncodeExpression(ButtonPlaceholder);
            if (ButtonImageUrl != DefaultValues.ButtonImageUrl)
                options["button_image_url"] = ClientScript.EncodeText(ButtonImageUrl);
            if (ButtonWidth != DefaultValues.ButtonWidth)
                options["button_width"] = ClientScript.EncodeInt32(ButtonWidth);
            if (ButtonHeight != DefaultValues.ButtonHeight)
                options["button_height"] = ClientScript.EncodeInt32(ButtonHeight);
            if (ButtonText != DefaultValues.ButtonText)
                options["button_text"] = ClientScript.EncodeText(ButtonText);
            if (ButtonTextStyle != DefaultValues.ButtonTextStyle)
                options["button_text_style"] = ClientScript.EncodeText(ButtonTextStyle);
            if (ButtonTextTopPadding != DefaultValues.ButtonTextTopPadding)
                options["button_text_top_padding"] = ClientScript.EncodeInt32(ButtonTextTopPadding);
            if (ButtonTextLeftPadding != DefaultValues.ButtonTextLeftPadding)
                options["button_text_left_padding"] = ClientScript.EncodeInt32(ButtonTextLeftPadding);
            if (ButtonAction != DefaultValues.ButtonAction)
                switch (ButtonAction)
                {
                    case Action.SelectFile:
                        options["button_action"] = "SWFUpload.BUTTON_ACTION.SELECT_FILE";
                        break;
                    case Action.SelectFiles:
                        options["button_action"] = "SWFUpload.BUTTON_ACTION.SELECT_FILES";
                        break;
                    case Action.StartUpload:
                        options["button_action"] = "SWFUpload.BUTTON_ACTION.START_UPLOAD";
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            if (ButtonDisabled != DefaultValues.ButtonDisabled)
                options["button_disabled"] = ClientScript.EncodeBool(ButtonDisabled);
            if (ButtonCursor != DefaultValues.ButtonCursor)
                switch (ButtonCursor)
                {
                    case Cursor.Arrow:
                        options["button_cursor"] = "SWFUpload.CURSOR.ARROW";
                        break;
                    case Cursor.Hand:
                        options["button_cursor"] = "SWFUpload.CURSOR.HAND";
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            if (ButtonWindowMode != DefaultValues.ButtonWindowMode)
                switch (ButtonWindowMode)
                {
                    case WindowMode.Window:
                        options["button_window_mode"] = "SWFUpload.WINDOW_MODE.WINDOW";
                        break;
                    case WindowMode.Transparent:
                        options["button_window_mode"] = "SWFUpload.WINDOW_MODE.TRANSPARENT";
                        break;
                    case WindowMode.Opaque:
                        options["button_window_mode"] = "SWFUpload.WINDOW_MODE.OPAQUE";
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            // EVENT HANDLERS
            if (SwfUploadLoadedHandler != DefaultValues.SwfUploadLoadedHandler)
                options["swfupload_loaded_handler"] = ClientScript.EncodeExpression(SwfUploadLoadedHandler);
            if (FileDialogStartHandler != DefaultValues.FileDialogStartHandler)
                options["file_dialog_start_handler"] = ClientScript.EncodeExpression(FileDialogStartHandler);
            if (FileQueuedHandler != DefaultValues.FileQueuedHandler)
                options["file_queued_handler"] = ClientScript.EncodeExpression(FileQueuedHandler);
            if (FileQueueErrorHandler != DefaultValues.FileQueueErrorHandler)
                options["file_queue_error_handler"] = ClientScript.EncodeExpression(FileQueueErrorHandler);
            if (FileDialogCompleteHandler != DefaultValues.FileDialogCompleteHandler)
                options["file_dialog_complete_handler"] = ClientScript.EncodeExpression(FileDialogCompleteHandler);
            if (UploadStartHandler != DefaultValues.UploadStartHandler)
                options["upload_start_handler"] = ClientScript.EncodeExpression(UploadStartHandler);
            if (UploadProgressHandler != DefaultValues.UploadProgressHandler)
                options["upload_progress_handler"] = ClientScript.EncodeExpression(UploadProgressHandler);
            if (UploadErrorHandler != DefaultValues.UploadErrorHandler)
                options["upload_error_handler"] = ClientScript.EncodeExpression(UploadErrorHandler);
            if (UploadSuccessHandler != DefaultValues.UploadSuccessHandler)
                options["upload_success_handler"] = ClientScript.EncodeExpression(UploadSuccessHandler);
            if (UploadCompleteHandler != DefaultValues.UploadCompleteHandler)
                options["upload_complete_handler"] = ClientScript.EncodeExpression(UploadCompleteHandler);
            if (DebugHandler != DefaultValues.DebugHandler)
                options["debug_handler"] = ClientScript.EncodeExpression(DebugHandler);
            // CUSTOM SETTINGS
            if (CustomSettings != DefaultValues.CustomSettings)
                options["custom_settings"] = ClientScript.EncodeDictionary(NewAttrib.Parse(CustomSettings));
            return options;
        }

        /// <summary>
        /// Gets or sets the upload URL.
        /// </summary>
        /// <value>The upload URL.</value>
        /// <remarks>
        /// The upload_url setting accepts a full, absolute, or relative target URL for the uploaded file. Relative URLs should be relative to document. The upload_url should be in the same domain as the Flash Control for best compatibility.
        /// If the preserve_relative_urls setting is false SWFUpload will convert the relative URL to an absolute URL to avoid the URL being interpreted differently by the Flash Player on different platforms. If you disable SWFUploads conversion of the URL relative URLs should be relative to the swfupload.swf file.
        /// </remarks>
        public string UploadUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the file post.
        /// </summary>
        /// <value>The name of the file post.</value>
        /// <remarks>
        /// The file_post_name allows you to set the value name used to post the file. This is not related to the file name. The default value is 'Filedata'. For maximum compatibility it is recommended that the default value is used.
        /// </remarks>
        public string FilePostName { get; set; }

        /// <summary>
        /// Gets or sets the post params.
        /// </summary>
        /// <value>The post params.</value>
        /// <remarks>
        /// The post_params setting defines the name/value pairs that will be posted with each uploaded file. This setting accepts a simple JavaScript object. Multiple post name/value pairs should be defined as demonstrated in the sample settings object. Values must be either strings or numbers (as interpreted by the JavaScript typeof function).
        /// Note: Flash Player 8 does not support sending additional post parameters. SWFUpload will automatically send the post_params as part of the query string.
        /// </remarks>
        public object PostParams { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use query string].
        /// </summary>
        /// <value><c>true</c> if [use query string]; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// The use_query_string setting may be true or false. This value indicates whether SWFUpload should send the post_params and file params on the query string or the post. This setting was introduced in SWFUpload v2.1.0.
        /// </remarks>
        public bool UseQueryString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [preserve relative urls].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [preserve relative urls]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// A boolean value that indicates whether SWFUpload should attempt to convert relative URLs used by the Flash Player to absolute URLs. If set to true SWFUpload will not modify any URLs. The default value is false.
        /// </remarks>
        public bool PreserveRelativeUrls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [requeue on error].
        /// </summary>
        /// <value><c>true</c> if [requeue on error]; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// The requeue_on_error setting may be true or false. When this setting is true any files that has an uploadError (excluding fileQueue errors and the FILE_CANCELLED uploadError) is returned to the front of the queue rather than being discarded. The file can be uploaded again if needed. To remove the file from the queue the cancelUpload method must be called.
        /// All the events associated with a failed upload are still called and so the requeuing the failed upload can conflict with the Queue Plugin (or custom code that uploads the entire queue). Code that automatically uploads the next file in the queue will upload the failed file over and over again if care is not taken to allow the failing upload to be cancelled.
        /// This setting was introduced in SWFUpload v2.1.0.
        /// </remarks>
        public bool RequeueOnError { get; set; }

        /// <summary>
        /// Gets or sets the HTTP success.
        /// </summary>
        /// <value>The HTTP success.</value>
        /// <remarks>
        /// An array that defines the HTTP Status Codes that will trigger success. 200 is always a success. Also, only the 200 status code provides the serverData.
        /// When returning and accepting an HTTP Status Code other than 200 it is not necessary for the server to return content.
        /// </remarks>
        public string[] HttpSuccess { get; set; }

        /// <summary>
        /// Gets or sets the assume success timeout.
        /// </summary>
        /// <value>The assume success timeout.</value>
        /// <remarks>
        /// The number of seconds SWFUpload should wait for Flash to detect the server's response after the file has finished uploading. This setting allows you to work around the Flash Player bugs where long running server side scripts causes Flash to ignore the server response or the Mac Flash Player bug that ignores server responses with no content.
        /// Testing has shown that Flash will ignore server responses that take longer than 30 seconds after the last uploadProgress event.
        /// A timeout of zero (0) seconds disables this feature and is the default value. SWFUpload will wait indefinitely for the Flash Player to trigger the uploadSuccess event.
        /// </remarks>
        public int AssumeSuccessTimeout { get; set; }

        /// <summary>
        /// Gets or sets the file types.
        /// </summary>
        /// <value>The file types.</value>
        /// <remarks>
        /// The file_types setting accepts a semi-colon separated list of file extensions that are allowed to be selected by the user. Use '*.*' to allow all file types.
        /// </remarks>
        public string FileTypes { get; set; }

        /// <summary>
        /// Gets or sets the file types description.
        /// </summary>
        /// <value>The file types description.</value>
        /// <remarks>
        /// A text description that is displayed to the user in the File Browser dialog.
        /// </remarks>
        public string FileTypesDescription { get; set; }

        /// <summary>
        /// Gets or sets the file size limit.
        /// </summary>
        /// <value>The file size limit.</value>
        /// <remarks>
        /// The file_size_limit setting defines the maximum allowed size of a file to be uploaded. This setting accepts a value and unit. Valid units are B, KB, MB and GB. If the unit is omitted default is KB. A value of 0 (zero) is interpreted as unlimited.
        /// Note: This setting only applies to the user's browser. It does not affect any settings or limits on the web server.
        /// </remarks>
        public string FileSizeLimit { get; set; }

        /// <summary>
        /// Gets or sets the file upload limit.
        /// </summary>
        /// <value>The file upload limit.</value>
        /// <remarks>
        /// Defines the number of files allowed to be uploaded by SWFUpload. This setting also sets the upper bound of the file_queue_limit setting. Once the user has uploaded or queued the maximum number of files she will no longer be able to queue additional files. The value of 0 (zero) is interpreted as unlimited. Only successful uploads (uploads the trigger the uploadSuccess event) are counted toward the upload limit. The setStats function can be used to modify the number of successful uploads.
        /// Note: This value is not tracked across pages and is reset when a page is refreshed. File quotas should be managed by the web server.
        /// </remarks>
        public int FileUploadLimit { get; set; }

        /// <summary>
        /// Gets or sets the file queue limit.
        /// </summary>
        /// <value>The file queue limit.</value>
        /// <remarks>
        /// Defines the number of unprocessed files allowed to be simultaneously queued. Once a file is uploaded, errored, or cancelled a new files can be queued in its place until the queue limit has been reached. If the upload limit (or remaining uploads allowed) is less than the queue limit then the lower number is used.
        /// </remarks>
        public int FileQueueLimit { get; set; }

        /// <summary>
        /// Gets or sets the flash URL.
        /// </summary>
        /// <value>The flash URL.</value>
        /// <remarks>
        /// The full, absolute, or relative URL to the Flash Control swf file. This setting cannot be changed once the SWFUpload has been instantiated. Relative URLs are relative to the page URL.
        /// </remarks>
        public string FlashUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of the flash.
        /// </summary>
        /// <value>The width of the flash.</value>
        /// <remarks>
        /// (Removed in v2.1.0) Defines the width of the HTML element that contains the flash. Some browsers do not function correctly if this setting is less than 1 px. This setting is optional and has a default value of 1px.
        /// </remarks>
        public string FlashWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the flash.
        /// </summary>
        /// <value>The height of the flash.</value>
        /// <remarks>
        /// (Removed in v2.1.0) Defines the height of the HTML element that contains the flash. Some browsers do not function correctly if this setting is less than 1 px. This setting is optional and has a default value of 1px.
        /// </remarks>
        public string FlashHeight { get; set; }

        /// <summary>
        /// Gets or sets the color of the flash.
        /// </summary>
        /// <value>The color of the flash.</value>
        /// <remarks>
        /// Removed in v2.2.0 This setting sets the background color of the HTML element that contains the flash. The default value is '#FFFFFF'.
        /// Note: This setting may not be effective in "skinning" 1px flash element in all browsers.
        /// </remarks>
        public string FlashColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [prevent SWF caching].
        /// </summary>
        /// <value><c>true</c> if [prevent SWF caching]; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Added in v2.2.0 This boolean setting indicates whether a random value should be added to the Flash URL in an attempt to prevent the browser from caching the SWF movie. This works around a bug in some IE-engine based browsers.
        /// Note: The algorithm for adding the random number to the URL is dumb and cannot handle URLs that already have some parameters.
        /// </remarks>
        public bool PreventSwfCaching { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SwfUpload"/> is debug.
        /// </summary>
        /// <value><c>true</c> if debug; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// A boolean value that defines whether the debug event handler should be fired.
        /// </remarks>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the button placeholder id.
        /// </summary>
        /// <value>The button placeholder id.</value>
        /// <remarks>
        /// (Added in v2.2.0) This required setting sets the ID of DOM element that will be replaced by the Flash Button. This setting overrides the button_placeholder setting. The Flash button can be styled using the CSS class 'swfupload'.
        /// </remarks>
        public string ButtonPlaceholderId { get; set; }

        /// <summary>
        /// Gets or sets the button placeholder.
        /// </summary>
        /// <value>The button placeholder.</value>
        /// <remarks>
        /// (Added in v2.2.0) This required setting sets the DOM element that will be replaced by the Flash Button. This setting is only applied if the button_placeholder_id is not set. The Flash button can be styled using the CSS class 'swfupload'.
        /// </remarks>
        public string ButtonPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets the button image URL.
        /// </summary>
        /// <value>The button image URL.</value>
        /// <remarks>
        /// (Added in v2.2.0) Fully qualified, absolute or relative URL to the image file to be used as the Flash button. Any Flash supported image file format can be used (another SWF file or gif, jpg, or png).
        /// This URL is affected by the preserve_relative_urls setting and should follow the same rules as the upload_url setting.
        /// The button image is treated as a sprite. There are 4 button states that must be represented by the button image. Each button state image should be stacked above the other in this order: normal, hover, down/click, disabled.
        /// </remarks>
        public string ButtonImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of the button.
        /// </summary>
        /// <value>The width of the button.</value>
        /// <remarks>
        /// (Added in v2.2.0) A number defining the width of the Flash button.
        /// </remarks>
        public int ButtonWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the button.
        /// </summary>
        /// <value>The height of the button.</value>
        /// <remarks>
        /// (Added in v2.2.0) A number defining the height of the Flash button. This value should be 1/4th of the height or the button image.
        /// </remarks>
        public int ButtonHeight { get; set; }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        /// <value>The button text.</value>
        /// <remarks>
        /// (Added in v2.2.0) Plain or HTML text that is displayed over the Flash button. HTML text can be further styled using CSS classes and the button_text_style setting. See Adobe's Flash documentation for details.
        /// </remarks>
        public string ButtonText { get; set; }

        /// <summary>
        /// Gets or sets the button text style.
        /// </summary>
        /// <value>The button text style.</value>
        /// <remarks>
        /// (Added in v2.2.0) CSS style string that defines how the button_text is displayed. See Adobe's Flash documentation for details.
        /// </remarks>
        public string ButtonTextStyle { get; set; }

        /// <summary>
        /// Gets or sets the button text top padding.
        /// </summary>
        /// <value>The button text top padding.</value>
        /// <remarks>
        /// (Added in v2.2.0) Used to vertically position the Flash button text. Negative values may be used.
        /// </remarks>
        public int ButtonTextTopPadding { get; set; }

        /// <summary>
        /// Gets or sets the button text left padding.
        /// </summary>
        /// <value>The button text left padding.</value>
        /// <remarks>
        /// (Added in v2.2.0) Used to horizontally position the Flash button text. Negative values may be used.
        /// </remarks>
        public int ButtonTextLeftPadding { get; set; }

        /// <summary>
        /// Gets or sets the button action.
        /// </summary>
        /// <value>The button action.</value>
        /// <remarks>
        /// (Added in v2.2.0) Defines the action taken when the Flash button is clicked. Valid action values can be found in the swfupload.js file under the BUTTON_ACTION object.
        /// </remarks>
        public Action ButtonAction { get; set; }

        /// <summary>
        /// Gets or sets the button disabled.
        /// </summary>
        /// <value>The button disabled.</value>
        /// <remarks>
        /// (Added in v2.2.0) A boolean value that sets whether the Flash button is in the disabled state. When in the disabled state the button will not execute any actions.
        /// </remarks>
        public bool ButtonDisabled { get; set; }

        /// <summary>
        /// Gets or sets the button cursor.
        /// </summary>
        /// <value>The button cursor.</value>
        /// <remarks>
        /// (Added in v2.2.0) Used to define what type of mouse cursor is displayed when hovering over the Flash button.
        /// </remarks>
        public Cursor ButtonCursor { get; set; }

        /// <summary>
        /// Gets or sets the button window mode.
        /// </summary>
        /// <value>The button window mode.</value>
        /// <remarks>
        /// (Added in v2.2.0) Sets the WMODE property of the Flash Movie. Valid values are available in the SWFUpload.WINDOW_MODE constants.
        /// </remarks>
        public WindowMode ButtonWindowMode { get; set; }

        /// <summary>
        /// Gets or sets the swfupload loaded handler.
        /// </summary>
        /// <value>The swfupload loaded handler.</value>
        public string SwfUploadLoadedHandler { get; set; }

        /// <summary>
        /// Gets or sets the file dialog start handler.
        /// </summary>
        /// <value>The file dialog start handler.</value>
        public string FileDialogStartHandler { get; set; }

        /// <summary>
        /// Gets or sets the file queued handler.
        /// </summary>
        /// <value>The file queued handler.</value>
        public string FileQueuedHandler { get; set; }

        /// <summary>
        /// Gets or sets the file queue error handler.
        /// </summary>
        /// <value>The file queue error handler.</value>
        public string FileQueueErrorHandler { get; set; }

        /// <summary>
        /// Gets or sets the file dialog complete handler.
        /// </summary>
        /// <value>The file dialog complete handler.</value>
        public string FileDialogCompleteHandler { get; set; }

        //+
        /// <summary>
        /// Gets or sets the upload start handler.
        /// </summary>
        /// <value>The upload start handler.</value>
        public string UploadStartHandler { get; set; }

        /// <summary>
        /// Gets or sets the upload progress handler.
        /// </summary>
        /// <value>The upload progress handler.</value>
        public string UploadProgressHandler { get; set; }

        /// <summary>
        /// Gets or sets the upload error handler.
        /// </summary>
        /// <value>The upload error handler.</value>
        public string UploadErrorHandler { get; set; }

        /// <summary>
        /// Gets or sets the upload success handler.
        /// </summary>
        /// <value>The upload success handler.</value>
        public string UploadSuccessHandler { get; set; }

        /// <summary>
        /// Gets or sets the upload complete handler.
        /// </summary>
        /// <value>The upload complete handler.</value>
        public string UploadCompleteHandler { get; set; }

        //+
        /// <summary>
        /// Gets or sets the debug handler.
        /// </summary>
        /// <value>The debug handler.</value>
        public string DebugHandler { get; set; }

        //+
        /// <summary>
        /// Gets or sets the custom settings.
        /// </summary>
        /// <value>The custom settings.</value>
        public object CustomSettings { get; set; }
        #endregion
    }
}