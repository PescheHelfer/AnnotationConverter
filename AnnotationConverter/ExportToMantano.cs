using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using AnnotationConverter.Helpers;

namespace AnnotationConverter
{
    internal class ExportToMantano : AbsExport
    {
        private long _documentId;
        internal string DbFile { get; set; }
        private SQLiteConnection _connection;

        internal override void PrepareTarget(string targetFile)
        {
            throw new NotSupportedException(
                "'PrepareTarget(string targetFile)' is not supported by ExportToMantano.\nUse 'PrepareTarget(long bookID)' instead");
        }

        internal override void PrepareTarget(long bookId)
        {
            _documentId = bookId;
            ProvideConnection();
        }

        internal SQLiteConnection ProvideConnection()
        {
            var dataSource = DbFile;
            _connection = new SQLiteConnection();
            _connection.ConnectionString = "Data Source=" + dataSource;
            if (_connection == null) throw new Exception("Could not create a database connection.");
            _connection.Open();
            return _connection;
        }

        internal void VerifyMantanoDb()
        {
            Helpers.Utils.VerifyDb("document", "note", "note_metadata", ProvideConnection);
        }

        internal void BuildTargetBookList(List<Tuple<long, string, string>> liBooksContent) // Book-ID, Book-Name
        {
            liBooksContent.Clear();
            liBooksContent.Add(new Tuple<long, string, string>(-1, "", "--- Please choose ---"));
            using (var connection = ProvideConnection())
            {
                var cmdBooks = new SQLiteCommand(connection)
                {
                    CommandText = "SELECT id, title, file_metadata\r\n"
                                  + "FROM Document\r\n"
                                  + "WHERE title IS NOT NULL\r\n"
                                  + "ORDER BY title"
                };

                using (var reader = cmdBooks.ExecuteReader())
                {
                    string authors;

                    while (reader.Read())
                    {
                        authors = Helpers.Utils.GetAuthors(reader[2].ToString());
                        liBooksContent.Add(new Tuple<long, string, string>((long) reader[0], (string) reader[1], (string) reader[1] + " " + authors));
                    }
                }
                cmdBooks.Dispose();
            }
        }

