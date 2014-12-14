using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnotationConverter
{
    abstract class AbsExport
    {
        internal int RecordsSkipped { get; set; }
        internal int RecordsInserted { get; set; }
        internal int RecordsUpdated { get; set; }
        internal int RecordErrors { get; set; } 

        /// <summary>
        /// Target is an individual file (e.g. each books stores the annotations in its own xml)
        /// </summary>
        /// <param name="targetFile">path including filename</param>
        abstract internal void PrepareTarget(string targetFile);
        
        /// <summary>
        /// Target resides in a DB
        /// </summary>
        /// <param name="bookId">ID of the book within the DB</param>
        abstract internal void PrepareTarget(long bookId);
        
        abstract internal void ExportRow(ExportRowParams exportRowParams);
        
        abstract internal void CloseTarget();
    }
}
