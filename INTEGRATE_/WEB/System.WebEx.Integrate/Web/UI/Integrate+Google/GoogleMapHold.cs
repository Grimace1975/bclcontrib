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
//namespace Instinct.ApplicationUnit_.WebApplicationControl
//{
//    //- GoogleMap -//
//    //+ [GoogleApi]http://www.google.com/apis/maps/documentation/
//    //+ [Geocoder]http://geocoder.us/help/
//    public class GoogleMap : System.Web.UI.WebControls.WebControl
//    {
//        private static System.Type s_type = typeof(GoogleMap);
//        private System.Collections.Generic.Dictionary<int, GoogleMapPoint> m_pointHash = new System.Collections.Generic.Dictionary<int, GoogleMapPoint>();
//        private System.Collections.Generic.Dictionary<int, GoogleMapTextOverlay> m_textOverlayHash = new System.Collections.Generic.Dictionary<int, GoogleMapTextOverlay>();
//        private string m_keyId = string.Empty;
//        private int m_zoomOffset = 0;
//        private GoogleMapControlType m_controlType = GoogleMapControlType.Small;
//        private GoogleMapPointType m_pointType = GoogleMapPointType.Simple;
//        private bool m_isAutoSelect = true;
//        private string m_iconRootUri = "/";

//        //- GoogleMapControlType -//
//        public enum GoogleMapControlType
//        {
//            Large,
//            LargePlus,
//            Small,
//            SmallPlus
//        }

//        //- GoogleMapPointType -//
//        public enum GoogleMapPointType
//        {
//            Simple,
//            Direction
//        }

//        //- Main -//
//        public GoogleMap()
//        {
//            Width = new System.Web.UI.WebControls.Unit("560px");
//            Height = new System.Web.UI.WebControls.Unit("400px");
//        }
//        public GoogleMap(string width, string height)
//        {
//            Width = new System.Web.UI.WebControls.Unit(width);
//            Height = new System.Web.UI.WebControls.Unit(height);
//        }

//        //- AddPoint -//
//        public void AddPoint(GoogleMapPoint point)
//        {
//            m_pointHash.Add(m_pointHash.Count, point);
//        }

//        //- AddTextOverlay -//
//        public void AddTextOverlay(GoogleMapTextOverlay textOverlay)
//        {
//            m_textOverlayHash.Add(m_textOverlayHash.Count, textOverlay);
//        }

//        //- AppendMapToStream -//
//        private void AppendMapToStream(System.Text.StringBuilder textStream)
//        {
//            textStream.AppendLine("   var map = new GMap2(document.getElementById(\"" + ClientID + "\"));");
//            switch (m_controlType)
//            {
//                case GoogleMapControlType.Large:
//                    textStream.AppendLine("   map.addControl(new GLargeMapControl());");
//                    break;
//                case GoogleMapControlType.LargePlus:
//                    textStream.AppendLine("   map.addControl(new GLargeMapControl()); map.addControl(new GMapTypeControl());");
//                    break;
//                case GoogleMapControlType.Small:
//                    textStream.AppendLine("   map.addControl(new GSmallMapControl());");
//                    break;
//                case GoogleMapControlType.SmallPlus:
//                    textStream.AppendLine("   map.addControl(new GSmallMapControl()); map.addControl(new GMapTypeControl());");
//                    break;
//            }
//        }

//        //- ControlType -//
//        public GoogleMapControlType ControlType
//        {
//            get
//            {
//                return m_controlType;
//            }
//            set
//            {
//                m_controlType = value;
//            }
//        }

//        //- ZoomOffset -//
//        public int ZoomOffset
//        {
//            get
//            {
//                return m_zoomOffset;
//            }
//            set
//            {
//                m_zoomOffset = value;
//            }
//        }

//        //- KeyId -//
//        public string KeyId
//        {
//            get
//            {
//                return m_keyId;
//            }
//            set
//            {
//                m_keyId = value;
//            }
//        }

//        //- PointType -//
//        public GoogleMapPointType PointType
//        {
//            get
//            {
//                return m_pointType;
//            }
//            set
//            {
//                m_pointType = value;
//            }
//        }

//        //- IconRootUri -//
//        public string IconRootUri
//        {
//            get
//            {
//                return m_iconRootUri;
//            }
//            set
//            {
//                m_iconRootUri = value;
//            }
//        }

