using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.ComponentModel; // for ProgressChangedEventArgs

namespace AnnotationConverter
{
    class ImportFromSonyPRST : AbsImport
    {
        [System.ComponentModel.DefaultValue(false)] // only way pre C# 6 to set a default value of an auto property
        public bool HasOutsourcedAnnotations { get; set; }

        private string _contentFilter;
        private Byte[] _blobMark = new Byte[0]; // empty byte array. Will be filled when converting the blob to Byte[], including correct number of elements
        private Byte[] _blobMarkEnd = new Byte[0];

        internal ImportFromSonyPRST(AbsExport absExport)  // constructor 
            : base(absExport) { } // just execute the constructor as defined in the abstract class (w/o this, the subclass would lack a constructor)

        #region old constructor, no longer used
        /*
        internal ImportFromSonyPRST(AbsExport absExport, long contentID, bool hasOutsourcedAnnotations)  // constructor
            : base(absExport, contentID)
        {
            _contentFilter = hasOutsourcedAnnotations ? " IN (" + contentID.ToString() + ", " + (contentID + 100000).ToString() + ")" : " = " + contentID.ToString();
        }
        // first executes the (absExport, contentID) constructor of the base class, then the more specific constructor of the subclass 
         */
        #endregion

        protected override object ProvideConnection()
        {
            SQLiteConnection connection = new SQLiteConnection();
            connection.ConnectionString = "Data Source=" + DataSource;
            if (connection == null) throw new Exception("Could not create a database connection.");
            connection.Open();
            return connection;

        }

        internal override void BuildBookList(List<Tuple<long, string, string>> liBooksContent)
        {
            using (SQLiteConnection connection = (SQLiteConnection)ProvideConnection())
            {
                SQLiteCommand cmdBooks = new SQLiteCommand(connection);
                cmdBooks.CommandText = "SELECT A._id AS content_id, A.title, A.author, A.file_name FROM books A\r\n"
                                     + "WHERE mime_type = 'application/epub+zip'\r\n"
                                     + "AND (   EXISTS ( SELECT 1 FROM annotation B WHERE B.content_id = A._id)\r\n"
                                     + "    OR  EXISTS ( SELECT 1 FROM bookmark B WHERE B.content_id = A._id)   )";

                using (SQLiteDataReader reader = cmdBooks.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //  Console.WriteLine("{0}\t\t{1}\t\t{2}", (long)_reader[0], (string)_reader[1], (string)_reader[2]);
                        liBooksContent.Add(new Tuple<long, string, string>((long)reader[0], (string)reader[1] + " [" + (string)reader[2] + "]", (string)reader[3]));
                        // System.Windows.Forms.MessageBox.Show(String.Format ("{0}\t\t{1}\t\t{2}", (long)_reader[0], (string)_reader[1], (string)_reader[2]));
                    }
                }
                cmdBooks.Dispose();
            }
        }

        internal override void GetTotalCountOfAnnotations()
        {
            using (SQLiteConnection connection = (SQLiteConnection)ProvideConnection())
            {
                SQLiteCommand cmdCount = new SQLiteCommand(connection);
                _contentFilter = HasOutsourcedAnnotations ? " IN (" + ContentID.ToString() + ", " + (ContentID + 100000).ToString() + ")" : " = " + ContentID.ToString();
                cmdCount.CommandText = "SELECT COUNT(*) AS number FROM annotation WHERE content_id " + _contentFilter;
                TotalCount = (int)(long)cmdCount.ExecuteScalar();
                cmdCount.CommandText = "SELECT COUNT(*) AS number FROM bookmark WHERE content_id " + _contentFilter;
                TotalCount += (int)(long)cmdCount.ExecuteScalar();
                cmdCount.Dispose();
            }
        }

        /// <summary>
        /// Connects to the DB and loops through each row in the table 'annotation' that matches contentID
        /// </summary>
        /// <param name="contentID">ID of the book</param>
        internal override void ReadAnnotations()
        {
            using (SQLiteConnection connection = (SQLiteConnection)ProvideConnection())
            {
                SQLiteCommand cmdAnnotations = new SQLiteCommand(connection);
                //_contentFilter = HasOutsourcedAnnotations ? " IN (" + ContentID.ToString() + ", " + (ContentID + 100000).ToString() + ")" : " = " + ContentID.ToString();
                cmdAnnotations.CommandText = "SELECT _id, markup_type, added_date, name, marked_text, mark, mark_end, page FROM annotation WHERE content_id " + _contentFilter; // +" LIMIT 10";

                using (SQLiteDataReader reader = cmdAnnotations.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConsumeAnnotationRow(reader);
                        CountAnnotations++;
                        var e = new ProgressChangedEventArgs(100*(CountAnnotations + CountBookmarks) / TotalCount, null);
                        OnProgressChanged(e);
                    }
                }
                cmdAnnotations.Dispose();
            }
        }

        /// <summary>
        /// Connects to the DB and loops through each row in the table 'bookmark' that matches contentID
        /// </summary>
        /// <param name="contentID">ID of the book</param>
        internal override void ReadBookmarks()
        {
            using (SQLiteConnection connection = (SQLiteConnection)ProvideConnection())
            {
                SQLiteCommand cmdBookmarks = new SQLiteCommand(connection);

                cmdBookmarks.CommandText = "SELECT _id, markup_type, added_date, name, marked_text, mark, mark_end, page FROM bookmark WHERE content_id " + _contentFilter;

                using (SQLiteDataReader reader = cmdBookmarks.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConsumeAnnotationRow(reader);
                        CountBookmarks++;
                        var e = new ProgressChangedEventArgs(100*(CountAnnotations+CountBookmarks)/TotalCount, null);
                        OnProgressChanged(e);
                    }
                }

                cmdBookmarks.Dispose();
            }
        }

        protected override void ConsumeAnnotationRow(System.Data.SQLite.SQLiteDataReader row)
        {
            //ToDo: Object, Struct oder Tupel für modifizierte row definieren und das Objekt übergeben

            if (row != null)
            {
                _iD = (long)row[0];
                _markupType = (int)(long)row[1];
                _strName = (string)row[3];
                _strMarkedText = (string)row[4];
                _blobMark = (Byte[])(row[5]);     // mark,     BLOB (Byte array)
                if (row[6] == DBNull.Value)
                {
                    _blobMarkEnd = (Byte[])(row[5]); // especially bookmarks of markup type 1 sometimes have no blobMarkEnd --> workarround should at least work for ADE, which uses identical start and end coordinates for bookmarks
                }
                else
                {
                    _blobMarkEnd = (Byte[])(row[6]);  // mark_end, BLOB
                }

                _strMarkStart = System.Text.Encoding.UTF8.GetString(_blobMark); // decodes all the bytes in the specified byte array into a string
                _strMarkEnd = System.Text.Encoding.UTF8.GetString(_blobMarkEnd);
                // round digits >= 0.25 up to the next integer (unclear where the threshold for Digital editions really is. It is between .0074 and .309
                _page = (int)(((double)row[7]) % 1 >= .25 ? Math.Floor((double)row[7]) + 1F : Math.Floor((double)row[7]));
                _addedDate = Utils.Utils.convertUnixToDateTime((long)row[2]);
            }

            //    Console.WriteLine(strMark);
            //     Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), strMark, strMarkEnd);
            _absExport.ExportRow(_iD, _markupType, _strMarkStart, _strMarkEnd, _strName, _strMarkedText, _page.ToString(), _addedDate);
        }
    }
}
