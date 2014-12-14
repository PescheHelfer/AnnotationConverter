using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace AnnotationConverter.Helpers
{
    internal static class Utils
    {
        /// <summary>
        /// Converts the unix format used in SQLite to a DateTime object (UTC).
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        internal static DateTime ConvertUnixToDateTime(long unixTime)
        {
            var startDate = new DateTime(1970, 01, 01);
            return startDate.AddMilliseconds(unixTime);
        }

        internal static long ConvertDateTimeToUnix(DateTime dateTime)
        {
            var startDate = new DateTime(1970, 01, 01);
            var ts = dateTime - startDate;
            return (long) ts.TotalMilliseconds;
        }

        internal static string GetAuthors(string metadata)
        {
            int i1, i2 = -1;

            i1 = metadata.IndexOf(@"authors"":[");

            if (i1 == -1)
            {
                return "";
            }

            i2 = metadata.IndexOf(@"""]", i1);

            if (i1 > -1 && i2 > i1)
            {
                return (metadata.Substring(i1 + 9, i2 - i1 - 7)).Replace(@""",""", "; ").Replace("\"", "");
            }

            return "";
        }

        internal delegate object ProvideConnection();

        internal static void VerifyDb(string table1, string table2, string table3, ProvideConnection provideConnection)
        {
            bool dBvalid = false;
            using (var connection = (SQLiteConnection) provideConnection())
            {
                var cmdVerifyDb = new SQLiteCommand(connection)
                {
                    CommandText =
                        string.Format("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name IN ('{0}','{1}','{2}')", table1, table2, table3)
                };

                dBvalid = (long) cmdVerifyDb.ExecuteScalar() == 3;
            }
            if (!dBvalid)
            {
                throw new InvalidDataException(
                    string.Format("The chosen database does not match the expected signature."
                                  + "\r\nAt least one of the following tables is missing in the indicated database:"
                                  + "\r\n'{0}', '{1}', '{2}'",
                        table1, table2, table3));
            }
        }

        internal enum EReader
        {
            SonyPRST,
            AdobeDigitalEditions,
            Mantano
        }

        internal enum CompareOutcomes
        {
            Skip = 0,
            Insert = 1,
            UpdateBothPosIdentical = 2,
            UpdateStartPosIdentical = 3,
            UpdateEndPosIdentical = 4,
            Error = 9
        }
    }
}