//        //- IsAutoSelect -//
//        public bool IsAutoSelect
//        {
//            get
//            {
//                return m_isAutoSelect;
//            }
//            set
//            {
//                m_isAutoSelect = value;
//            }
//        }

//        //- OnPreRender -//
//        protected override void OnPreRender(System.EventArgs e)
//        {
//            base.OnPreRender(e);
//            WebApplicationFactory.RegisterScript(Page, WebApplicationFactory.RegisterScriptFlag.Kernel);
//            Page.ClientScript.RegisterClientScriptInclude(s_type, "Google", "http://maps.google.com/maps?file=api&v=2&key=" + m_keyId);
//            if (m_textOverlayHash.Count > 0)
//            {
//                Page.ClientScript.RegisterClientScriptBlock(s_type, "GoogleMapTextOverlay", @"
//<script type=""text/javascript"">
////<![CDATA[
//function GoogleMapTextOverlay(html, point, width, height, borderStyle, moveByX, moveByY, textAlign, verticalAlign) {
//	this.html_ = html;
//	this.point_ = point;
//	this.width_ = width;
//	this.height_ = height;
//	this.borderStyle_ = borderStyle;
//	this.moveByX_ = moveByX;
//	this.moveByY_ = moveByY;
//	this.textAlign_ = textAlign;
//	this.verticalAlign_ = verticalAlign;
//}
//GoogleMapTextOverlay.prototype = new GOverlay();
//
////+ creates the DIV representing this GoogleMapTextOverlay.
//GoogleMapTextOverlay.prototype.initialize = function(map) {
//	// create the div representing our GoogleMapTextOverlay
//	var div = document.createElement('div');
//	div.style.border = this.borderStyle_;
//	div.style.position = 'absolute';
//	div.style.width = this.width_;
//	div.style.height = this.height_;
//	div.style.textAlign = this.textAlign_;
//	div.style.verticalAlign = this.verticalAlign_;
//	div.innerHTML = this.html_;
//	//+ GoogleMapTextOverlay is flat against the map, so we add our selves to the MAP_PANE pane,
//	//+ which is at the same z-index as the map itself (i.e., below the marker shadows)
//	map.getPane(G_MAP_MAP_PANE).appendChild(div);
//	//+
//	this.map_ = map;
//	this.div_ = div;
//}
//
////+ remove the main DIV from the map pane
//GoogleMapTextOverlay.prototype.remove = function() {
//  this.div_.parentNode.removeChild(this.div_);
//}
//
////+ copy data to a new GoogleMapTextOverlay
//GoogleMapTextOverlay.prototype.copy = function() {
//  return new GoogleMapTextOverlay(this.html_, this.point_, this.width_, this.height_, this.borderStyle_, this.moveByX_, this.moveByY_, this.textAlign_, this.verticalAlign_);
//}
//
////+ redraw the GoogleMapTextOverlay based on the current projection and zoom level
//GoogleMapTextOverlay.prototype.redraw = function(force) {
//	// redraw only if the coordinate system has changed
//	if (!force) return;
//	//+ map point to location in div
//	var c1 = this.map_.fromLatLngToDivPixel(this.point_);
//	// position div
//	this.div_.style.left = (c1.x + this.moveByX_) + 'px';
//	this.div_.style.top = (c1.y + this.moveByY_) + 'px';
//}
////]]>
//</script>", false);
//            }
//            switch (m_pointType)
//            {
//                case GoogleMapPointType.Direction:
//                    if (Page.ClientScript.IsClientScriptBlockRegistered(s_type, "Direction") == false)
//                    {
//                        string clientId = ClientID;
//                        System.Text.StringBuilder textBuilder = new System.Text.StringBuilder();
//                        textBuilder.Append(@"<script type=""text/javascript"">
////<![CDATA[
//function GoogleMapTo(key) {
//   var o = _gel('"); textBuilder.Append(clientId); textBuilder.Append(@"');
//   if (o != null) {
//      o.Marker[key].openInfoWindowHtml(o.ToText[key]);
//   }
//}
//function GoogleMapFrom(key) {
//	var o = _gel('"); textBuilder.Append(clientId); textBuilder.Append(@"');
//   if (o != null) {
//      o.Marker[key].openInfoWindowHtml(o.FromText[key]);
//   }
//}
//var g_icon = new GIcon();
//g_icon.shadow = '"); textBuilder.Append(m_iconRootUri); textBuilder.Append(@"IconShadow.png';
//g_icon.iconSize = new GSize(20, 34);
//g_icon.shadowSize = new GSize(37, 34);
//g_icon.iconAnchor = new GPoint(9, 34);
//g_icon.infoWindowAnchor = new GPoint(9, 2);
//g_icon.infoShadowAnchor = new GPoint(18, 25);
//function CreatePoint(o, bound, key, icon, point, address, text) {
//   bound.extend(point);
//   o.ToText[key] = text
//   + '<br />Directions: <b>To here</b> - <a href=""javascript:GoogleMapFrom(' + key + ')"">From here</a>'
//   + '<br />Start address:<form action=""http://maps.google.com/maps"" method=""get"" target=""_blank"" style=""margin: 0;"">'
//   + '<input id=""saddr"" name=""saddr"" type=""text"" size=""25"" maxlength=""60"" value="""" /><br />'
//   + '<input value=""Get Directions"" type=""submit"">'
//   + '<input type=""hidden"" name=""daddr"" value=""' + address + '""/></form>';
//   o.FromText[key] = text
//   + '<br />Directions: <a href=""javascript:GoogleMapTo(' + key + ')"">To here</a> - <b>From here</b>'
//   + '<br />End address:<form action=""http://maps.google.com/maps"" method=""get"" target=""_blank"" style=""margin: 0;"">'
//   + '<input id=""daddr"" name=""daddr"" type=""text"" size=""25"" maxlength=""60"" value="""" /><br />'
//   + '<input value=""Get Directions"" type=""submit"">'
//   + '<input type=""hidden"" name=""saddr"" value=""' + address + '""/></form>';
//   text = text.replace(/\[\:Direction\:\]/, 'Directions: <a href=""javascript:GoogleMapTo(' + key + ')"">To here</a> - <a href=""javascript:GoogleMapFrom(' + key + ')"">From here</a>');
//   if (icon != '') {
//      var icon2 = new GIcon(g_icon);
//      icon2.image = '"); textBuilder.Append(m_iconRootUri); textBuilder.Append(@"' + icon + '.png';
//      var marker = new GMarker(point, icon2);
//   } else {
//      var marker = new GMarker(point);
//   }
//   GEvent.addListener(marker, 'click', function() {
//      marker.openInfoWindowHtml(text);
//   });
//   o.Marker[key] = marker;
//   o.Text[key] = text;
//   return marker;
//}
////]]>
//</script>");
//                        Page.ClientScript.RegisterClientScriptBlock(s_type, "Direction", textBuilder.ToString(), false);
//                    }
//                    break;
//            }
//        }

