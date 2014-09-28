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

        internal override void PrepareDocument(string targetFile)
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

        /// <summary>
        /// Prepares the data in the row (converts Blob data etc.)
        /// </summary>
        /// <param name="row">row filled by the SQLiteDataReader</param>
        internal override void ExportRow(long iD, int markupType, string strMark, string strMarkEnd, string strName, string strMarkedText, string page, DateTime addedDate)
        {
            //Console.WriteLine(strMark, strMarkEnd);
            // writer.WriteStartElement("annotation");
            _writer.WriteStartElement("annotation", ""); // second parameter leads to empty namespace: xmlns="" (as in the original file)
            if (markupType > 10) // highlight with note -> make it visible
            {
                _writer.WriteAttributeString("isvisible", "true");
            }

            _namespaceUrl = "http://purl.org/dc/elements/1.1/";
            _prefix = _writer.LookupPrefix(_namespaceUrl);
            _writer.WriteElementString(_prefix, "identifier", _namespaceUrl, "urn:uuid:" + iD.ToString()); // the ID is not required by digital editions. Hence, the _id from the Sony reader is used (although it has a different format).
            _writer.WriteElementString(_prefix, "date", _namespaceUrl, addedDate.ToString("s") + "-00:00");
            _writer.WriteElementString(_prefix, "creator", _namespaceUrl, "creator id");
            if (markupType == 1)
            {
                _writer.WriteElementString(_prefix, "title", _namespaceUrl, strName);
            }
            else
            {
                _writer.WriteElementString(_prefix, "title", _namespaceUrl, "Seite " + page + ", " + addedDate.ToString("U") + " UTC");
            }
            _writer.WriteStartElement("target");
            _writer.WriteStartElement("fragment");

            // remove illegal \0 at the end of the string (would cause XML error)
            strMark = strMark.Substring(0, strMark.Length - 1);
            strMarkEnd = strMarkEnd.Substring(0, strMarkEnd.Length - 1);

            _writer.WriteAttributeString("start", strMark);
            if (markupType < 2)
            {
                _writer.WriteAttributeString("end", strMark);
            }
            else
            {
                _writer.WriteAttributeString("end", strMarkEnd);
            }
            //writer.WriteAttributeString("end", strMarkEnd);
            if (markupType < 2)
            {
                _writer.WriteElementString("text", "");
            }
            else
            {
                _writer.WriteElementString("text", strMarkedText);
            }
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.WriteStartElement("content");
            if (markupType > 10)
            {
                _writer.WriteElementString(_prefix, "date", _namespaceUrl, addedDate.ToString("s") + "-00:00");
                _writer.WriteElementString("text", strName);
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

        internal override void CloseDocument()
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
