using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnotationConverter.Utils
{
    internal static class Utils
    {

        /// <summary>
        /// Converts the unix format used in SQLite to a DateTime object (UTC).
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        internal static DateTime convertUnixToDateTime(long unixTime)
        {
            DateTime startDate = new DateTime(1970, 01, 01);
            return startDate.AddMilliseconds(unixTime);
        }

        internal enum EReader
        {
            SonyPRST,
            AdobeDigitalEditions
            //Mantano
        }
    }
}