//        //- Render -//
//        protected override void Render(System.Web.UI.HtmlTextWriter writer)
//        {
//            HtmlBuilder z = HtmlBuilder.GetBuilder(writer);
//            //+
//            string clientId = ClientID;
//            System.Text.StringBuilder textBuilder;
//            switch (m_pointType)
//            {
//                case GoogleMapPointType.Simple:
//                    if (m_pointHash.Count < 1)
//                    {
//                        return;
//                    }
//                    //+ startup script
//                    textBuilder = new System.Text.StringBuilder();
//                    textBuilder.AppendLine("if (GBrowserIsCompatible() != null) {");
//                    AppendMapToStream(textBuilder);
//                    GoogleMapPoint point = m_pointHash[0];
//                    //+ 37.4419, -122.1419, 13
//                    textBuilder.AppendFormat("   map.setCenter(new GLatLng({0}, {1}), {2});\r\n", point.Latitude, point.Longitude, point.Zoom); point = null;
//                    //+ text overlay
//                    foreach (GoogleMapTextOverlay textOverlay in m_textOverlayHash.Values)
//                    {
//                        textBuilder.AppendFormat("   map.addOverlay(new GoogleMapTextOverlay('{0}', new GLatLng({1}, {2}), '{3}', '{4}', '{5}', {6}, {7}, '{8}', '{9}'));\r\n", textOverlay.Html, textOverlay.Latitude, textOverlay.Longitude, textOverlay.Width, textOverlay.Height, textOverlay.BorderStyle, textOverlay.MoveByX, textOverlay.MoveByY, textOverlay.TextAlign, textOverlay.VerticalAlign);
//                    }
//                    textBuilder.AppendLine("}");
//                    Page.ClientScript.RegisterStartupScript(s_type, clientId, textBuilder.ToString(), true);
//                    break;
//                case GoogleMapPointType.Direction:
//                    if (m_pointHash.Count < 1)
//                    {
//                        return;
//                    }
//                    //+ startup script
//                    textBuilder = new System.Text.StringBuilder();
//                    textBuilder.Append(@"<script type=""text/javascript"">
////<![CDATA[
//if (window.Kernel) {
//   Kernel.AddEvent(window, 'load', function() {
//var o = _gel('"); textBuilder.Append(clientId); textBuilder.AppendLine(@"');
//if ((o != null) && (GBrowserIsCompatible() != null)) {
//   var key = 0;
//   o.Marker = [];
//   o.Text = [];
//   o.ToText = [];
//   o.FromText = [];");
//                    AppendMapToStream(textBuilder);
//                    textBuilder.AppendLine("   var bound = new GLatLngBounds();");
//                    foreach (GoogleMapPoint point2 in m_pointHash.Values)
//                    {
//                        string address = KernelText.Axb(point2.Street, " ", point2.Street2) + ", " + KernelText.Axb(KernelText.Axb(point2.City, " ", point2.State), " ", point2.Zip);
//                        textBuilder.AppendFormat("   CreatePoint(o, bound, key++, '{0}', new GLatLng({1}, {2}), unescape('{3}'), unescape('{4}'));\r\n", point2.Icon, point2.Latitude, point2.Longitude, Http.Escape(address), Http.Escape(point2.Text));
//                    }
//                    textBuilder.Append(@"
//   var centerLat = (bound.getNorthEast().lat() + bound.getSouthWest().lat()) / 2;
//   var centerLng = (bound.getNorthEast().lng() + bound.getSouthWest().lng()) / 2;
//   var zoomLevel = map.getBoundsZoomLevel(bound);
//   map.setCenter(new GLatLng(centerLat, centerLng), zoomLevel - "); textBuilder.Append(m_zoomOffset); textBuilder.Append(@");
//   while (key > 0) {
//      map.addOverlay(o.Marker[--key]);
//   };");
//                    if ((m_isAutoSelect == true) && (m_pointHash.Count == 1))
//                    {
//                        textBuilder.AppendLine("   o.Marker[0].openInfoWindowHtml(o.Text[0]);");
//                    }
//                    //+ text overlay
//                    foreach (GoogleMapTextOverlay textOverlay in m_textOverlayHash.Values)
//                    {
//                        textBuilder.AppendFormat("   map.addOverlay(new GoogleMapTextOverlay('{0}', new GLatLng({1}, {2}), '{3}', '{4}', '{5}', {6}, {7}, '{8}', '{9}'));\r\n", textOverlay.Html, textOverlay.Latitude, textOverlay.Longitude, textOverlay.Width, textOverlay.Height, textOverlay.BorderStyle, textOverlay.MoveByX, textOverlay.MoveByY, textOverlay.TextAlign, textOverlay.VerticalAlign);
//                    }
//                    textBuilder.Append(@"
//}
//  });
//}
////]]>
//</script>");
//                    Page.ClientScript.RegisterStartupScript(s_type, clientId, textBuilder.ToString(), false);
//                    break;
//            }
//            //+ render
//            z.AddHtmlAttrib(HtmlAttrib.Id, clientId);
//            z.AddHtmlAttrib(HtmlAttrib.StyleWidth, Width.ToString());
//            z.AddHtmlAttrib(HtmlAttrib.StyleHeight, Height.ToString());
//            z.EmptyHtmlTag(HtmlTag.Div);
//        }

