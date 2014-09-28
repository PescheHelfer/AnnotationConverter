using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnotationConverter
{
    abstract class AbsExport
    {
        abstract internal void PrepareDocument(string targetFile);
        abstract internal void ExportRow(long iD, int markupType, string strMark, string strMarkEnd, string strName, string strMarkedText, string page, DateTime addedDate);
        abstract internal void CloseDocument();
    }
}