        internal override void ExportRow(ExportRowParams exportRowParams)
        {
            string record = exportRowParams.StrMarkedText ?? (exportRowParams.StrName ?? "N/A");

            if (exportRowParams.StrMark == null || exportRowParams.StrMarkEnd == null || exportRowParams.Page == null)
            {
                MessageBox.Show(string.Format("Record {0} has been skipped due to empty parameters (StrMark, StrMarkEnd or Page).", record),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var cmd = new SQLiteCommand {Connection = _connection};
            cmd.Parameters.AddWithValue("@title", exportRowParams.StrName); // title
            cmd.Parameters.AddWithValue("@created", Helpers.Utils.ConvertDateTimeToUnix(exportRowParams.AddedDate)); // created
            cmd.Parameters.AddWithValue("@last_access", Helpers.Utils.ConvertDateTimeToUnix(exportRowParams.AddedDate)); // last_access
            cmd.Parameters.AddWithValue("@last_edit", Helpers.Utils.ConvertDateTimeToUnix(exportRowParams.ModifiedDate)); // last_edit
            cmd.Parameters.AddWithValue("@kind", exportRowParams.IsBookmark ? 0 : 1); // kind 1: highlight, 0: bookmark
            switch (exportRowParams.MarkupType) // content_type, 0: w/o comment or bookmark,  1: w/ comment, 2: drawing
            {
                case 1: // bookmark with comment
                    cmd.Parameters.AddWithValue("@content_type", 0);
                    break;
                case 11: // annotation with comment
                    cmd.Parameters.AddWithValue("@content_type", 1);
                    break;
                default: // 0: bookmark w/o comment, 10: annotation w/o comment
                    cmd.Parameters.AddWithValue("@content_type", 0);
                    break;
            }
            cmd.Parameters.AddWithValue("@background_type", 0); // background_type, 0: for text, 1: for drawings
            cmd.Parameters.AddWithValue("@start_pos", exportRowParams.StrMark); // start_pos
            cmd.Parameters.AddWithValue("@end_pos", exportRowParams.StrMarkEnd); // end_pos
            cmd.Parameters.AddWithValue("@text_snippet", exportRowParams.StrMarkedText); // text_snippet
            cmd.Parameters.AddWithValue("@color", exportRowParams.IsBookmark ? (object)DBNull.Value : exportRowParams.Color); // color, -131216 = yellow
            cmd.Parameters.AddWithValue("@highlight_style", exportRowParams.HighlightStyle);
                // highlight_style, 0=highlight, 1=underline, 2=strikethrough, 3=vertical bar in margin
            cmd.Parameters.AddWithValue("@page_num", exportRowParams.Page); // page_num
            cmd.Parameters.AddWithValue("@content",
                exportRowParams.StrName != exportRowParams.StrMarkedText && !exportRowParams.IsBookmark
                    ? exportRowParams.StrName
                    : (object) DBNull.Value); // content
            cmd.Parameters.AddWithValue("@fk_document_id", _documentId); // fk_document_id 

            var outcome = CompareRow(cmd);

            switch (outcome)
            {
                case Utils.CompareOutcomes.Skip:
                    RecordsSkipped++;
                    break;
                case Utils.CompareOutcomes.Insert:
                    InsertRow(cmd, record);
                    break;
                case Utils.CompareOutcomes.UpdateBothPosIdentical:
                    UpdateRowBothIdentical(cmd, record);
                    break;

                case Utils.CompareOutcomes.UpdateStartPosIdentical:
                    UpdateRowStartPosIdentical(cmd, record);
                    break;

                case Utils.CompareOutcomes.UpdateEndPosIdentical:
                    UpdateRowEndPosIdentical(cmd, record);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Utils.CompareOutcomes CompareRow(SQLiteCommand cmd)
        {
            cmd.CommandText = "WITH notes AS (SELECT * FROM note WHERE fk_document_id = @fk_document_id)"
                              + "\r\nSELECT CASE"
                              + "\r\n    WHEN NOT EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos OR end_pos = @end_pos )"
                              + "\r\n     THEN 1  -- no match -> INSERT (taken first because it is the most likely case)"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos = @end_pos AND IFNULL(content,'') = IFNULL(@content,'') AND @kind = kind AND @content_type = content_type )"
                              + "\r\n     THEN 0 -- identical -> SKIP"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos = @end_pos AND (IFNULL(content,'') <> IFNULL(@content,'') OR @kind <> kind OR @content_type <> content_type ) AND last_edit <= @last_edit)"
                              + "\r\n     THEN 2 -- match, content not empty and not older -> UPDATE, both pos identical"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos = @end_pos)"
                              + "\r\n     THEN 0 -- match, content empty or older -> SKIP"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos <> @end_pos AND last_edit >= @last_edit)"
                              + "\r\n     THEN 0 -- partial match, source older -> SKIP"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos <> @start_pos AND end_pos = @end_pos AND last_edit >= @last_edit)"
                              + "\r\n     THEN 0 -- partial match, source older -> SKIP"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos = @end_pos AND last_edit >= @last_edit)"
                              + "\r\n     THEN 0 -- partial match, source older -> SKIP"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos = @start_pos AND end_pos <> @end_pos AND last_edit < @last_edit)"
                              + "\r\n     THEN 3 -- partial match, source newer -> UPDATE, start identical"
                              +
                              "\r\n    WHEN EXISTS ( SELECT 1 FROM notes WHERE start_pos <> @start_pos AND end_pos = @end_pos AND last_edit < @last_edit)"
                              + "\r\n     THEN 4 -- partial match, source newer -> UPDATE, end identical"
                              + "\r\n    ELSE 9  -- unexpected case -> ERROR"
                              + "\r\n  END"
                ;
            return (Utils.CompareOutcomes) (int) (long) cmd.ExecuteScalar();
        }

        private void InsertRow(SQLiteCommand cmd, string record)
        {
            cmd.CommandText = "INSERT INTO note ( title"
                              + ", created"
                              + ", last_access"
                              + ", last_edit"
                              + ", kind"
                              + ", content_type"
                              + ", background_type"
                              + ", start_pos"
                              + ", end_pos"
                              + ", text_snippet"
                              + ", color"
                              + ", highlight_style"
                              + ", page_num"
                              + ", content"
                              + ", fk_document_id)"
                              + " VALUES ( @title"
                              + ", @created"
                              + ", @last_access"
                              + ", @last_edit"
                              + ", @kind"
                              + ", @content_type"
                              + ", @background_type"
                              + ", @start_pos"
                              + ", @end_pos"
                              + ", @text_snippet"
                              + ", @color"
                              + ", @highlight_style"
                              + ", @page_num"
                              + ", @content"
                              + ", @fk_document_id )";

            try
            {
                cmd.ExecuteNonQuery();
                RecordsInserted++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Record {0} has been skipped due to the following error:\r\n{1}.", record, ex.Message),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                RecordErrors++;
                return;
            }
        }

        private void UpdateRowBothIdentical(SQLiteCommand cmd, string record)
        {
            cmd.CommandText = ("UPDATE note"
                               + "\r\nSET created = @created"
                               + "\r\n,    last_access = @last_access"
                               + "\r\n,    last_edit = @last_edit"
                               + "\r\n,    kind = @kind"
                               + "\r\n,    content_type = @content_type"
                               + "\r\n,    background_type = @background_type"
                               + "\r\n,    start_pos = @start_pos"
                               + "\r\n,    end_pos = @end_pos"
                               + "\r\n,    text_snippet = @text_snippet"
                               + "\r\n,    color = @color"
                               + "\r\n,    highlight_style = @highlight_style"
                               + "\r\n,    page_num = @page_num"
                               + "\r\n,    content = @content"
                               +
                               "\r\nWHERE fk_document_id = @fk_document_id AND start_pos = @start_pos AND end_pos = @end_pos AND (IFNULL(content,'') <> IFNULL(@content,'') OR @kind <> kind OR @content_type <> content_type ) AND last_edit <= @last_edit");

            try
            {
                cmd.ExecuteNonQuery();
                RecordsUpdated++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Record {0} has been skipped due to the following error:\r\n{1}.", record, ex.Message),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                RecordErrors++;
                return;
            }
        }

        private void UpdateRowStartPosIdentical(SQLiteCommand cmd, string record)
        {
            cmd.CommandText = ("UPDATE note"
                               + "\r\nSET created = @created"
                               + "\r\n,    last_access = @last_access"
                               + "\r\n,    last_edit = @last_edit"
                               + "\r\n,    kind = @kind"
                               + "\r\n,    content_type = @content_type"
                               + "\r\n,    background_type = @background_type"
                               + "\r\n,    start_pos = @start_pos"
                               + "\r\n,    end_pos = @end_pos"
                               + "\r\n,    text_snippet = @text_snippet"
                               + "\r\n,    color = @color"
                               + "\r\n,    highlight_style = @highlight_style"
                               + "\r\n,    page_num = @page_num"
                               + "\r\n,    content = @content"
                               +
                               "\r\nWHERE start_pos = @start_pos AND end_pos <> @end_pos AND last_edit < @last_edit");

            try
            {
                cmd.ExecuteNonQuery();
                RecordsUpdated++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Record {0} has been skipped due to the following error:\r\n{1}.", record, ex.Message),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                RecordErrors++;
                return;
            }
        }

        private void UpdateRowEndPosIdentical(SQLiteCommand cmd, string record)
        {
            cmd.CommandText = ("UPDATE note"
                               + "\r\nSET created = @created"
                               + "\r\n,    last_access = @last_access"
                               + "\r\n,    last_edit = @last_edit"
                               + "\r\n,    kind = @kind"
                               + "\r\n,    content_type = @content_type"
                               + "\r\n,    background_type = @background_type"
                               + "\r\n,    start_pos = @start_pos"
                               + "\r\n,    end_pos = @end_pos"
                               + "\r\n,    text_snippet = @text_snippet"
                               + "\r\n,    color = @color"
                               + "\r\n,    highlight_style = @highlight_style"
                               + "\r\n,    page_num = @page_num"
                               + "\r\n,    content = @content"
                               +
                               "\r\nWHERE start_pos <> @start_pos AND end_pos = @end_pos AND last_edit < @last_edit");

            try
            {
                cmd.ExecuteNonQuery();
                RecordsUpdated++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Record {0} has been skipped due to the following error:\r\n{1}.", record, ex.Message),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                RecordErrors++;
                return;
            }
        }

        internal override void CloseTarget()
        {
            _connection.Dispose(); // closes the connection and disposes the object (same is also being done by using)
        }
    }
}