//        //- POINT -//
//        #region POINT
//        //- GoogleMapPoint -//
//        public class GoogleMapPoint
//        {
//            private string m_icon = string.Empty;
//            private string m_street = string.Empty;
//            private string m_street2 = string.Empty;
//            private string m_city = string.Empty;
//            private string m_state = string.Empty;
//            private string m_zip = string.Empty;
//            private string m_text = string.Empty;
//            private decimal m_latitude = -1;
//            private decimal m_longitude = -1;
//            private int m_zoom = -1;

//            //- Main -//
//            public GoogleMapPoint()
//            {
//            }
//            public GoogleMapPoint(TableBase table, string fieldPrefix)
//            {
//                m_street = table.GetText(fieldPrefix + "Street");
//                m_street2 = table.GetText(fieldPrefix + "Street2");
//                m_city = table.GetText(fieldPrefix + "City");
//                m_state = table.GetText(fieldPrefix + "State");
//                m_zip = table.GetText(fieldPrefix + "Zip");
//                SetCoordinate(table[fieldPrefix + "Latitude"], table[fieldPrefix + "Longitude"]);
//            }

//            //- Icon -//
//            public string Icon
//            {
//                get
//                {
//                    return m_icon;
//                }
//                set
//                {
//                    m_icon = value;
//                }
//            }

