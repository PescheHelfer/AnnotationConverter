using System;

namespace AnnotationConverter
{
    internal class ExportRowParams
    {
        public long ID { get; set; }

        public int MarkupType { get; set; }

        public string StrMark { get; set; }

        public string StrMarkEnd { get; set; }

        public string StrName { get; set; }

        public string StrMarkedText { get; set; }

        public int Color { get; set; }

        public int HighlightStyle { get; set; }

        public string Page { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsBookmark { get; set; }
    }
}