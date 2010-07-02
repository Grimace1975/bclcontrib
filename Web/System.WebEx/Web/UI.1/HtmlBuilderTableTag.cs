namespace System.Web.UI
{
    /// <summary>
    /// HtmlBuilderTableTag
    /// </summary>
    public class HtmlBuilderTableTag
    {
        public class TableAttrib
        {
            public int? ColumnCount { get; set; }
            public string NullTdBody { get; set; }
            public int? RowPitch { get; set; }
            public int? ColumnPitch { get; set; }
            public string SelectedClass { get; set; }
            public string SelectedStyle { get; set; }
            public string AlternateClass { get; set; }
            public string AlternateStyle { get; set; }
            public TableTrCloseMethod? TrCloseMethod { get; set; }
            public TableAlternateOrientation? AlternateOrientation { get; set; }
        }

        public enum TableStage
        {
            Undefined,
            Colgroup,
            TheadTfoot,
            Tbody,
        }

        public enum TableTrCloseMethod
        {
            Undefined,
            Td,
            TdColspan
        }

        public enum TableAlternateOrientation
        {
            Column,
            Row
        }

        public HtmlBuilderTableTag(HtmlBuilder b, TableAttrib attrib)
        {
            if (attrib != null)
            {
                int? columnCount = attrib.ColumnCount;
                ColumnCount = (columnCount != null ? columnCount.Value : 0);
                NullTdBody = attrib.NullTdBody;
                int? rowPitch = attrib.RowPitch;
                RowPitch = (rowPitch != null ? rowPitch.Value : 1);
                int? columnPitch = attrib.ColumnPitch;
                ColumnPitch = (columnPitch != null ? columnPitch.Value : 1);
                SelectedClass = attrib.SelectedClass;
                SelectedStyle = attrib.SelectedStyle;
                AlternateClass = attrib.AlternateClass;
                AlternateStyle = attrib.AlternateStyle;
                TableTrCloseMethod? trCloseMethod = attrib.TrCloseMethod;
                TrCloseMethod = (trCloseMethod != null ? trCloseMethod.Value : TableTrCloseMethod.Undefined);
                TableAlternateOrientation? alternateOrientation = attrib.AlternateOrientation;
                AlternateOrientation = (alternateOrientation != null ? alternateOrientation.Value : TableAlternateOrientation.Row);
            }
            else
            {
                ColumnCount = 0;
                NullTdBody = string.Empty;
                RowPitch = 1;
                ColumnPitch = 1;
                SelectedClass = string.Empty;
                SelectedStyle = string.Empty;
                AlternateClass = string.Empty;
                AlternateStyle = string.Empty;
                TrCloseMethod = TableTrCloseMethod.Undefined;
                AlternateOrientation = TableAlternateOrientation.Row;
            }
        }

        protected internal virtual void AddHtmlAttrib(HtmlBuilder b, HtmlTextWriterEx w, HtmlTag tag, Nattrib attrib)
        {
            bool isSelected;
            string appendStyle;
            bool isStyleDefined;
            string appendClass;
            bool isClassDefined;
            if (attrib != null)
            {
                isSelected = attrib.Slice<bool>("selected");
                appendStyle = attrib.Slice<string>("appendStyle");
                isStyleDefined = attrib.Exists("style");
                appendClass = attrib.Slice<string>("appendClass");
                isClassDefined = attrib.Exists("class");
                if (tag == HtmlTag.Tr)
                    IsTrHeader = attrib.Slice<bool>("header");
                if (attrib.Count > 0)
                    b.AddHtmlAttrib(attrib, null);
            }
            else
            {
                isSelected = false;
                appendStyle = string.Empty;
                isStyleDefined = false;
                appendClass = string.Empty;
                isClassDefined = false;
            }
            // only apply remaining to td/th
            if ((tag == HtmlTag.Td) || (tag == HtmlTag.Th))
            {
                // style
                if (!isStyleDefined)
                {
                    string effectiveStyle;
                    if ((isSelected) && (!string.IsNullOrEmpty(SelectedStyle)))
                    {
                        effectiveStyle = (appendStyle.Length == 0 ? SelectedStyle : SelectedStyle + " " + appendStyle);
                        w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, effectiveStyle);
                    }
                    else if (!string.IsNullOrEmpty(AlternateStyle))
                    {
                        effectiveStyle = (appendStyle.Length == 0 ? AlternateStyle : AlternateStyle + " " + appendStyle);
                        switch (AlternateOrientation)
                        {
                            case TableAlternateOrientation.Column:
                                if ((((ColumnIndex - ColumnOffset - 1 + ColumnPitch) / ColumnPitch) % 2) == 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, effectiveStyle);
                                else if (appendStyle.Length > 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, appendStyle);
                                break;
                            case TableAlternateOrientation.Row:
                                if ((((RowIndex - RowOffset - 1 + RowPitch) / RowPitch) % 2) == 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, effectiveStyle);
                                else if (appendStyle.Length > 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, appendStyle);
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                    }
                    else if (appendStyle.Length > 0)
                        w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Style, appendStyle);
                }
                // class
                if (!isClassDefined)
                {
                    string effectiveClass;
                    if ((isSelected) && (!string.IsNullOrEmpty(SelectedClass)))
                    {
                        effectiveClass = (appendClass.Length == 0 ? SelectedClass : SelectedClass + " " + appendClass);
                        w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, effectiveClass);
                    }
                    else if (!string.IsNullOrEmpty(AlternateClass))
                    {
                        effectiveClass = (appendClass.Length == 0 ? AlternateClass : AlternateClass + " " + appendClass);
                        switch (AlternateOrientation)
                        {
                            case TableAlternateOrientation.Column:
                                if ((((ColumnIndex - ColumnOffset - 1 + ColumnPitch) / ColumnPitch) % 2) == 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, effectiveClass);
                                else if (appendClass.Length > 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, appendClass);
                                break;
                            case TableAlternateOrientation.Row:
                                if ((((RowIndex - RowOffset - 1 + RowPitch) / RowPitch) % 2) == 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, effectiveClass);
                                else if (appendClass.Length > 0)
                                    w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, appendClass);
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                    }
                    else if (appendClass.Length > 0)
                        w.AddAttributeIfUndefined(HtmlTextWriterAttribute.Class, appendClass);
                }
            }
        }

        public string AlternateClass { get; set; }

        public string AlternateStyle { get; set; }

        public TableAlternateOrientation AlternateOrientation { get; set; }

        public int ColumnCount { get; internal set; }

        public int ColumnIndex { get; internal set; }

        public int ColumnOffset { get; set; }

        public int ColumnPitch { get; set; }

        internal bool IsTrHeader { get; set; }

        public string NullTdBody { get; set; }

        public int RowIndex { get; internal set; }

        public int RowOffset { get; set; }

        public int RowPitch { get; set; }

        public string SelectedClass { get; set; }

        public string SelectedStyle { get; set; }

        internal TableStage Stage { get; set; }

        public object Tag { get; set; }

        public TableTrCloseMethod TrCloseMethod { get; set; }
    }
}