//            //- Street -//
//            public string Street
//            {
//                get
//                {
//                    return m_street;
//                }
//                set
//                {
//                    m_street = value;
//                }
//            }

//            //- Street2 -//
//            public string Street2
//            {
//                get
//                {
//                    return m_street2;
//                }
//                set
//                {
//                    m_street2 = value;
//                }
//            }

//            //- City -//
//            public string City
//            {
//                get
//                {
//                    return m_city;
//                }
//                set
//                {
//                    m_city = value;
//                }
//            }

//            //- State -//
//            public string State
//            {
//                get
//                {
//                    return m_state;
//                }
//                set
//                {
//                    m_state = value;
//                }
//            }

//            //- Zip -//
//            public string Zip
//            {
//                get
//                {
//                    return m_zip;
//                }
//                set
//                {
//                    m_zip = value;
//                }
//            }

//            //- Text -//
//            public string Text
//            {
//                get
//                {
//                    return m_text;
//                }
//                set
//                {
//                    m_text = value;
//                }
//            }

//            //- Latitude -//
//            public decimal Latitude
//            {
//                get
//                {
//                    return m_latitude;
//                }
//                set
//                {
//                    m_latitude = value;
//                }
//            }

//            //- Longitude -//
//            public decimal Longitude
//            {
//                get
//                {
//                    return m_longitude;
//                }
//                set
//                {
//                    m_longitude = value;
//                }
//            }

//            //- Zoom -//
//            public int Zoom
//            {
//                get
//                {
//                    return m_zoom;
//                }
//                set
//                {
//                    m_zoom = value;
//                }
//            }

//            //- SetCoordinate -//
//            public void SetCoordinate(object latitude, object longitude)
//            {
//                string textLatitude;
//                m_latitude = -1;
//                if (latitude != null)
//                {
//                    if (latitude is decimal)
//                    {
//                        m_latitude = (decimal)latitude;
//                    }
//                    else if ((textLatitude = (latitude as string)) != null)
//                    {
//                        decimal.TryParse(textLatitude, out m_latitude);
//                    }
//                }
//                string textLongitude;
//                m_longitude = -1;
//                if (longitude != null)
//                {
//                    if (longitude is decimal)
//                    {
//                        m_longitude = (decimal)longitude;
//                    }
//                    else if ((textLongitude = (longitude as string)) != null)
//                    {
//                        decimal.TryParse(textLongitude, out m_longitude);
//                    }
//                }
//            }

//            //- GeocodeAddress -//
//            public bool GeocodeAddress(string googleMapApi)
//            {
//                string url = "http://maps.google.com/maps/geo?q=" + Http.UrlEncode(string.Concat(KernelText.Axb(Street, " ", Street2), ", ", KernelText.Axb(KernelText.Axb(City, " ", State), " ", Zip))) + "&output=csv&key=" + googleMapApi;
//                System.Net.HttpWebRequest httpRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
//                System.Net.HttpWebResponse httpResponse = null;
//                try
//                {
//                    httpResponse = (System.Net.HttpWebResponse)httpRequest.GetResponse();
//                }
//                catch (System.Exception)
//                {
//                    return false;
//                }
//                string[] responseArray;
//                try
//                {
//                    responseArray = KernelIo.ReadTextStream(httpResponse.GetResponseStream()).Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
//                }
//                catch (System.Exception e)
//                {
//                    KernelEvent.LogEvent(LogEventType.Warning, "GoogleMap::GeocodeAddress", e);
//                    return false;
//                }
//                finally
//                {
//                    httpResponse.Close();
//                }
//                responseArray = responseArray[responseArray.GetUpperBound(0)].Split(',');
//                if (Kernel.ParseInt32(responseArray[0], 0) != 200)
//                {
//                    return false;
//                }
//                if (responseArray.GetUpperBound(0) == 3)
//                {
//                    SetCoordinate(responseArray[2], responseArray[3]);
//                    return true;
//                }
//                return false;
//            }
//        }
//        #endregion POINT

