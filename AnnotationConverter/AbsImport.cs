using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.ComponentModel; // for ProgressChangedEventHandler

namespace AnnotationConverter
{
    abstract class AbsImport
    {
        internal string DataSource { get; set; }
        internal long ContentID { get; set; }
        internal string FileName { get; set; }
        internal int CountAnnotations { get; set; }
        internal int CountBookmarks { get; set; }
        internal int TotalCount { get; set; } // Annotations + Bookmarks

        protected DateTime _addedDate;
        protected long _iD;
        protected int _markupType;
        protected int _page;
        protected string _strMarkStart;
        protected string _strMarkedText;
        protected string _strMarkEnd;
        protected string _strName;
        protected AbsExport _absExport;

        internal AbsImport(AbsExport absExport) // constructor
        {
            _absExport = absExport;
        }

        // event for progress bar

        internal event ProgressChangedEventHandler ProgressChanged;
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            var hand = ProgressChanged;         // Optimisation for multithreading
            if (hand != null)                   // Checks if there are any subscribers, otherweise it would throw an exception
            {
                hand(this, e);                  // Notify Subscribers (raise the event)
            }
        }

        abstract protected object ProvideConnection();

        abstract internal void BuildBookList(List<Tuple<long, string, string>> liBooksContent);

        abstract internal void GetTotalCountOfAnnotations();

        abstract internal void ReadAnnotations();

        abstract internal void ReadBookmarks();

        abstract protected void ConsumeAnnotationRow(SQLiteDataReader row);
        //ToDo: make the row generic, so tha t not only SQLite is supported!
    }
}
