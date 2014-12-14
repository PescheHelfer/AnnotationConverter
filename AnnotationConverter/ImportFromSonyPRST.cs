using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.ComponentModel; // for ProgressChangedEventArgs
using System.IO;

namespace AnnotationConverter
{
    internal class ImportFromSonyPRST : AbsImport
    {
        [System.ComponentModel.DefaultValue(false)] // only way pre C# 6 to set a default value of an auto property
        public bool HasOutsourcedAnnotations { get; set; }

        private string _contentFilter;

        private Byte[] _blobMark = new Byte[0];
        // empty byte array. Will be filled when converting the blob to Byte[], including correct number of elements

        private Byte[] _blobMarkEnd = new Byte[0];

        internal ImportFromSonyPRST(AbsExport absExport) // constructor 
            : base(absExport)
        {
        } // just execute the constructor as defined in the abstract class (w/o this, the subclass would lack a constructor)

        protected override object ProvideConnection()
        {
            var connection = new SQLiteConnection();
            connection.ConnectionString = "Data Source=" + DataSource;
            if (connection == null) throw new Exception("Could not create a database connection.");
            connection.Open();
            return connection;
        }

        internal void VerifySonyPRSTDb()
        {
            Helpers.Utils.VerifyDb("bookmark", "annotation", "books", ProvideConnection);
        }

        internal override void BuildBookList(List<Tuple<long, string, string, string>> liBooksContent)
        {
            liBooksContent.Clear();
            liBooksContent.Add(new Tuple<long, string, string, string>(-1, "", "--- Please choose ---", ""));
            using (var connection = (SQLiteConnection) ProvideConnection())
            {
                var cmdBooks = new SQLiteCommand(connection);
                cmdBooks.CommandText = "SELECT A._id AS content_id, A.title, A.author, A.file_name FROM books A\r\n"
                                       + "WHERE mime_type = 'application/epub+zip'\r\n"
                                       + "AND (   EXISTS ( SELECT 1 FROM annotation B WHERE B.content_id = A._id)\r\n"
                                       + "    OR  EXISTS ( SELECT 1 FROM bookmark B WHERE B.content_id = A._id)   )";

                using (var reader = cmdBooks.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //  Console.WriteLine("{0}\t\t{1}\t\t{2}", (long)_reader[0], (string)_reader[1], (string)_reader[2]);
                        liBooksContent.Add(new Tuple<long, string, string, string>((long) reader[0], (string) reader[1],
                            (string) reader[1] + " [" + (string) reader[2] + "]", (string) reader[3]));
                        // System.Windows.Forms.MessageBox.Show(String.Format ("{0}\t\t{1}\t\t{2}", (long)_reader[0], (string)_reader[1], (string)_reader[2]));
                    }
                }
                cmdBooks.Dispose();
            }
        }

        internal override void GetTotalCountOfAnnotations()
        {
            using (var connection = (SQLiteConnection) ProvideConnection())
            {
                var cmdCount = new SQLiteCommand(connection);
                _contentFilter = HasOutsourcedAnnotations
                    ? " IN (" + ContentID.ToString() + ", " + (ContentID + 100000).ToString() + ")"
                    : " = " + ContentID.ToString();
                cmdCount.CommandText = "SELECT COUNT(*) AS number FROM annotation WHERE content_id " + _contentFilter;
                TotalCount = (int) (long) cmdCount.ExecuteScalar();
                cmdCount.CommandText = "SELECT COUNT(*) AS number FROM bookmark WHERE content_id " + _contentFilter;
                TotalCount += (int) (long) cmdCount.ExecuteScalar();
                cmdCount.Dispose();
            }
        }

        /// <summary>
        /// Connects to the DB and loops through each row in the table 'annotation' that matches contentID
        /// </summary>
        /// <param name="contentID">ID of the book</param>
        internal override void ReadAnnotations()
        {
            using (var connection = (SQLiteConnection) ProvideConnection())
            {
                var cmdAnnotations = new SQLiteCommand(connection);
                //_contentFilter = HasOutsourcedAnnotations ? " IN (" + ContentID.ToString() + ", " + (ContentID + 100000).ToString() + ")" : " = " + ContentID.ToString();
                cmdAnnotations.CommandText =
                    "SELECT _id, markup_type, added_date, modified_date, name, marked_text, mark, mark_end, page FROM annotation WHERE content_id " +
                    _contentFilter; // +" LIMIT 10";

                using (var reader = cmdAnnotations.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConsumeAnnotationRow(reader, false);
                        CountAnnotations++;
                        var e = new ProgressChangedEventArgs(100*(CountAnnotations + CountBookmarks)/TotalCount, null);
                        OnProgressChanged(e);
                    }
                }
                cmdAnnotations.Dispose();
            }
        }

        /// <summary>
        /// Connects to the DB and loops through each row in the table 'bookmark' that matches contentID
        /// </summary>
        internal override void ReadBookmarks()
        {
            using (var connection = (SQLiteConnection) ProvideConnection())
            {
                var cmdBookmarks = new SQLiteCommand(connection);

                cmdBookmarks.CommandText =
                    "SELECT _id, markup_type, added_date, modified_date, name, marked_text, mark, mark_end, page FROM bookmark WHERE content_id " +
                    _contentFilter;

                using (var reader = cmdBookmarks.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConsumeAnnotationRow(reader, true);
                        CountBookmarks++;
                        var e = new ProgressChangedEventArgs(100*(CountAnnotations + CountBookmarks)/TotalCount, null);
                        OnProgressChanged(e);
                    }
                }

                cmdBookmarks.Dispose();
            }
        }

        protected override void ConsumeAnnotationRow(System.Data.SQLite.SQLiteDataReader row, bool isBookmark)
        {
            if (row == null)
            {
                return;
            }

            var expParams = new ExportRowParams();

            expParams.ID = (long) row[0];
            expParams.MarkupType = (int) (long) row[1];
            expParams.StrName = (string) row[4];
            expParams.StrMarkedText = (string) row[5];

            _blobMark = (Byte[]) (row[6]); // mark,     BLOB (Byte array)
            if (row[7] == DBNull.Value)
            {
                _blobMarkEnd = (Byte[]) (row[6]);
                // especially bookmarks of markup type 1 sometimes have no blobMarkEnd --> workarround should at least work for ADE, which uses identical start and end coordinates for bookmarks
            }
            else
            {
                _blobMarkEnd = (Byte[]) (row[7]); // mark_end, BLOB
            }

            expParams.StrMark = System.Text.Encoding.UTF8.GetString(_blobMark,0, _blobMark.Length -1); // decodes all the bytes in the specified byte array into a string. The last one is skipped as it is some unknown sign causing Mantano to fail reading the note.
            expParams.StrMarkEnd = System.Text.Encoding.UTF8.GetString(_blobMarkEnd, 0, _blobMarkEnd.Length-1  );
            // round digits >= 0.25 up to the next integer (unclear where the threshold for Digital editions really is. It is between .0074 and .309
            expParams.Color = Color;
            expParams.HighlightStyle = HighlightStyle;
            
            expParams.Page = ((int) (((double) row[8])%1 >= .25 ? Math.Floor((double) row[8]) + 1F : Math.Floor((double) row[8]))).ToString();
                // why does it have to be string?
            expParams.AddedDate = Helpers.Utils.ConvertUnixToDateTime((long) row[2]);
            expParams.ModifiedDate = Helpers.Utils.ConvertUnixToDateTime((long) (row[3] == DBNull.Value ? row[2] : row[3]));

            _absExport.ExportRow(expParams);
        }
    }
}