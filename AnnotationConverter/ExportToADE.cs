using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AnnotationConverter
{
    class ExportToADE : AbsExport
    {
        // http://openbook.galileocomputing.de/visual_csharp_2010/visual_csharp_2010_16_005.htm

        private XmlWriter _writer;
        private XmlWriterSettings _settings = new XmlWriterSettings();
        private string _namespaceUrl;
        private string _prefix;

        internal override void PrepareTarget(string targetFile)
        {
            _settings.Indent = true;
            _settings.IndentChars = "  ";          // 2 Leerzeichen
            _settings.OmitXmlDeclaration = true;   // ommit prologue: <?xml version="1.0" encoding="utf-8"?>

            _writer = XmlWriter.Create(targetFile, _settings);
            _writer.WriteStartDocument();
            // Start-Tag des Stammelements

            _writer.WriteStartElement("annotationSet", "http://ns.adobe.com/digitaleditions/annotations"); // in StartElement deklarierter Namespace erscheint immer nach den mit AttributeString definierten NS, also zuletzt
            _writer.WriteAttributeString("xmlns", "xhtml", null, "http://www.w3.org/1999/xhtml");  // Trick, um für das Element weitere Namespaces hinzuzufügen, hier inkl. Präfix xhtml
            _writer.WriteAttributeString("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
        }

        internal override void PrepareTarget(long bookId)
        {
            throw new NotSupportedException(("'PrepareTarget(string dbFile, long bookID)' is not supported by ExportToMantano.\nUse 'PrepareTarget(string targetFile)' instead"));
        }

        /// <summary>
        /// Prepares the data in the row (converts Blob data etc.)
        /// </summary>
        /// <param name="row">row filled by the SQLiteDataReader</param>
        internal override void ExportRow(ExportRowParams exportRowParams)
        {
            //Console.WriteLine(strMark, strMarkEnd);
            // writer.WriteStartElement("annotation");
            _writer.WriteStartElement("annotation", ""); // second parameter leads to empty namespace: xmlns="" (as in the original file)
            if (exportRowParams.MarkupType > 10) // highlight with note -> make it visible
            {
                _writer.WriteAttributeString("isvisible", "true");
            }

            _namespaceUrl = "http://purl.org/dc/elements/1.1/";
            _prefix = _writer.LookupPrefix(_namespaceUrl);
            _writer.WriteElementString(_prefix, "identifier", _namespaceUrl, "urn:uuid:" + exportRowParams.ID.ToString()); // the ID is not required by digital editions. Hence, the _id from the Sony reader is used (although it has a different format).
            _writer.WriteElementString(_prefix, "date", _namespaceUrl, exportRowParams.AddedDate.ToString("s") + "-00:00");
            _writer.WriteElementString(_prefix, "creator", _namespaceUrl, "creator id");
            if (exportRowParams.MarkupType == 1)
            {
                _writer.WriteElementString(_prefix, "title", _namespaceUrl, exportRowParams.StrName);
            }
            else
            {
                _writer.WriteElementString(_prefix, "title", _namespaceUrl, "Seite " + exportRowParams.Page + ", " + exportRowParams.AddedDate.ToString("U") + " UTC");
            }
            _writer.WriteStartElement("target");
            _writer.WriteStartElement("fragment");

            // remove illegal \0 at the end of the string (would cause XML error)
            exportRowParams.StrMark = exportRowParams.StrMark.Substring(0, exportRowParams.StrMark.Length - 1);
            exportRowParams.StrMarkEnd = exportRowParams.StrMarkEnd.Substring(0, exportRowParams.StrMarkEnd.Length - 1);

            _writer.WriteAttributeString("start", exportRowParams.StrMark);
            if (exportRowParams.MarkupType < 2)
            {
                _writer.WriteAttributeString("end", exportRowParams.StrMark);
            }
            else
            {
                _writer.WriteAttributeString("end", exportRowParams.StrMarkEnd);
            }
            //writer.WriteAttributeString("end", strMarkEnd);
            if (exportRowParams.MarkupType < 2)
            {
                _writer.WriteElementString("text", "");
            }
            else
            {
                _writer.WriteElementString("text", exportRowParams.StrMarkedText);
            }
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.WriteStartElement("content");
            if (exportRowParams.MarkupType > 10)
            {
                _writer.WriteElementString(_prefix, "date", _namespaceUrl, exportRowParams.AddedDate.ToString("s") + "-00:00");
                _writer.WriteElementString("text", exportRowParams.StrName);
            }
            else
            {
                _writer.WriteElementString(_prefix, "date", _namespaceUrl, null);
                _writer.WriteStartElement("text");
                _writer.WriteEndElement();

            }
            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        internal override void CloseTarget()
        {
            _writer.WriteStartElement("publication");
            _writer.WriteElementString(_prefix, "identifier", _namespaceUrl, null);
            _writer.WriteElementString(_prefix, "title", _namespaceUrl, null);
            _writer.WriteEndElement();

            _writer.WriteEndDocument();
            _writer.Close();
        }
    }
}