/*
public void Create(Book book) {
    SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO Book (Id, Title, Language, PublicationDate, Publisher, Edition, OfficialUrl, Description, EBookFormat) VALUES (?,?,?,?,?,?,?,?)", sql_con);
    insertSQL.Parameters.Add(book.Id);
    insertSQL.Parameters.Add(book.Title);
    insertSQL.Parameters.Add(book.Language);
    insertSQL.Parameters.Add(book.PublicationDate);
    insertSQL.Parameters.Add(book.Publisher);
    insertSQL.Parameters.Add(book.Edition);
    insertSQL.Parameters.Add(book.OfficialUrl);
    insertSQL.Parameters.Add(book.Description);
    insertSQL.Parameters.Add(book.EBookFormat);
    try {
        insertSQL.ExecuteNonQuery();
    }
    catch (Exception ex) {
        throw new Exception(ex.Message);
    }    
}
 * book ID 35: Secrets of the Big Data Revolution
 * 
 * WITH CONTENT
INSERT INTO note (title, created, last_access, last_edit, kind, content_type, background_type, start_pos, end_pos, text_snippet, color, page_num, content, fk_document_id)
VALUES ('process typically starts with data warehousing'
	,	1370968559303
	,	1370968559303
	,	1370968559303
	,	1
	,	1
	,	0
	,	'text/part0008.html#point(/1/4/8/1:286)'
	,	'text/part0008.html#point(/1/4/8/1:332)'
	,	'process typically starts with data warehousing'
	,	-131216
	,	21+1
	,	'Test Content'
	,	35)
 * 
 * NO CONTENT
 INSERT INTO note (title, created, last_access, last_edit, kind, content_type, background_type, start_pos, end_pos, text_snippet, color, page_num, content, fk_document_id)
VALUES ('process typically starts with data warehousing'
	,	1370968559303
	,	1370968559303
	,	1370968559303
	,	1
	,	0
	,	0
	,	'text/part0008.html#point(/1/4/8/1:286)'
	,	'text/part0008.html#point(/1/4/8/1:332)'
	,	'process typically starts with data warehousing'
	,	-131216
	,	21+1
	,	NULL
	,	35)
	
 * 
*/