//        //- TEXTOVERLAY -//
//        #region TEXTOVERLAY
//        //- GoogleMapTextOverlay -//
//        public class GoogleMapTextOverlay
//        {
//            private string m_borderStyle = string.Empty;
//            private string m_html = string.Empty;
//            private decimal m_latitude = -1;
//            private decimal m_longitude = -1;
//            private System.Web.UI.WebControls.Unit m_width = -1;
//            private System.Web.UI.WebControls.Unit m_height = -1;
//            private int m_moveByX;
//            private int m_moveByY;
//            private string m_textAlign = string.Empty;
//            private string m_verticalAlign = string.Empty;

//            //- Main -//
//            public GoogleMapTextOverlay()
//            {
//            }
//            public GoogleMapTextOverlay(string html, decimal latitude, decimal longitude, System.Web.UI.WebControls.Unit width, System.Web.UI.WebControls.Unit height)
//                : this(html, latitude, longitude, width, height, 0, 0, "left", "top")
//            {
//            }
//            public GoogleMapTextOverlay(string html, decimal latitude, decimal longitude, string width, string height)
//                : this(html, latitude, longitude, new System.Web.UI.WebControls.Unit(width), new System.Web.UI.WebControls.Unit(height), 0, 0, "left", "top")
//            {
//            }
//            public GoogleMapTextOverlay(string html, decimal latitude, decimal longitude, string width, string height, int moveByX, int moveByY, string textAlign, string verticalAlign)
//                : this(html, latitude, longitude, new System.Web.UI.WebControls.Unit(width), new System.Web.UI.WebControls.Unit(height), moveByX, moveByY, textAlign, verticalAlign)
//            {
//            }
//            public GoogleMapTextOverlay(string html, decimal latitude, decimal longitude, System.Web.UI.WebControls.Unit width, System.Web.UI.WebControls.Unit height, int moveByX, int moveByY, string textAlign, string verticalAlign)
//            {
//                m_html = html;
//                m_latitude = latitude;
//                m_longitude = longitude;
//                m_width = width;
//                m_height = height;
//                m_moveByX = moveByX;
//                m_moveByY = moveByY;
//                m_textAlign = textAlign;
//                m_verticalAlign = verticalAlign;
//            }

//            //- BorderStyle -//
//            public string BorderStyle
//            {
//                get
//                {
//                    return m_borderStyle;
//                }
//                set
//                {
//                    m_borderStyle = value;
//                }
//            }

//            //- Height -//
//            public System.Web.UI.WebControls.Unit Height
//            {
//                get
//                {
//                    return m_height;
//                }
//                set
//                {
//                    m_height = value;
//                }
//            }

//            //- Html -//
//            public string Html
//            {
//                get
//                {
//                    return m_html;
//                }
//                set
//                {
//                    m_html = value;
//                }
//            }

//            //- Latitude -//
//            public decimal Latitude
//            {
//                get
//                {
//                    return m_latitude;
//                }
//                set
//                {
//                    m_latitude = value;
//                }
//            }

//            //- Longitude -//
//            public decimal Longitude
//            {
//                get
//                {
//                    return m_longitude;
//                }
//                set
//                {
//                    m_longitude = value;
//                }
//            }

//            //- MoveByX -//
//            public int MoveByX
//            {
//                get
//                {
//                    return m_moveByX;
//                }
//                set
//                {
//                    m_moveByX = value;
//                }
//            }

//            //- MoveByY -//
//            public int MoveByY
//            {
//                get
//                {
//                    return m_moveByY;
//                }
//                set
//                {
//                    m_moveByY = value;
//                }
//            }

//            //- TextAlign -//
//            public string TextAlign
//            {
//                get
//                {
//                    return m_textAlign;
//                }
//                set
//                {
//                    m_textAlign = value;
//                }
//            }

//            //- VerticalAlign -//
//            public string VerticalAlign
//            {
//                get
//                {
//                    return m_verticalAlign;
//                }
//                set
//                {
//                    m_verticalAlign = value;
//                }
//            }

//            //- Width -//
//            public System.Web.UI.WebControls.Unit Width
//            {
//                get
//                {
//                    return m_width;
//                }
//                set
//                {
//                    m_width = value;
//                }
//            }
//        }
//        #endregion TEXTOVERLAY
//    }
//}
